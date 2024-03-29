﻿using SharpDX.DirectSound;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace JocysCom.TextToSpeech.Monitor.Audio
{

	/// <summary>
	/// Summary description for Sound Effects Player.
	/// </summary>
	public partial class AudioPlayer : IDisposable
	{

		public event EventHandler<EventArgs> BeforePlay;

		public AudioPlayer(IntPtr handle)
		{
			_Handle = handle;
		}

		IntPtr _Handle;
		public SecondarySoundBuffer ApplicationBuffer = null;
		DirectSound ApplicationDevice = null;

		[DefaultValue(100)]
		public int Volume { get { return _Volume; } set { _Volume = value; } }
		int _Volume = 100;

		public byte[] GetBytes(Stream stream)
		{
			// Play.
			stream.Position = 0;
			// Make copy of the stream.
			var ms = new MemoryStream();
			int bufSize = 4096;
			byte[] buf = new byte[bufSize];
			int bytesRead = 0;
			while ((bytesRead = stream.Read(buf, 0, bufSize)) > 0)
				ms.Write(buf, 0, bytesRead);
			return ms.ToArray();
		}

		/// <summary>
		/// Load sound data.
		/// </summary>
		/// <param name="wavStream"></param>
		/// <returns>Returns duration.</returns>
		public decimal Load(Stream stream)
		{
			var ms = new MemoryStream();
			var ad = new SharpDX.MediaFoundation.AudioDecoder(stream);
			var samples = ad.GetSamples();
			var enumerator = samples.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var sample = enumerator.Current.ToArray();
				ms.Write(sample, 0, sample.Length);
			}
			var format = ad.WaveFormat;
			var bytes = ms.ToArray();
			return Load(bytes, format.SampleRate, format.BitsPerSample, format.Channels);
		}

		/// <summary>
		/// Load sound data. wavData must not contain WAV Head.
		/// </summary>
		/// <param name="wavBytes"></param>
		/// <returns>Returns duration.</returns>
		public decimal Load(byte[] wavBytes, int sampleRate, int bitsPerSample, int channelCount)
		{
			var format = new SharpDX.Multimedia.WaveFormat(sampleRate, bitsPerSample, channelCount);
			// Create and set the buffer description.
			var desc = new SoundBufferDescription();
			desc.Format = format;
			desc.Flags =
				// Play sound even if application loses focus.
				BufferFlags.GlobalFocus |
				// Allow to control Effects.
				BufferFlags.ControlEffects |
				// Allow to control Volume
				BufferFlags.ControlVolume;
			desc.BufferBytes = wavBytes.Length;
			// Create and set the buffer for playing the sound.
			ApplicationBuffer = new SecondarySoundBuffer(ApplicationDevice, desc);
			ApplicationBuffer.Write(wavBytes, 0, LockFlags.None);
			var duration = AudioHelper.GetDuration(wavBytes.Length, sampleRate, bitsPerSample, channelCount);
			return duration;
		}

		public void Play()
		{
			var ab = ApplicationBuffer;
			if (ab == null)
				return;
			// Used to apply effects.
			var ev = BeforePlay;
			if (ev != null)
				ev(this, new EventArgs());
			// If there is no sound then go to "Playback Devices", select your device,
			// Press [Configure] button, press [Test] button to see if all speaker are producing sound.
			// Volume ranges from -10000 (-100dB silent) to 0 (maximum) and represents the attenuation, in hundredths of a decibel(dB).
			// 10dB increase is required before a sound is perceived to be twice as loud.
			//
			// Doubling of the volume (loudness) should be sensed as a level difference of +10 dB.
			// Doubling the sound pressure (voltage) corresponds to a measured level change of +6 dB.
			// Doubling of sound intensity (acoustic energy) belongs to a calculated level change of +3 dB.
			//
			// Volume    dB     Value
			// ------  -------  -------
			//    100     0.00       0
			//     75    -0.36     -36
			//     50    -6.00    -600
			//     25   -31.11   -3111
			//      0  -100.00  -10000
			// 
			// When [0-100] slider is set to 50.
			//var percent50 = 0.5f;
			//var sliderMax = 100f;
			//var requiredDecibelsAt50Percent = 6f;
			//var maximumReductionOfPowerDecibels = 100f;
			//var raisingPower = -Math.Log10(100f / requiredDecibelsAt50Percent) / Math.Log10(percent50);
			//var newVolume = -Math.Pow((sliderMax - (float)volume) / sliderMax * percent50, raisingPower) * requiredDecibelsAt50Percent * 100f;
			var power = 4f;
			var doublePowerDb = 6f;
			var volume = (int)(-Math.Pow(100f - (float)Volume, power) / Math.Pow(50f, power) * doublePowerDb * 100f);
			ab.Volume = Math.Max(volume, -10000);
			ab.Play(0, PlayFlags.None);
		}

		public void Stop()
		{
			// Build the effects array
			//ApplicationBuffer.Volume = -10000;
			var ab = ApplicationBuffer;
			if (ab != null)
			{
				ab.Stop();
			}
			//ApplicationBuffer.Volume = 0;
		}

		string CurrentDeviceName;

		public void ChangeAudioDevice(string deviceName = null)
		{
			if (CurrentDeviceName == deviceName && ApplicationDevice != null)
				return;
			var playbackDevices = DirectSound.GetDevices();
			// Use default device.
			Guid driverGuid = Guid.Empty;
			foreach (var device in playbackDevices)
			{
				// Pick specific device for the plaback.
				if (string.Compare(device.Description, deviceName, true) == 0)
					driverGuid = device.DriverGuid;
			}
			if (ApplicationDevice != null)
			{
				ApplicationDevice.Dispose();
				ApplicationDevice = null;
			}
			// Create and set the sound device.
			ApplicationDevice = new DirectSound(driverGuid);
			SpeakerConfiguration speakerSet;
			SpeakerGeometry geometry;
			ApplicationDevice.GetSpeakerConfiguration(out speakerSet, out geometry);
			ApplicationDevice.SetCooperativeLevel(_Handle, CooperativeLevel.Normal);
			CurrentDeviceName = deviceName;
		}

		public static string s_DefaultDevice = "Default Device";

		public static string[] GetDeviceNames()
		{
			var list = new List<string>();
			list.Add(s_DefaultDevice);
			var devices = SharpDX.DirectSound.DirectSound.GetDevices();
			foreach (var device in devices)
			{
				if (device.DriverGuid == Guid.Empty)
					continue;
				list.Add(device.Description);
			}
			return list.ToArray();
		}


		#region IDisposable

		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		bool IsDisposing;

		void Dispose(bool disposing)
		{
			if (disposing)
			{

				// Don't dispose twice.
				if (IsDisposing)
					return;
				IsDisposing = true;
				if (ApplicationDevice != null) ApplicationDevice.Dispose();
				if (ApplicationBuffer != null) ApplicationBuffer.Dispose();
				_Handle = IntPtr.Zero;
			}
		}

		#endregion

	}
}