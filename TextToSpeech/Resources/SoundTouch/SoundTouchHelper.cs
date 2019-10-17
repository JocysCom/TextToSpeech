using SoundTouch;
using System;
using System.Diagnostics;
using System.IO;
using TLongSampleType = System.Double;
using TSampleType = System.Single;
//using TSampleType = System.Int16;
//using TLongSampleType = System.Int64;

namespace SoundStretch
{
	internal static class SoundTouchHelper
	{

		//private static int Main(string[] args)
		//{
		//	var parameters = new RunParameters();
		//	parameters.TempoDelta = 50;
		//	parameters.PitchDelta = 5;
		//	parameters.Speech = true;
		//	var inBytes = System.IO.File.ReadAllBytes("Test.wav");
		//	var inStream = new MemoryStream(inBytes);
		//	var outStream = new MemoryStream();
		//	Process(inStream, outStream, parameters);
		//	var outBytes = outStream.ToArray();
		//	File.WriteAllBytes("Test_Pith_Tempo_ms.wav", outBytes);
		//	return 0;
		//}


		public static void Process(Stream inStream, Stream outStream, RunParameters parameters)
		{
			var soundTouch = new SoundTouch<TSampleType, TLongSampleType>();
			// Open Input file.
			var inFile = new WavInFile(inStream);

			//var inFile = new WavInFile("Test.wav");
			int bits = inFile.GetNumBits();
			int samplerate = inFile.GetSampleRate();
			int channels = inFile.GetNumChannels();
			// Open output file.
			var outFile = new WavOutFile(outStream, samplerate, bits, channels);
			//var outFile = new WavOutFile("Test_Pith_Tempo.wav", samplerate, bits, channels);
			if (parameters.DetectBpm)
			{
				// detect sound BPM (and adjust processing parameters
				//  accordingly if necessary)
				DetectBpm(inFile, parameters);
			}
			// Setup the 'SoundTouch' object for processing the sound
			int sampleRate = inFile.GetSampleRate();
			soundTouch.SetSampleRate(sampleRate);
			soundTouch.SetChannels(channels);
			soundTouch.SetTempoChange(parameters.TempoDelta);
			soundTouch.SetPitchSemiTones(parameters.PitchDelta);
			soundTouch.SetRateChange(parameters.RateDelta);
			soundTouch.SetSetting(SettingId.UseQuickseek, parameters.Quick);
			soundTouch.SetSetting(SettingId.UseAntiAliasFilter, (parameters.NoAntiAlias == 1) ? 0 : 1);
			if (parameters.Speech)
			{
				// use settings for speech processing
				soundTouch.SetSetting(SettingId.SequenceDurationMs, 40);
				soundTouch.SetSetting(SettingId.SeekwindowDurationMs, 15);
				soundTouch.SetSetting(SettingId.OverlapDurationMs, 8);
			}
			// Process the sound
			Process(soundTouch, inFile, outFile);
			if (inFile != null) inFile.Dispose();
			if (outFile != null)
			{
				outFile.Dispose();
			}
		}

		// Processing chunk size (size chosen to be divisible by 2, 4, 6, 8, 10, 12, 14, 16 channels ...)
		private const int BUFF_SIZE = 6720;

		/// <summary>
		/// Processes the sound.
		/// </summary>
		private static void Process(SoundTouch<TSampleType, TLongSampleType> pSoundTouch, WavInFile inFile, WavOutFile outFile)
		{
			int nSamples;
			var sampleBuffer = new TSampleType[BUFF_SIZE];

			if ((inFile == null) || (outFile == null)) return; // nothing to do.

			int nChannels = inFile.GetNumChannels();
			Debug.Assert(nChannels > 0);
			int buffSizeSamples = BUFF_SIZE / nChannels;

			// Process samples read from the input file
			while (!inFile.Eof())
			{
				// Read a chunk of samples from the input file
				int num = inFile.Read(sampleBuffer, BUFF_SIZE);
				nSamples = num / inFile.GetNumChannels();

				// Feed the samples into SoundTouch processor
				pSoundTouch.PutSamples(sampleBuffer, nSamples);

				// Read ready samples from SoundTouch processor & write them output file.
				// NOTES:
				// - 'receiveSamples' doesn't necessarily return any samples at all
				//   during some rounds!
				// - On the other hand, during some round 'receiveSamples' may have more
				//   ready samples than would fit into 'sampleBuffer', and for this reason 
				//   the 'receiveSamples' call is iterated for as many times as it
				//   outputs samples.
				do
				{
					nSamples = pSoundTouch.ReceiveSamples(sampleBuffer, buffSizeSamples);
					outFile.Write(sampleBuffer, nSamples * nChannels);
				} while (nSamples != 0);
			}

			// Now the input file is processed, yet 'flush' few last samples that are
			// hiding in the SoundTouch's internal processing pipeline.
			pSoundTouch.Flush();
			do
			{
				nSamples = pSoundTouch.ReceiveSamples(sampleBuffer, buffSizeSamples);
				outFile.Write(sampleBuffer, nSamples * nChannels);
			} while (nSamples != 0);
		}

		/// <summary>
		/// Detect BPM rate of <paramref name="inFile"/> and adjust tempo
		/// setting accordingly if necessary.
		/// </summary>
		private static void DetectBpm(WavInFile inFile, RunParameters parameters)
		{
			var bpm = BpmDetect<TSampleType, TLongSampleType>.NewInstance(inFile.GetNumChannels(), inFile.GetSampleRate());
			var sampleBuffer = new TSampleType[BUFF_SIZE];

			// detect bpm rate
			Console.Error.Write("Detecting BPM rate...");
			Console.Error.Flush();

			int nChannels = inFile.GetNumChannels();
			Debug.Assert(BUFF_SIZE % nChannels == 0);

			// Process the 'inFile' in small blocks, repeat until whole file has 
			// been processed
			while (inFile.Eof() == false)
			{
				// Read sample data from input file
				int num = inFile.Read(sampleBuffer, BUFF_SIZE);

				// Enter the new samples to the bpm analyzer class
				int samples = num / nChannels;
				bpm.InputSamples(sampleBuffer, samples);
			}

			// Now the whole song data has been analyzed. Read the resulting bpm.
			float bpmValue = bpm.GetBpm();
			Console.Error.WriteLine("Done!");

			// rewind the file after bpm detection
			inFile.Rewind();

			if (bpmValue > 0)
			{
				Console.Error.WriteLine("Detected BPM rate {0:0.0}\n", bpmValue);
			}
			else
			{
				Console.Error.WriteLine("Couldn't detect BPM rate.\n");
				return;
			}
			if (parameters.GoalBpm > 0)
			{
				// adjust tempo to given bpm
				parameters.TempoDelta = (parameters.GoalBpm / bpmValue - 1.0f) * 100.0f;
				Console.Error.WriteLine("The file will be converted to {0:0.0} BPM\n", parameters.GoalBpm);
			}
		}

	}
}