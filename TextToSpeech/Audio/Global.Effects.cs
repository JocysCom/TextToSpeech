using System.IO;
using System.Threading;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		public static bool ApplyEffects = false;
		public static float PitchShift = 1.0F;

		static void ApplyPitch(PlayItem item)
		{
			// Get info about the WAV.
			int sampleRate = item.WavHead.SampleRate;
			int bitsPerSample = item.WavHead.BitsPerSample;
			int channelCount = item.WavHead.Channels;
			// Get info about effects and pitch.
			var ms = new MemoryStream();
			var writer = new System.IO.BinaryWriter(ms);
			var bytes = item.WavData;
			// Add 100 milliseconds at the start.
			var silenceStart = 100;
			// Add 200 milliseconds at the end.
			var silenceEnd = 200;
			var silenceBytes = AudioHelper.GetSilenceByteCount(sampleRate, bitsPerSample, channelCount, silenceStart + silenceEnd);
			// Comment WriteHeader(...) line, because SharpDX don't need that (it creates noise).
			//AudioHelper.WriteHeader(writer, bytes.Length + silenceBytes, channelCount, sampleRate, bitsPerSample);
			if (ApplyEffects)
			{
				token = new CancellationTokenSource();
				// This part could take long time.
				bytes = EffectsGeneral.ApplyPitchShift(bytes, channelCount, sampleRate, bitsPerSample, PitchShift, token);
				// If pitch shift was canceled then...
				if (token.IsCancellationRequested)
					return;
			}
			// Add silence at the start to make room for effects.
			Audio.AudioHelper.WriteSilenceBytes(writer, sampleRate, bitsPerSample, channelCount, silenceStart);
			writer.Write(bytes);
			// Add silence at the back to make room for effects.
			Audio.AudioHelper.WriteSilenceBytes(writer, sampleRate, bitsPerSample, channelCount, silenceEnd);
			// Add result to play list.
			item.WavData = ms.ToArray();
			//System.IO.File.WriteAllBytes("Temp.wav", item.Data);
			var duration = ((decimal)bytes.Length * 8m) / (decimal)channelCount / (decimal)sampleRate / (decimal)bitsPerSample * 1000m;
			duration += (silenceStart + silenceEnd);
			item.Duration = (int)duration;
		}

	}
}
