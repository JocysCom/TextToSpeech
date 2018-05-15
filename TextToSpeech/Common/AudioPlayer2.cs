using System;
using System.IO;
using SharpDX;
using SharpDX.IO;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System.Linq;
using System.Collections.Generic;
using JocysCom.ClassLibrary.Controls.IssuesControl;

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

		static bool ManagerStarted;
		static object ManagerLock = new object();


		public AudioPlayer2(string deviceName = null)
		{
			lock (ManagerLock)
			{
				if (!ManagerStarted)
				{
					// This is mandatory when using any of SharpDX.MediaFoundation classes
					MediaManager.Startup();
					ManagerStarted = true;
				}
			}
			// Starts The XAudio2 engine
			// Version XAudio2Version.Version27 is required for GetDeviceDetails to work.
			ChangeAudioDevice(deviceName ?? s_DefaultDevice);
		}

		public static string s_DefaultDevice = "Default Device";

		public string[] GetDeviceNames()
		{
			var list = new List<string>();
			list.Add(s_DefaultDevice);
			lock (lockAudio)
			{
				// +-----------------------------------------------------+
				// |                    |  Platform      | Major | Minor |
				// +-----------------------------------------------------+
				// | Windows 95         |  Win32Windows  |   4   |   0   |
				// | Windows 98         |  Win32Windows  |   4   |  10   |
				// | Windows Me         |  Win32Windows  |   4   |  90   |
				// | Windows NT 4.0     |  Win32NT       |   4   |   0   |
				// | Windows 2000       |  Win32NT       |   5   |   0   |
				// | Windows XP         |  Win32NT       |   5   |   1   |
				// | Windows 2003       |  Win32NT       |   5   |   2   |
				// | Windows Vista      |  Win32NT       |   6   |   0   |
				// | Windows 2008       |  Win32NT       |   6   |   0   |
				// | Windows 7          |  Win32NT       |   6   |   1   |
				// | Windows 2008 R2    |  Win32NT       |   6   |   1   |
				// | Windows 8          |  Win32NT       |   6   |   2   |
				// | Windows 8.1        |  Win32NT       |   6   |   3   |
				// | Windows 10         |  Win32NT       |  10   |   0   |
				// +-----------------------------------------------------+
				//
				var version = IssueHelper.GetRealOSVersion();
				// If windows 8 +
				if (version >= new Version(6, 2))
				{
					var devices = SharpDX.DirectSound.DirectSound.GetDevices();
					var names = devices.Where(x => x.DriverGuid != Guid.Empty).Select(x => x.Description);
					list.AddRange(names);
				}
				else
				{
					var count = xaudio2.DeviceCount;
					for (int i = 0; i < count; i++)
					{
						var di = xaudio2.GetDeviceDetails(i);
						list.Add(di.DisplayName);
					}
				}
			}
			return list.ToArray();
		}

		public string CurrentDeviceName;

		public void ChangeAudioDevice(string deviceName)
		{
			lock (lockAudio)
			{
				if (deviceName == CurrentDeviceName && masteringVoice != null)
					return;
				var version = IssueHelper.GetRealOSVersion();
				// If windows 8 +
				if (version >= new Version(6, 2))
				{
					xaudio2 = new XAudio2(XAudio2Version.Version28);
				}
				else
				{
					xaudio2 = new XAudio2(XAudio2Version.Version27);
				}
				xaudio2.StartEngine();
				if (masteringVoice != null)
				{
					Utilities.Dispose(ref masteringVoice);
				}
				// If windows 8 +
				if (version >= new Version(6, 2))
				{
					var devices = SharpDX.DirectSound.DirectSound.GetDevices();
					var deviceId = devices.Where(x => x.Description == deviceName).Select(x => x.ModuleName).FirstOrDefault();
					// If device found then..
					masteringVoice = string.IsNullOrEmpty(deviceId)
						? new MasteringVoice(xaudio2)
						: new MasteringVoice(xaudio2, 2, 44100, deviceId);
				}
				else
				{
					int deviceIndex = -1;
					var count = xaudio2.DeviceCount;
					for (int i = 0; i < count; i++)
					{
						var di = xaudio2.GetDeviceDetails(i);
						if (di.DisplayName == deviceName)
						{
							deviceIndex = i;
							break;
						}
					}
					masteringVoice = deviceIndex > -1
						? new MasteringVoice(xaudio2, 0, 0, deviceIndex)
						: new MasteringVoice(xaudio2);
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
					//audioPlayer.Close();
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