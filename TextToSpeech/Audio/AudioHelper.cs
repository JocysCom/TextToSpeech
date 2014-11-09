using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public class AudioHelper
	{

		#region WAV Header Writer

		static char[] CHUNK_ID = { 'R', 'I', 'F', 'F' };
		static char[] CHUNK_FORMAT = { 'W', 'A', 'V', 'E' };
		static char[] SUBCHUNK1_ID = { 'f', 'm', 't', ' ' };
		static char[] SUBCHUNK2_ID = { 'd', 'a', 't', 'a' };
		static byte[] AUDIO_FORMAT = new byte[] { 0x01, 0x00 };

		public static void WriteHeader(System.IO.BinaryWriter writer, int byteStreamSize, int channelCount, int sampleRate, int bitsPerSample)
		{
			int bytesPerSample = bitsPerSample / 8;
			int byteRate = sampleRate * channelCount * bytesPerSample;
			int blockAlign = channelCount * bytesPerSample;
			int subChunk1Size = 16;
			writer.Write(CHUNK_ID, 0, CHUNK_ID.Length);
			writer.Write(PackageInt(byteStreamSize + 42, 4), 0, 4);
			writer.Write(CHUNK_FORMAT, 0, CHUNK_FORMAT.Length);
			writer.Write(SUBCHUNK1_ID, 0, SUBCHUNK1_ID.Length);
			writer.Write(PackageInt(subChunk1Size, 4), 0, 4);
			writer.Write(AUDIO_FORMAT, 0, AUDIO_FORMAT.Length);
			writer.Write(PackageInt(channelCount, 2), 0, 2);
			writer.Write(PackageInt(sampleRate, 4), 0, 4);
			writer.Write(PackageInt(byteRate, 4), 0, 4);
			writer.Write(PackageInt(blockAlign, 2), 0, 2);
			writer.Write(PackageInt(bytesPerSample * 8), 0, 2);
			//targetStream.Write(PackageInt(0,2), 0, 2); // Extra param size
			writer.Write(SUBCHUNK2_ID, 0, SUBCHUNK2_ID.Length);
			writer.Write(PackageInt(byteStreamSize, 4), 0, 4);
		}

		public static int GetSilenceByteCount(int channelCount, int sampleRate, int bitsPerSample, int milliseconds)
		{
			int bytesPerSample = bitsPerSample / 8;
			// Get amount of bytes needed to write for audio with one channel and one byte per sample.
			var dataLength = (int)((decimal)sampleRate * ((decimal)milliseconds / 1000));
			// Get total amount of bytes written.
			int totalBytes = dataLength * channelCount * bytesPerSample;
			return totalBytes;
		}

		public static int WriteSilenceBytes(System.IO.BinaryWriter writer, int channelCount, int sampleRate, int bitsPerSample, int milliseconds)
		{
			int bytesPerSample = bitsPerSample / 8;
			// Get amount of bytes needed to write for audio with one channel and one byte per sample.
			var dataLength = (int)((decimal)sampleRate * ((decimal)milliseconds / 1000));
			for (int i = 0; i < dataLength; i++)
			{
				if (bytesPerSample == 1)
				{
					for (int c = 0; c < channelCount; c++)	Write8bit(writer, 0F, 0F);
				}
				else
				{
					for (int c = 0; c < channelCount; c++) Write16bit(writer, 0F, 0F);
				}
			}
			// Get total amount of bytes written.
			int totalBytes = dataLength * channelCount * bytesPerSample;
			return totalBytes;
		}

		/// <summary>Convert byte to one double in the range -1 to 1</summary>
		public static float BytesToNormalized_8(byte firstByte)
		{
			return (float)(firstByte + sbyte.MinValue) / 128F;
		}

		/// <summary>Convert a float value into two bytes (use k as conversion value and not Int16.MaxValue to avoid peaks)</summary>
		public static void  Write8bit(BinaryWriter writer, float value, float k)
		{
			sbyte s = (sbyte)(value * k);
			// Write byte.
			writer.Write((byte)(s - sbyte.MinValue));
		}

		/// <summary>Convert two bytes to one double in the range -1 to 1</summary>
		public static float BytesToNormalized_16(byte firstByte, byte secondByte)
		{
			// convert two bytes to one short (little endian)
			short s = (short)((secondByte << 8) | firstByte);
			// convert to range from -1 to (just below) 1
			return (float)s / 32678f;
		}

		/// <summary>Convert a float value into two bytes (use k as conversion value and not Int16.MaxValue to avoid peaks)</summary>
		public static void Write16bit(BinaryWriter writer, float value, float k)
		{
			short s = (short)(value * k);
			// Write first byte.
			writer.Write((byte)(s & 0xFF));
			// Write second byte.
			writer.Write((byte)(s >> 8));
		}

		static byte[] PackageInt(int source, int length = 2)
		{
			if ((length != 2) && (length != 4)) throw new ArgumentException("Length must be either 2 or 4", "length");
			var retVal = new byte[length];
			retVal[0] = (byte)(source & 0xFF);
			retVal[1] = (byte)((source >> 8) & 0xFF);
			if (length == 4)
			{
				retVal[2] = (byte)((source >> 0x10) & 0xFF);
				retVal[3] = (byte)((source >> 0x18) & 0xFF);
			}
			return retVal;
		}

		#endregion

	}
}
