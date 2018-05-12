using System;
using System.IO;
using SharpDX;
using SharpDX.IO;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System.Linq;
using System.Collections.Generic;

namespace AudioPlayerApp
{
	/// <summary>
	/// AudioPlayer using XAudio2 and MediaFoundation.
	/// </summary>
	public partial class AudioPlayer2 : IDisposable
	{
		private XAudio2 xaudio2;
		private MasteringVoice masteringVoice;
		private Stream audioStream;
		private AudioPlayer audioPlayer;
		private object lockAudio = new object();

		public AudioPlayer2(string deviceName = null)
		{
			// This is mandatory when using any of SharpDX.MediaFoundation classes
			MediaManager.Startup();
			// Starts The XAudio2 engine
			// Version XAudio2Version.Version27 is required for GetDeviceDetails to work.
			xaudio2 = new XAudio2(XAudio2Version.Version27);
			xaudio2.StartEngine();
			ChangeAudioDevice(deviceName ?? s_DefaultDevice);
		}

		public static string s_DefaultDevice = "Default Device";

		public string[] GetDeviceNames()
		{
			var list = new List<string>();
			list.Add(s_DefaultDevice);
			lock (lockAudio)
			{
				var count = xaudio2.DeviceCount;
				for (int i = 0; i < count; i++)
				{
					var di = xaudio2.GetDeviceDetails(i);
					var name = string.Format("{0}. {1}", i + 1, di.DisplayName);
					list.Add(name);
				}
			}
			return list.ToArray();
		}

		public string CurrentDeviceName;

		public void ChangeAudioDevice(string deviceName)
		{
			lock (lockAudio)
			{
				if (deviceName == CurrentDeviceName)
					return;
				int deviceIndex = -1;
				var count = xaudio2.DeviceCount;
				for (int i = 0; i < count; i++)
				{
					var di = xaudio2.GetDeviceDetails(i);
					var name = string.Format("{0}. {1}", i + 1, di.DisplayName);
					if (name == deviceName)
					{
						deviceIndex = i;
						break;
					}
				}
				if (masteringVoice != null)
				{
					Utilities.Dispose(ref masteringVoice);
				}
				// If device found then..
				if (deviceIndex > -1)
				{
					masteringVoice = new MasteringVoice(xaudio2,  0, 0, deviceIndex);
				}
				else
				{
					// Use default device.
					masteringVoice = new MasteringVoice(xaudio2);
				}
				CurrentDeviceName = deviceName;
			}
		}

		public void Play()
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
				{
					if (audioPlayer.State != AudioPlayerState.Playing)
						audioPlayer.Play();
				}
			}
		}

		private void Pause()
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
				{
					if (audioPlayer.State == AudioPlayerState.Playing)
						audioPlayer.Pause();
				}
			}
		}

		public void Stop()
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
					audioPlayer.Stop();
			}
		}

		public void Load(string fileName, bool autoPlay = false)
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
				{
					audioPlayer.Close();
					audioPlayer = null;
				}
				if (audioStream != null)
				{
					audioStream.Close();
					audioStream = null;
				}
				audioStream = new NativeFileStream(fileName, NativeFileMode.Open, NativeFileAccess.Read);
				// Ask the user for a video or audio file to play
				audioPlayer = new AudioPlayer(xaudio2, audioStream);
				if (autoPlay)
					audioPlayer.Play();
			}
		}

		public void Load(Stream stream, bool autoPlay = false)
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
				{
					audioPlayer.Close();
					audioPlayer = null;
				}
				if (audioStream != null)
				{
					audioStream.Close();
					audioStream = null;
				}
				audioStream = stream;
				// Ask the user for a video or audio file to play
				audioPlayer = new AudioPlayer(xaudio2, audioStream);
				if (autoPlay)
					audioPlayer.Play();
			}
		}

		/// <summary>
		/// 0.0 - 1.0
		/// </summary>
		/// <param name="value"></param>
		public void SetVolume(float value)
		{
			lock (lockAudio)
			{
				if (audioPlayer != null)
				{
					var volume = (float)Math.Min(1.0, Math.Max(0.0, value));
					audioPlayer.Volume = volume;
				}
			}
		}

		#region IDisposable

		// Dispose() calls Dispose(true)
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		bool IsDisposing;

		// The bulk of the clean-up code is implemented in Dispose(bool)
		void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Don't dispose twice.
				if (IsDisposing)
					return;
				IsDisposing = true;
				lock (lockAudio)
				{
					if (audioPlayer != null)
					{
						Utilities.Dispose(ref masteringVoice);
						Utilities.Dispose(ref xaudio2);
						audioPlayer = null;
					}
				}
			}
		}

		#endregion

	}
}