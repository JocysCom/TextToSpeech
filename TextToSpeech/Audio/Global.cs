using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		public static BindingList<PlayItem> playlist;
		static object playlistLock = new object();
		public static IntPtr Handle;
		static AudioPlayer WavPlayer;
		public static AudioPlayer EffectsPlayer;

		public static event EventHandler<EventArgs<VoiceListItem>> AddingVoiceListItem;
		public static event EventHandler<EventArgs<InstalledVoiceEx>> VoiceChanged;
		public static event EventHandler<EventArgs<Exception>> Exception;

		public static void OnEvent<T>(EventHandler<EventArgs<T>> handler, T data)
		{
			if (handler != null)
				ControlsHelper.Invoke(handler, null, new EventArgs<T>(data));
		}

		public static void InitGlobal(IntPtr handle)
		{
			Handle = handle;
			InstalledVoices = new BindingList<InstalledVoiceEx>();
			LocalVoices = new BindingList<InstalledVoiceEx>();
			AmazonNeuralVoices = new BindingList<InstalledVoiceEx>();
			AmazonStandardVoices = new BindingList<InstalledVoiceEx>();
			WavPlayer = new AudioPlayer(Handle);
			EffectsPlayer = new AudioPlayer(Handle);
			EffectsPlayer = new AudioPlayer(Handle);
			EffectsPlayer.ChangeAudioDevice();
			playlist = new BindingList<PlayItem>();
			playlist.ListChanged += playlist_ListChanged;
			// Track Amazon Polly errors globally.
			Voices.AmazonPolly.Exception += AmazonPolly_Exception;
			SettingsManager.Options.PropertyChanged += Options_PropertyChanged;
		}

		private static void Options_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(SettingsManager.Options.Volume))
			{
				WavPlayer.Volume = SettingsManager.Options.Volume;
			}
		}

		private static void AmazonPolly_Exception(object sender, EventArgs<Exception> e)
		{
			OnEvent(Exception, e.Data);
		}

		public static void DisposeGlobal()
		{
			if (EffectsPlayer != null)
			{
				EffectsPlayer.Dispose();
				EffectsPlayer = null;
			}
			if (token != null)
			{
				token.Cancel();
				token.Dispose();
			}
		}


		#region Intro Sounds

		public static string[] GetIntroSoundNames()
		{
			var prefix = ".Audio.";
			var assembly = Assembly.GetExecutingAssembly();
			var names = assembly.GetManifestResourceNames().Where(x => x.Contains(prefix)).ToArray();
			names = names.Select(x => x.Substring(x.IndexOf(prefix) + prefix.Length).Replace(".wav", "")).ToArray();
			return names;
		}

		public static Stream GetIntroSound(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;
			var suffix = (".Audio." + name + ".wav").ToLower();
			var assembly = Assembly.GetExecutingAssembly();
			var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.ToLower().EndsWith(suffix));
			return fullResourceName == null ? null : assembly.GetManifestResourceStream(fullResourceName);
		}

		public static void AddIntroSoundToPlayList(string text, string group, Stream stream)
		{
			var item = new PlayItem()
			{
				Text = text,
				WavData = new byte[0],
				StreamData = stream,
				Group = group,
				Status = JobStatusType.Pitched,
			};
			lock (playlistLock) { playlist.Add(item); }
		}

		public static void PlayCurrentIntroSound()
		{
			var introSound = SettingsManager.Options.DefaultIntroSoundComboBox.ToLower();
			var stream = GetIntroSound(introSound);
			if (stream != null)
			{
				var introSoundPlayer = new AudioPlayer(Handle);
				introSoundPlayer.ChangeAudioDevice(SettingsManager.Options.PlaybackDevice);
				introSoundPlayer.Load(stream);
				introSoundPlayer.Volume = SettingsManager.Options.Volume;
				introSoundPlayer.Play();
			}
		}

		public static void PlayLogSound()
		{
			var stream = GetIntroSound("Radio2");
			if (stream != null)
			{
				var logSoundPlayer = new AudioPlayer(Handle);
				logSoundPlayer.ChangeAudioDevice(SettingsManager.Options.PlaybackDevice);
				logSoundPlayer.Load(stream);
				logSoundPlayer.Volume = SettingsManager.Options.Volume;
				logSoundPlayer.Play();
			}
		}

		#endregion

		#region Play List

		static bool threadIsRunning;
		static object threadIsRunningLock = new object();

		/// <summary>
		/// This event will fire when item is added, removed or change in the list.
		/// </summary>
		static void playlist_ListChanged(object sender, ListChangedEventArgs e)
		{
			var changed = (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Status");
			var added = e.ListChangedType == ListChangedType.ItemAdded;
			// CheckPlayList will adjust playlist item properties.
			// Item will be in the process of adding to the grid control and could fail.
			// Use BeginInvoke (delay execution) to make sure that grid control will complete update.
			ControlsHelper.BeginInvoke(() => { CheckPlayList(added, changed); });
		}

		static void CheckPlayList(bool added, bool changed)
		{
			// If new item was added or item status changed then...
			var items = playlist.ToArray();
			if (added || changed)
			{
				// If nothing is playing then...
				if (!items.Any(x => x.Status == JobStatusType.Playing))
				{
					// Get first item ready to play.
					var pitchedItem = items.FirstOrDefault(x => x.Status == JobStatusType.Pitched);
					//playlist.Remove(item);
					if (pitchedItem != null)
					{
						if (pitchedItem.StreamData != null)
						{
							// Takes WAV bytes without header.
							WavPlayer.ChangeAudioDevice(SettingsManager.Options.PlaybackDevice);
							var duration = (int)WavPlayer.Load(pitchedItem.StreamData);
							WavPlayer.Volume = SettingsManager.Options.Volume;
							WavPlayer.Play();
							// Start timer which will reset status to Played
							pitchedItem.Duration = duration;
							pitchedItem.StartPlayTimer();
						}
						else
						{
							// Must be outside begin invoke.
							int sampleRate = pitchedItem.WavHead.SampleRate;
							int bitsPerSample = pitchedItem.WavHead.BitsPerSample;
							int channelCount = pitchedItem.WavHead.Channels;
							// Takes WAV bytes without header.
							EffectsPlayer.ChangeAudioDevice(SettingsManager.Options.PlaybackDevice);
							EffectsPlayer.Load(pitchedItem.WavData, sampleRate, bitsPerSample, channelCount);
							EffectsPlayer.Volume = SettingsManager.Options.Volume;
							EffectsPlayer.Play();
							// Start timer which will reset status to Played
							pitchedItem.StartPlayTimer();
						}
					}
				}
				// If last item finished playing or any item resulted in error then clear then..
				var lastItem = items.LastOrDefault();
				if ((lastItem != null && lastItem.Status == JobStatusType.Played) || (items.Any(x => x.Status == JobStatusType.Error)))
				{
					ControlsHelper.BeginInvoke(() =>
					{
						bool groupIsPlaying;
						int itemsLeftToPlay;
						lock (playlistLock) { ClearPlayList(null, out groupIsPlaying, out itemsLeftToPlay); }
					});
				}
				else
				{
					ControlsHelper.BeginInvoke(() =>
					{
						lock (threadIsRunningLock)
						{
							// If thread is not running or stopped then...
							if (!threadIsRunning)
							{
								threadIsRunning = true;
								ThreadPool.QueueUserWorkItem(ProcessPlayItems);
							}
						}
					});
				}
			}
		}

		/// <summary>
		/// If group specified then remove only items from the group.
		/// </summary>
		/// <param name="group"></param>
		static void ClearPlayList(string group, out bool groupIsPlaying, out int itemsLeftToPlay)
		{
			PlayItem[] itemsToClear;
			if (string.IsNullOrEmpty(group))
			{
				itemsToClear = playlist.ToArray();
				groupIsPlaying = false;
			}
			else
			{
				itemsToClear = playlist.Where(x => x.Group != null && x.Group.ToLower() == group.ToLower()).ToArray();
				groupIsPlaying = itemsToClear.Any(x => x.Status == JobStatusType.Playing);
			}
			foreach (var item in itemsToClear)
			{
				item.Dispose();
				playlist.Remove(item);
			}
			itemsLeftToPlay = playlist.Count();
		}



		#endregion

		public static void StopPlayer(string group = null)
		{
			bool groupIsPlaying;
			int itemsLeftToPlay;
			lock (playlistLock) { ClearPlayList(group, out groupIsPlaying, out itemsLeftToPlay); }
			if (groupIsPlaying || itemsLeftToPlay == 0)
			{
				addMessage = null;
				if (token != null)
					token.Cancel();
				EffectsPlayer.Stop();
				WavPlayer.Stop();
			}
			if (itemsLeftToPlay > 0)
			{
				CheckPlayList(false, true);
			}
		}

		public static void AddMessageToPlay(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("<message"))
				{
					var voiceItem = (VoiceListItem)Activator.CreateInstance(Program.MonitorItem.GetType());
					voiceItem.Load(text);
					addVoiceListItem(voiceItem);
				}
				else
				{
					var item = new PlayItem()
					{
						Text = "SAPI XML",
						Xml = text,
						Status = JobStatusType.Parsed,
					};
					lock (playlistLock) { playlist.Add(item); }
				}
			}
		}

		public static void addVoiceListItem(VoiceListItem item)
		{
			OnEvent(AddingVoiceListItem, item);
			ProcessVoiceTextMessage(item.VoiceXml);
		}

	}
}
