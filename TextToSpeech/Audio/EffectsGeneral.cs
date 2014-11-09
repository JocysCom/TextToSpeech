using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public struct EffectsGeneral
	{

		public float Pitch { get; set; }

		#region Helper Methods

		public static byte[] ApplyPitchShift(byte[] bytes, int channels, int sampleRate, int bitsPerSample, float pitchShift, CancellationTokenSource token)
		{
			float[] in_data_l = null;
			float[] in_data_r = null;
			var newBytes = new byte[bytes.Length];
			GetWaveData(bytes, channels, sampleRate, bitsPerSample, out in_data_l, out in_data_r);
			// Apply Pitch Shifting
			if (in_data_l != null)
			{
				Mike.Rules.PitchShifter.PitchShift(pitchShift, in_data_l.Length, (long)1024, (long)10, sampleRate, in_data_l, token);
				if (token != null && token.IsCancellationRequested) return newBytes;
			}
			if (in_data_r != null)
			{
				Mike.Rules.PitchShifter.PitchShift(pitchShift, in_data_r.Length, (long)1024, (long)10, sampleRate, in_data_r, token);
				if (token != null && token.IsCancellationRequested) return newBytes;
			}
			GetWaveData(in_data_l, in_data_r, ref newBytes, bitsPerSample);
			return newBytes;
		}


		// Returns left and right float arrays. 'right' will be null if sound is mono.
		public static void GetWaveData(byte[] wav, int channels, int sampleRate, int bitsPerSample, out float[] left, out float[] right)
		{
			int bytesPerSample = bitsPerSample / 8;
			int samples = wav.Length / bytesPerSample / channels;
			// Allocate memory (right will be null if only mono sound)
			left = new float[samples];
			right = (channels == 2) ? new float[samples] : null;
			// Write to float array/s:
			var subchunk2Size = wav.Length;
			var pos = 0;
			int i = 0;
			while (pos < subchunk2Size)
			{
				if (bytesPerSample == 1)
				{
					left[i] = AudioHelper.BytesToNormalized_8(wav[pos]);
					pos += 1;
					if (channels == 2)
					{
						right[i] = AudioHelper.BytesToNormalized_8(wav[pos]);
						pos += 1;
					}
					i++;
				}
				else
				{
					left[i] = AudioHelper.BytesToNormalized_16(wav[pos], wav[pos + 1]);
					pos += 2;
					if (channels == 2)
					{
						right[i] = AudioHelper.BytesToNormalized_16(wav[pos], wav[pos + 1]);
						pos += 2;
					}
					i++;
				}

			}
		}

		// Return byte data from left and right float data. Ignore right when sound is mono
		public static void GetWaveData(float[] left, float[] right, ref byte[] data, int bitsPerSample)
		{
			int bytesPerSample = bitsPerSample / 8;
			// Calculate k
			// This value will be used to convert float to Int16
			// We are not using Int16.Max to avoid peaks due to overflow conversions            
			float k = (bytesPerSample == 1)
				? (float)sbyte.MaxValue / left.Select(x => Math.Abs(x)).Max()
				: (float)short.MaxValue / left.Select(x => Math.Abs(x)).Max();
			// Revert data to byte format
			Array.Clear(data, 0, data.Length);
			int dataLenght = left.Length;
			using (BinaryWriter writer = new BinaryWriter(new MemoryStream(data)))
			{
				for (int i = 0; i < dataLenght; i++)
				{
					if (bytesPerSample == 1)
					{
						AudioHelper.Write8bit(writer, left[i], k);
						if (right != null) AudioHelper.Write8bit(writer, right[i], k);
					}
					else
					{
						AudioHelper.Write16bit(writer, left[i], k);
						if (right != null) AudioHelper.Write16bit(writer, right[i], k);
					}
				}
			}
		}

		#endregion

	}
}
