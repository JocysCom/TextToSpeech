using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			Program.TopForm = this;
			InitializeComponent();
			if (IsDesignMode) return;
			WavPlayer = new AudioPlayer(Handle);

			playlist = new BindingList<PlayItem>();
			playlist.ListChanged += playlist_ListChanged;
			PlayListDataGridView.AutoGenerateColumns = false;
			PlayListDataGridView.DataSource = playlist;
			Text = MainHelper.GetProductFullName();
			InstalledVoices = new BindingList<InstalledVoiceEx>();
			DefaultIntroSoundComboBox.DataSource = GetIntroSoundNames();
			UpdateLabel.Text = "You are running " + MainHelper.GetProductFullName();
			// Add supported items.
			PlugIns.Add(new WowListItem());
			ProgramComboBox.DataSource = PlugIns;
			ProgramComboBox.DisplayMember = "Name";
			var name = Properties.Settings.Default.ProgramComboBoxText;
			if (!string.IsNullOrEmpty(name))
			{
				ProgramComboBox.Text = name;
			}
			// If nothing is selected but list have values.
			if (ProgramComboBox.SelectedIndex == -1 && ProgramComboBox.Items.Count > 0)
			{
				// Select first one.
				ProgramComboBox.SelectedIndex = 0;
			}
			MonitorItem = (VoiceListItem)ProgramComboBox.SelectedItem;
			ProgramComboBox.SelectedIndexChanged += ProgramComboBox_SelectedIndexChanged;
		}


		public bool IsDesignMode
		{
			get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
		}

		List<VoiceListItem> PlugIns = new List<VoiceListItem>();

		//System.Media.SoundPlayer WavPlayer;
		AudioPlayer WavPlayer;

		void InstalledVoices_ListChanged(object sender, ListChangedEventArgs e)
		{

			ValidateList();
		}

		void ValidateList()
		{
			string error = "";
			if (InstalledVoices.Count == 0) error = "No voices were found";
			else if (!InstalledVoices.Any(x => x.Female > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Female\" column to use it as female voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Female > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Female\" column for at least one voice to use it as female voice.";
			else if (!InstalledVoices.Any(x => x.Male > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Male\" column to use it as male voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Male > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Male\" column for at least one voice to use it as male voice.";
			else if (!InstalledVoices.Any(x => x.Neutral > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Neutral\" column to use it as neutral voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Neutral > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Neutral\" column for at least one voice to use it as neutral voice.";
			VoiceErrorLabel.Visible = error.Length > 0;
			VoiceErrorLabel.Text = error;
		}

		/// <summary>
		/// This event will fire when item is added, removed or change in the list.
		/// </summary>
		void playlist_ListChanged(object sender, ListChangedEventArgs e)
		{
			var changed = (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Status");
			var added = e.ListChangedType == ListChangedType.ItemAdded;
			var deleted = e.ListChangedType == ListChangedType.ItemDeleted;
			var reset = e.ListChangedType == ListChangedType.Reset;
			if (added || deleted || reset)
			{
				BeginInvoke((Action)(() =>
				{
					var items = playlist.ToArray();
					PlayListTabPage.Text = items.Length == 0
						? "Play List"
						: string.Format("Play List: {0}", items.Length);
				}));
			}
			CheckPlayList(added, changed);
		}

		void CheckPlayList(bool added, bool changed)
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
							// Takes WAV bytes witout header.
							WavPlayer.ChangeAudioDevice(Properties.Settings.Default.PlaybackDevice);
							var duration = (int)WavPlayer.Load(pitchedItem.StreamData);
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
							// Takes WAV bytes witout header.
							EffectPresetsEditorSoundEffectsControl.Player.ChangeAudioDevice(Properties.Settings.Default.PlaybackDevice);
							EffectPresetsEditorSoundEffectsControl.Player.Load(pitchedItem.WavData, sampleRate, bitsPerSample, channelCount);
							EffectPresetsEditorSoundEffectsControl.Player.Play();
							// Start timer which will reset status to Played
							pitchedItem.StartPlayTimer();
						}
					}
				}
				// If last item finished playing or any item resulted in error then clear then..
				var lastItem = items.LastOrDefault();
				if ((lastItem != null && lastItem.Status == JobStatusType.Played) || (items.Any(x => x.Status == JobStatusType.Error)))
				{
					BeginInvoke((Action)(() =>
					{
						bool groupIsPlaying;
						int itemsLeftToPlay;
						lock (playlistLock) { ClearPlayList(null, out groupIsPlaying, out itemsLeftToPlay); }
					}));
				}
				else
				{
					BeginInvoke((Action)(() =>
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
					}));
				}
			}
		}

		/// <summary>
		/// If group specified then remove only items from the group.
		/// </summary>
		/// <param name="group"></param>
		void ClearPlayList(string group, out bool groupIsPlaying, out int itemsLeftToPlay)
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

		bool threadIsRunning;
		object threadIsRunningLock = new object();

		/// <summary>
		/// Thread which will process all play items and convert XML to WAV bytes.
		/// </summary>
		/// <param name="status"></param>
		void ProcessPlayItems(object status)
		{

			while (true)
			{
				PlayItem item = null;
				lock (threadIsRunningLock)
				{
					lock (playlistLock)
					{
						// Get first incomplete item in the list.
						JobStatusType[] validStates = { JobStatusType.Parsed, JobStatusType.Synthesized };
						item = playlist.FirstOrDefault(x => validStates.Contains(x.Status));
						// If nothing to do then...
						if (item == null || playlist.Any(x => x.Status == JobStatusType.Error))
						{
							// Exit thread.
							threadIsRunning = false;
							return;
						}
					}
				}
				try
				{
					// If XML is available.
					if (item.Status == JobStatusType.Parsed)
					{
						item.Status = JobStatusType.Synthesizing;
						var encoding = System.Text.Encoding.UTF8;
						var synthesize = true;
						FileInfo xmlFi = null;
						FileInfo wavFi = null;
						if (Properties.Settings.Default.CacheDataRead)
						{
							var dir = MainHelper.GetCreateCacheFolder();

							// Look for generalized file first.
							var uniqueName = item.GetUniqueFilePath(true);
							// Get XML file path.
							var xmlFile = string.Format("{0}.xml", uniqueName);
							var xmlFullPath = Path.Combine(dir.FullName, xmlFile);
							xmlFi = new FileInfo(xmlFullPath);
							// If genrealized file do not exists then...
							if (!xmlFi.Exists)
							{
								// Look for normal file.
								uniqueName = item.GetUniqueFilePath(false);
								// Get XML file path.
								xmlFile = string.Format("{0}.xml", uniqueName);
								xmlFullPath = Path.Combine(dir.FullName, xmlFile);
								xmlFi = new FileInfo(xmlFullPath);
							}
							// If xml file was found then...
							if (xmlFi.Exists)
							{
								// Prefer MP3 audio file first (custom recorded file).
								var wavFile = string.Format("{0}.mp3", uniqueName);
								var wavFullPath = Path.Combine(dir.FullName, wavFile);
								wavFi = new FileInfo(wavFullPath);
								if (!wavFi.Exists)
								{
									// Get WAV file path.
									wavFile = string.Format("{0}.wav", uniqueName);
									wavFullPath = Path.Combine(dir.FullName, wavFile);
									wavFi = new FileInfo(wavFullPath);
								}
							}
							// If both files exists then...
							if (xmlFi.Exists && wavFi.Exists)
							{
								using (Stream stream = new FileStream(wavFi.FullName, FileMode.Open, FileAccess.Read))
								{
									// Load existing XML and WAV data into PlayItem.
									var ms = new MemoryStream();
									var ad = new SharpDX.MediaFoundation.AudioDecoder(stream);
									var samples = ad.GetSamples();
									var enumerator = samples.GetEnumerator();
									while (enumerator.MoveNext())
									{
										var sample = enumerator.Current.ToArray();
										ms.Write(sample, 0, sample.Length);
									}
									// Read WAV head.
									item.WavHead = ad.WaveFormat;
									// Read WAV data.
									item.WavData = ms.ToArray();
									item.Duration = (int)ad.Duration.TotalMilliseconds;
								}
								// Load XML.
								item.Xml = System.IO.File.ReadAllText(xmlFi.FullName);
								// Make sure WAV data is not synthesized.
								synthesize = false;
							}
						}
						if (synthesize)
						{
							int bitsPerSample = 0;
							int sampleRate = 0;
							int channelCount = 0;
							Invoke((Action)(() =>
							{
								sampleRate = (int)AudioSampleRateComboBox.SelectedItem;
								bitsPerSample = (int)AudioBitsPerSampleComboBox.SelectedItem;
								channelCount = (int)(AudioChannel)AudioChannelsComboBox.SelectedItem;
							}));
							item.WavData = ConvertSapiXmlToWav(item.Xml, sampleRate, bitsPerSample, channelCount);
							item.WavHead = new SharpDX.Multimedia.WaveFormat(sampleRate, bitsPerSample, channelCount);
							item.Duration = AudioHelper.GetDuration(item.WavData.Length, item.WavHead.SampleRate, item.WavHead.BitsPerSample, item.WavHead.Channels);
							if (Properties.Settings.Default.CacheDataWrite && item.WavData != null)
							{
								// Create directory if not exists.
								if (!xmlFi.Directory.Exists)
									xmlFi.Directory.Create();
								using (Stream stream = new FileStream(wavFi.FullName, FileMode.Create))
								{
									var headBytes = AudioHelper.GetWavHead(item.WavData.Length, sampleRate, bitsPerSample, channelCount);
									// Write WAV head.
									stream.Write(headBytes, 0, headBytes.Length);
									// Write WAV data.
									stream.Write(item.WavData, 0, item.WavData.Length);

								}
								// Write XML.
								System.IO.File.WriteAllText(xmlFi.FullName, item.Xml, encoding);
							}

						}
						item.Status = (item.WavHead == null || item.WavData == null)
							? item.Status = JobStatusType.Error
							: item.Status = JobStatusType.Synthesized;
					}
					if (item.Status == JobStatusType.Synthesized)
					{
						item.Status = JobStatusType.Pitching;
						ApplyPitch(item);
						item.Status = JobStatusType.Pitched;
					}
				}
				catch (Exception ex)
				{
					LastException = ex;
					item.Status = JobStatusType.Error;
					// Exit thread.
					threadIsRunning = false;
					return;
				}
			}
		}

		void ApplyPitch(PlayItem item)
		{
			// Get info about the WAV.
			int sampleRate = item.WavHead.SampleRate;
			int bitsPerSample = item.WavHead.BitsPerSample;
			int channelCount = item.WavHead.Channels;
			// Get info about effects and pitch.
			bool applyEffects = false;
			float pitchShift = 1.0F;
			Invoke((Action)(() =>
			{
				applyEffects = EffectPresetsEditorSoundEffectsControl.GeneralCheckBox.Checked;
				pitchShift = ((float)EffectPresetsEditorSoundEffectsControl.GeneralPitchTrackBar.Value / 100F);

			}));
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
			if (applyEffects)
			{
				token = new CancellationTokenSource();
				// This part could take long time.
				bytes = EffectsGeneral.ApplyPitchShift(bytes, channelCount, sampleRate, bitsPerSample, pitchShift, token);
				// If pitch shift was canceled then...
				if (token.IsCancellationRequested) return;
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

		Exception _LastException;
		Exception LastException
		{
			get { return _LastException; }
			set
			{
				_LastException = value;
				if (Disposing || IsDisposed || !IsHandleCreated)
				{
					var errorText = ((Exception)_LastException).ToString();
					System.IO.File.WriteAllText("JocysCom.TextToSpeech.Monitor.Error2.txt", errorText);
				}
				else
				{
					BeginInvoke((Action)(() =>
					{
						ErrorStatusLabel.Text = value == null ? "" : MainHelper.CropText(value.Message, 64);
						ErrorStatusLabel.Visible = value != null;

					}));
				}
			}
		}

		private void VoicesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[AgeColumn.Name].Index)
			{
				if (voice.Age.ToString() == "NotSet") e.Value = "...";
			}
			e.CellStyle.ForeColor = voice.Enabled
				? VoicesDataGridView.DefaultCellStyle.ForeColor
				: System.Drawing.SystemColors.ControlDark;
			e.CellStyle.SelectionBackColor = voice.Enabled
			 ? VoicesDataGridView.DefaultCellStyle.SelectionBackColor
			 : System.Drawing.SystemColors.ControlDark;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (IsDesignMode) return;
			SettingsFile.Current.Load();
			SettingsManager.Current.Acronyms.Load();
			LastException = null;
			VoicesDataGridView.AutoGenerateColumns = false;
			MessagesDataGridView.AutoGenerateColumns = false;
			EffectsPresetsDataGridView.AutoGenerateColumns = false;
			MessagesDataGridView.DataSource = WowMessageList;
			// Enable double buffering to make redraw faster.
			typeof(DataGridView).InvokeMember("DoubleBuffered",
			BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
			null, VoicesDataGridView, new object[] { true });
			resetBuffer();
			InitializeSpeech();
			ValidateList();
			InstalledVoices.ListChanged += InstalledVoices_ListChanged;
			if (MonitorPortCheckBox.Checked)
			{
				StartNetworkMonitor();
				ProgramComboBox.Enabled = false;
			}
			UpdateClipboardMonitor();
			// Load "JocysCom.TextToSpeech.Monitor.rtf" file
			var stream = MainHelper.GetResource("JocysCom.TextToSpeech.Monitor.rtf");
			var sr = new StreamReader(stream);
			AboutRichTextBox.Rtf = sr.ReadToEnd();
			sr.Close();
			InitWatcher();
			ResetHelpToDefault();
		}

		//Make the recognizer ready
		SpeechRecognitionEngine recognitionEngine = null;

		// Set voice.
		void SelectVoice(string name, string language, VoiceGender gender)
		{
			int popularity;
			InstalledVoiceEx[] choice;
			// Get only enabled voices.
			var data = InstalledVoices.Where(x => x.Enabled).ToArray();
			InstalledVoiceEx voice = null;

			// Set voice if only gender value is submitted ("Male", "Female", "Neutral").           
			if (string.IsNullOrEmpty(name))
			{
				// Select first most popular voice in "FemaleColumn", "MaleColumn" or "NeutralColumn".
				// Male.
				if (gender == VoiceGender.Male)
				{
					if (string.IsNullOrEmpty(language))
					{
						voice = data.OrderByDescending(x => x.Male).FirstOrDefault();
					}
					else
					{
						voice = data.OrderByDescending(x => x.Male).Where(x => x.Language == language).FirstOrDefault();
					}
					popularity = voice == null ? 0 : voice.Male;
				}
				// Female.
				else if (gender == VoiceGender.Female)
				{
					if (string.IsNullOrEmpty(language))
					{
						voice = data.OrderByDescending(x => x.Female).FirstOrDefault();
					}
					else
					{
						voice = data.OrderByDescending(x => x.Female).Where(x => x.Language == language).FirstOrDefault();
					}
					popularity = voice == null ? 0 : voice.Female;
				}
				// Neutral.
				else
				{
					if (string.IsNullOrEmpty(language))
					{
						voice = data.OrderByDescending(x => x.Neutral).FirstOrDefault();
					}
					else
					{
						voice = data.OrderByDescending(x => x.Neutral).Where(x => x.Language == language).FirstOrDefault();
					}
					popularity = voice == null ? 0 : voice.Neutral;
				}

				if (string.IsNullOrEmpty(language))
				{
					if (popularity == 0) MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" column. Set popularity value to 100 ( normal usage ) or 101 ( normal usage / favourite ) for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
				}
				else
				{
					if (popularity == 0) MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" column with \"Language\" value \"{1}\". Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column with \"Language\" value \"{1}\".", gender, language);
				}
			}
			// Select voice if name and gender values are submitted... ("IVONA 2 Amy") or ("Marshal McBride" and "Male", "Female" or "Neutral").
			else
			{
				// Select specific voice if it exists ("IVONA 2 Amy").
				voice = data.FirstOrDefault(x => x.Name == name);

				// If voice was not found then... generate voice number and assign voice ("Marshal McBride" and "Male", "Female" or "Neutral").
				if (voice == null)
				{
					// Select enabled (with value higher than 0) voices.
					if (string.IsNullOrEmpty(language))
					{
						if (gender == VoiceGender.Male) choice = data.Where(x => x.Male > 0).ToArray();
						else if (gender == VoiceGender.Female) choice = data.Where(x => x.Female > 0).ToArray();
						else choice = data.Where(x => x.Neutral > 0).ToArray();
						if (choice.Length == 0)
						{
							MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" column. Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
						}
					}
					else
					{
						if (gender == VoiceGender.Male) choice = data.Where(x => x.Male > 0).Where(x => x.Language == language).ToArray();
						else if (gender == VoiceGender.Female) choice = data.Where(x => x.Female > 0).Where(x => x.Language == language).ToArray();
						else choice = data.Where(x => x.Neutral > 0).Where(x => x.Language == language).ToArray();

						if (choice.Length == 0)
						{
							if (gender == VoiceGender.Male) choice = data.Where(x => x.Male > 0).ToArray();
							else if (gender == VoiceGender.Female) choice = data.Where(x => x.Female > 0).ToArray();
							else choice = data.Where(x => x.Neutral > 0).ToArray();
							MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" column with \"Language\" value \"{1}\". Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column with \"Language\" value \"{1}\".", gender, language);
						}
					}

					// If nothing to choose from then try all.
					if (choice.Length == 0)
					{
						choice = data.ToArray();
					}
					if (choice.Length == 0) return;
					// Generate number for selecting voice.
					var number = MainHelper.GetNumber(0, choice.Count() - 1, "name", name);
					voice = choice[number];
				}

			}

			foreach (DataGridViewRow row in VoicesDataGridView.Rows)
			{
				if (row.DataBoundItem.Equals(voice))
				{
					row.Selected = true;
					VoicesDataGridView.FirstDisplayedCell = row.Cells[0];
					break;
				}
			}
		}

		InstalledVoiceEx SelectedVoice
		{
			get
			{
				var selectedItem = VoicesDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
				if (selectedItem == null) return null;
				return (InstalledVoiceEx)selectedItem.DataBoundItem;
			}
		}

		void StopPlayer(string group = null)
		{
			bool groupIsPlaying;
			int itemsLeftToPlay;
			lock (playlistLock) { ClearPlayList(group, out groupIsPlaying, out itemsLeftToPlay); }
			if (groupIsPlaying || itemsLeftToPlay == 0)
			{
				resetBuffer();
				if (token != null) token.Cancel();
				EffectPresetsEditorSoundEffectsControl.Player.Stop();
				WavPlayer.Stop();
			}
			if (itemsLeftToPlay > 0)
			{
				CheckPlayList(false, true);
			}
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			StopPlayer();
		}

		private void TextXmlTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			var en = (TextXmlTabControl.SelectedTab != SapiTabPage);
			RateMinComboBox.Enabled = en;
			RateMaxComboBox.Enabled = en;
			PitchMinComboBox.Enabled = en;
			PitchMaxComboBox.Enabled = en;
			_Volume = VolumeTrackBar.Value;
			VolumeTrackBar.Enabled = en;
			VoicesDataGridView.Enabled = en;
			VoicesDataGridView.DefaultCellStyle.SelectionBackColor = en
				? System.Drawing.SystemColors.Highlight
				: System.Drawing.SystemColors.ControlDark;
			//Fill SandBox Tab if it is empty
			if (string.IsNullOrEmpty(SandBoxTextBox.Text))
			{
				SandBoxTextBox.Text = "<message command=\"Play\" language=\"809\" name=\"Marshal McBride\" gender=\"Male\" effect=\"Humanoid\" group=\"Quest\" pitch=\"0\" rate=\"1\" volume=\"100\"><part>Test text to speech. [comment]Test text to speech.[/comment]</part></message>";
			}

			//Fill SAPI Tab
			if (string.IsNullOrEmpty(IncomingTextTextBox.Text))
			{
				SapiTextBox.Text = ConvertTextToSapiXml("Test text to speech.");
			}
			else
			{
				var blocks = AddTextToPlaylist(ProgramComboBox.Text, IncomingTextTextBox.Text, false, "TextBox");
				SapiTextBox.Text = string.Join("\r\n\r\n", blocks.Select(x => x.Xml));
			}
		}

		private void MessagesClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WowMessageList.Clear();
		}

		private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var preset = EffectsPreset.NewPreset();
			refreshPresets();
		}

		void refreshPresets()
		{
			var presets = EffectsPreset.GetPresets();
			EffectsPresetsDataGridView.DataSource = presets;
		}

		void SelectEffectsPreset(string name)
		{
			foreach (DataGridViewRow row in EffectsPresetsDataGridView.Rows)
			{
				var preset = (EffectsPreset)row.DataBoundItem;
				if (preset.Name == name)
				{
					row.Selected = true;
					EffectsPresetsDataGridView.FirstDisplayedCell = row.Cells[0];
					break;
				}
			}
		}

		private void EffectsPresetsDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			var row = EffectsPresetsDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
			EffectsPreset preset = null;
			if (row != null)
			{
				preset = (EffectsPreset)row.DataBoundItem;
				EffectPresetsEditorSoundEffectsControl.LoadPresetIntoForm(preset);
			}
			EffectsPresetsEditorTabPage.Text = (preset == null)
				? "Effects Presets Editor"
				: string.Format("Effects Presets Editor: {0}", preset.Name);
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			refreshPresets();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
				if (token != null)
				{
					token.Cancel();
					token.Dispose();
				}
				DisposeRecognitionEngine();
			}
			base.Dispose(disposing);
		}

		void DisposeRecognitionEngine()
		{
			if (recognitionEngine != null)
			{
				recognitionEngine.SpeechRecognized -= recognitionEngine_SpeechRecognized;
				recognitionEngine.SpeechRecognitionRejected -= recognitionEngine_SpeechRecognitionRejected;
				recognitionEngine.Dispose();
				recognitionEngine = null;
			}
		}

		private void RecognizeButton_Click(object sender, EventArgs e)
		{
			DisposeRecognitionEngine();
			recognitionEngine = new SpeechRecognitionEngine(); //SelectedVoice.Culture
															   //Load grammar
			Choices sentences = new Choices();
			sentences.Add(new string[] { "Yes" });
			sentences.Add(new string[] { "No" });
			GrammarBuilder gBuilder = new GrammarBuilder(sentences);
			Grammar g = new Grammar(gBuilder);
			recognitionEngine.LoadGrammar(g);
			//Add a handler
			recognitionEngine.SpeechRecognitionRejected += recognitionEngine_SpeechRecognitionRejected;
			recognitionEngine.SpeechRecognized += recognitionEngine_SpeechRecognized;
			recognitionEngine.SetInputToDefaultAudioDevice();
			recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
		}


		void recognitionEngine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
		{
			string text = "";
			foreach (RecognizedWordUnit wordUnit in e.Result.Words)
			{
				text += "Rejected: " + wordUnit.Text + ", " + wordUnit.LexicalForm + ", " + wordUnit.Pronunciation + "\r\n";
			}
			IncomingTextTextBox.Text += text;
		}

		void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			string text = "";
			foreach (RecognizedWordUnit wordUnit in e.Result.Words)
			{
				text += "Recognized: " + wordUnit.Text + ", " + wordUnit.LexicalForm + ", " + wordUnit.Pronunciation + "\r\n";
			}
			IncomingTextTextBox.Text += text;
		}

		private void aboutControl_Load(object sender, EventArgs e)
		{
		}

		// Open URL Links in AboutRichTextBox (About.rtf) when LinkClicked.
		private void AboutRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}

		private void VoicesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
			{
				var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
				voice.Enabled = !voice.Enabled;
				VoicesDataGridView.Invalidate();
			}
			if (e.ColumnIndex == grid.Columns[FemaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[MaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[NeutralColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
		}

		private void EffectPresetsEditorSoundEffectsControl_Load(object sender, EventArgs e)
		{

		}

		public void ResetHelpToDefault()
		{
			MainHelpLabel.Text = "Please download this tool only from trustworthy sources. Make sure that this tool is always signed by verified publisher ( Jocys.com ) with signature issued by trusted certificate authority.";
		}

		// ToolTip Main
		private void MouseLeave_MainHelpLabel(object sender, EventArgs e)
		{
			ResetHelpToDefault();
		}

		// ToolTip MouseHover

		private void RateLabel_MouseHover(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Voice rate ( speed ). Default minimum value is 1. Default maximum value is 1.";
		}

		private void MouseHover_RateMin(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Minimum voice rate ( speed ). Default value is 1.";
		}

		private void MouseHover_RateMax(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Maximum voice rate ( speed ). Default value is 1.";
		}

		private void PitchLabel_MouseHover(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Voice pitch. Default minimum value is 0. Default maximum value is 0.";
		}

		private void MouseHover_PitchMin(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Minimum voice pitch. Default value is 0.";
		}

		private void MouseHover_PitchMax(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Maximum voice pitch. Default value is 0.";
		}

		private void MouseHover_AudioSampleRate(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Audio sample rate. Default value is 22050.";
		}

		private void MouseHover_AudioBitsPerSample(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Audio bits per sample. Default value is 16.";
		}

		private void MouseHover_AudioChannels(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Audio channels. Default value is Mono.";
		}

		private void MouseHover_EffectPresets(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Configuration of each preset is stored in separate \"PresetName.preset.xml\" file in \"Presets\" folder.";
		}

		private void MouseHover_Voices(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Use voices from this list as \"Male\", \"Female\" or \"Neutral\". Popularity values are from 0 ( don’t use ) ... 50 ( reduced usage ) ... 100 ( normal usage ). Voice with highest value will be selected, if \"name\" is not submitted.";
		}

		private void MouseHover_SandBox(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "In \"SandBox\" tab you can create and test your messages.";
		}

		private void MouseHover_IncomingGroupBox(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Incoming values have priority over values set in \"Pitch [ min - max ]\", \"Rate [ min - max ]\", \"Default Gender:\" fields and with \"Volume:\" slider. If you will submit specific values, Monitor will use them instead of local setup.";
		}

		private void MouseHover_GenderComboBox(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "This gender will be used if gender value is not submitted or submitted value is not \"Male\", \"Female\" or \"Neutral\".";
		}

		private void MouseHover_MonitorClipboardComboBox(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "[ Disabled ] - clipboard monitor is disabled. [ For <message> tags ] - will read clipboard text between <message><part>...</part></message> tags only. [ For all text ] - will read all clipboard text.";
		}

		private void MouseHover_PortNumericUpDown(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Port number. Default value is 3724 ( World of Warcraft ).";
		}

		private void MonitorPortCheckBox_MouseEnter(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Disable or enable monitoring. Uncheck to edit.";
		}

		// Volume TrackBar value changed
		private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
		{
			VolumeTextBox.Text = VolumeTrackBar.Value.ToString() + "%";
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			UpdateWebBrowser.Navigate("http://www.jocys.com/files/updates/JocysCom.TextToSpeech.Monitor.html");
		}

		#region Clipboard Monitor

		/// <summary>
		/// Sent when the contents of the clipboard have changed.
		/// </summary>
		private const int WM_CLIPBOARDUPDATE = 0x031D;

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_CLIPBOARDUPDATE)
			{
				IDataObject iData = Clipboard.GetDataObject();      // Clipboard's data.

				/* Depending on the clipboard's current data format we can process the data differently.
				 * Feel free to add more checks if you want to process more formats. */
				if (iData.GetDataPresent(DataFormats.Text))
				{
					string text = (string)iData.GetData(DataFormats.Text);

					if (MonitorClipboardComboBox.SelectedIndex == 2 && !text.Contains("<message")) text = "<message command=\"Play\"><part>" + text + "</part></message>";
					if (string.IsNullOrEmpty(text) || !text.Contains("<message")) return;
					var voiceItem = (VoiceListItem)Activator.CreateInstance(MonitorItem.GetType());
					voiceItem.Load(text);
					addVoiceListItem(voiceItem);
					// do something with it
				}
				else if (iData.GetDataPresent(DataFormats.Bitmap))
				{
					//Bitmap image = (Bitmap)iData.GetData(DataFormats.Bitmap);
					// do something with it
				}
			}
		}

		private void MonitorClipboardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateClipboardMonitor();
		}

		bool MonitoringClipboard;
		object MonitoringClipboardLock = new object();

		void UpdateClipboardMonitor()
		{
			if (MonitorClipboardComboBox.SelectedIndex > 0)
			{
				lock (MonitoringClipboardLock)
				{
					// If not monitoring clipboard then...
					if (!MonitoringClipboard)
					{
						// Add form window to the clipboard's format listener list.
						MonitoringClipboard = NativeMethods.AddClipboardFormatListener(this.Handle);
						// if failed then...
						if (!MonitoringClipboard)
						{
							BeginInvoke((Action)delegate ()
							{
								// Set drop down to disabled.
								MonitorClipboardComboBox.SelectedIndex = 0;
							});
						}
					}
				}
			}
			else
			{
				lock (MonitoringClipboardLock)
				{
					if (MonitoringClipboard)
					{
						// Remove our window from the clipboard's format listener list.
						NativeMethods.RemoveClipboardFormatListener(this.Handle);
					}
				}
			}

		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (IsDesignMode) return;
			lock (MonitoringClipboardLock)
			{
				if (MonitoringClipboard)
				{
					// Remove our window from the clipboard's format listener list.
					NativeMethods.RemoveClipboardFormatListener(this.Handle);
				}
			}
			StopNetworkMonitor();
			DisposeWatcher();
			SaveSettings();
			// Save settings
			var name = Properties.Settings.Default.ProgramComboBoxText;
			Properties.Settings.Default.Save();
		}


		#endregion

		#region Load/Save Settings

		public void SaveSettings()
		{
			if (InstalledVoices == null) return;
			var xml = Serializer.SerializeToXmlString(InstalledVoices);
			Properties.Settings.Default.VoicesData = xml;
			SettingsFile.Current.Save();
			SettingsManager.Current.Save();
			//Properties.Settings.Default.Save();
		}

		public void LoadSettings(InstalledVoiceEx[] voices)
		{
			var xml = Properties.Settings.Default.VoicesData;
			InstalledVoiceEx[] savedVoices = null;
			if (!string.IsNullOrEmpty(xml))
			{
				try { savedVoices = Serializer.DeserializeFromXmlString<InstalledVoiceEx[]>(xml); }
				catch (Exception) { }
			}
			if (savedVoices == null) savedVoices = new InstalledVoiceEx[0];
			var newVoices = new List<InstalledVoiceEx>();
			var oldVoices = new List<InstalledVoiceEx>();
			foreach (var voice in voices)
			{
				var savedVoice = savedVoices.FirstOrDefault(x => x.Name == voice.Name && x.Gender == voice.Gender);
				if (savedVoice == null)
				{
					newVoices.Add(voice);
				}
				else
				{
					oldVoices.Add(voice);
					voice.Enabled = savedVoice.Enabled;
					voice.Female = savedVoice.Female;
					voice.Male = savedVoice.Male;
					voice.Neutral = savedVoice.Neutral;
				}
			}
			// If new voices added then...
			if (newVoices.Count > 0)
			{
				var maleIvonaFound = voices.Any(x => x.Name.StartsWith("IVONA") && x.Gender == VoiceGender.Male);
				var femaleIvonaFound = voices.Any(x => x.Name.StartsWith("IVONA") && x.Gender == VoiceGender.Female);
				foreach (var newVoice in newVoices)
				{
					// If new voice is Microsoft then...
					if (newVoice.Name.StartsWith("Microsoft"))
					{
						if (newVoice.Gender == VoiceGender.Male && maleIvonaFound) newVoice.Enabled = false;
						if (newVoice.Gender == VoiceGender.Female && femaleIvonaFound) newVoice.Enabled = false;
					}
				}
				var firstVoiceVoice = newVoices.First();
				// If list doesn't have female voices then use first new voice.
				if (!voices.Any(x => x.Female > 0)) firstVoiceVoice.Female = InstalledVoiceEx.MaxVoice;
				// If list doesn't have male voices then use first new voice.
				if (!voices.Any(x => x.Male > 0)) firstVoiceVoice.Male = InstalledVoiceEx.MaxVoice;
				// If list doesn't have neutral voices then use first voice.
				if (!voices.Any(x => x.Neutral > 0))
				{
					var neutralVoices = voices.Where(x => x.Gender == VoiceGender.Neutral);
					foreach (var neutralVoice in neutralVoices) neutralVoice.Neutral = InstalledVoiceEx.MaxVoice;
					if (neutralVoices.Count() == 0)
					{
						var maleVoices = voices.Where(x => x.Gender == VoiceGender.Male);
						foreach (var maleVoice in maleVoices) maleVoice.Neutral = InstalledVoiceEx.MaxVoice;
						if (maleVoices.Count() == 0)
						{
							var femaleVoices = voices.Where(x => x.Gender == VoiceGender.Female);
							foreach (var femaleVoice in femaleVoices) femaleVoice.Neutral = InstalledVoiceEx.MaxVoice;
						}
					}
				}
			}
		}
		#endregion

		private void VoicesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//if (e.RowIndex == -1) return;
			//var grid = (DataGridView)sender;
			//var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			e.Cancel = true;
		}

		private void MonitorPortCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateMonitor();
		}

		void UpdateMonitor()
		{
			ProgramComboBox.Enabled = !MonitorPortCheckBox.Checked;
			if (MonitorPortCheckBox.Checked)
			{
				StartNetworkMonitor();
			}
			else
			{
				StopNetworkMonitor();
			}
		}

		#region Intro Sounds

		string[] GetIntroSoundNames()
		{
			var prefix = ".Audio.";
			var assembly = Assembly.GetExecutingAssembly();
			var names = assembly.GetManifestResourceNames().Where(x => x.Contains(prefix)).ToArray();
			names = names.Select(x => x.Substring(x.IndexOf(prefix) + prefix.Length).Replace(".wav", "")).ToArray();
			return names;
		}

		Stream GetIntroSound(string name)
		{
			if (string.IsNullOrEmpty(name)) return null;
			var suffix = (".Audio." + name + ".wav").ToLower();
			var assembly = Assembly.GetExecutingAssembly();
			var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.ToLower().EndsWith(suffix));
			return fullResourceName == null ? null : assembly.GetManifestResourceStream(fullResourceName);
		}

		void AddIntroSoundToPlayList(string text, string group, Stream stream)
		{
			var item = new PlayItem(this)
			{
				Text = text,
				WavData = new byte[0],
				StreamData = stream,
				Group = group,
				Status = JobStatusType.Pitched,
			};
			lock (playlistLock) { playlist.Add(item); }
		}

		void PlayCurrentIntroSond()
		{
			var intro = (string)DefaultIntroSoundComboBox.SelectedItem;
			var stream = GetIntroSound(intro);
			if (stream != null)
			{
				var player = new AudioPlayer(Handle);
				player.ChangeAudioDevice(Properties.Settings.Default.PlaybackDevice);
				player.Load(stream);
				player.Play();
			}
		}

		private void DefaultIntroSoundComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			PlayCurrentIntroSond();
		}

		#endregion

		#region Process Monitor

		ManagementEventWatcher startWatch;
		ManagementEventWatcher stopWatch;

		void InitWatcher()
		{
			startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
			startWatch.EventArrived += StartWatch_EventArrived;
			startWatch.Start();
			stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
			stopWatch.EventArrived += StopWatch_EventArrived;
			stopWatch.Start();
			CheckProcessStatus();
		}

		string GetRunningProcessName()
		{
			var mi = MonitorItem;
			if (mi == null)
			{
				return null;
			}
			var names = mi.Process.Select(x => x.ToLower()).ToArray();
			string wmiQueryString = "SELECT ExecutablePath FROM Win32_Process WHERE ExecutablePath <> Null";
			var paths = new List<string>();
			using (var searcher = new ManagementObjectSearcher(wmiQueryString))
			{
				using (var results = searcher.Get())
				{
					var mos = results.Cast<ManagementObject>().ToList();
					foreach (var mo in mos)
					{
						if (mo != null)
						{
							var path = (string)mo["ExecutablePath"];
							var name = Path.GetFileName(path).ToLower();
							paths.Add(name);
							if (names.Contains(name))
							{
								return name;
							}
						}
					}
				}
			}
			return null;
		}

		void CheckProcessStatus()
		{
			var name = GetRunningProcessName();
			if (string.IsNullOrEmpty(name))
			{
				ProcessStatusLabel.Text = "Process: None";
				//StopNetworkMonitor();
			}
			else
			{
				ProcessStatusLabel.Text = string.Format("{0}: Running", name);
				//StartNetworkMonitor();
			}
			lock (monitorLock)
			{
				SetFilter(MonitorItem);
			}
		}

		void DisposeWatcher()
		{
			if (startWatch != null)
			{
				startWatch.Stop();
				startWatch.Dispose();
				startWatch = null;
			}
			if (stopWatch != null)
			{
				stopWatch.Stop();
				stopWatch.Dispose();
				stopWatch = null;
			}
		}

		private void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
		{
			var name = (string)e.NewEvent.Properties["ProcessName"].Value;
			var item = MonitorItem;
			if (item.Process.Contains(name.ToLower()))
			{
				CheckProcessStatus();
			}
		}

		private void StopWatch_EventArrived(object sender, EventArrivedEventArgs e)
		{
			var name = (string)e.NewEvent.Properties["ProcessName"].Value;
			var item = MonitorItem;
			if (item.Process.Contains(name.ToLower()))
			{
				CheckProcessStatus();
			}
		}

		#endregion


		VoiceListItem MonitorItem;

		private void ProgramComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ProgramComboBox.SelectedIndex > -1)
			{
				var item = (VoiceListItem)ProgramComboBox.SelectedItem;
				MonitorItem = item;
				Properties.Settings.Default.ProgramComboBoxText = item.Name;
			}
		}

		private void FilterStatusLabel_Click(object sender, EventArgs e)
		{
			var filters = string.Join(" and \r\n", LastFilters);
			MessageBox.Show(filters, "Last Filter");
		}

		private void ErrorStatusLabel_Click(object sender, EventArgs e)
		{
			var message = LastException.ToString();
			if (LastException.Data.Contains("Voice"))
			{
				//var voice = (SpeechLib.SpVoice)LastException.Data["Voice"];
				message = "Monitor can't use selected voice.\r\n\r\n" + message;
			}
			MessageBox.Show(message, "Last Exception");
		}
	}
}
