using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.ClassLibrary.Win32;
using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.Capturing;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm : Form
	{

		public MainForm()
		{
			Program.TopForm = this;
			ControlsHelper.InitInvokeContext();
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			LoadSettings();
			Audio.Global.InitGlobal(Handle);

			Global.EffectsPlayer.BeforePlay += EffectsPlayer_BeforePlay;
			Global.AddingVoiceListItem += AudioGlobal_AddingVoiceListItem;
			Global.ProcessedMessage += AudioGlobal_ProcessedMessage;
			Global.HelpSuggested += AudioGlobal_HelpSuggested;
			Global.EffectsPresetSelected += Global_EffectsPresetSelected;

			MonitorsEnabledCheckBox.DataBindings.Add(nameof(MonitorsEnabledCheckBox.Checked), SettingsManager.Options, nameof(SettingsManager.Options.MonitorsEnabled));

			Audio.Global.playlist.ListChanged += Playlist_ListChanged;

			PlayListDataGridView.AutoGenerateColumns = false;
			PlayListDataGridView.DataSource = Global.playlist;
			Text = MainHelper.GetProductFullName();
			UpdateLabel.Text = "You are running " + MainHelper.GetProductFullName();
			// Add supported items.
			ProgramComboBox.DataSource = Program.PlugIns;
			ProgramComboBox.DisplayMember = "Name";
			var name = SettingsManager.Options.ProgramComboBoxText;
			if (!string.IsNullOrEmpty(name))
				ProgramComboBox.Text = name;
			// If nothing is selected but list have values.
			if (ProgramComboBox.SelectedIndex == -1 && ProgramComboBox.Items.Count > 0)
			{
				// Select first one.
				ProgramComboBox.SelectedIndex = 0;
			}
			Program._ClipboardMonitor.StatusChanged += _Monitor_StatusChanged;
			Program._NetworkMonitor.StatusChanged += _Monitor_StatusChanged;
			Program._UdpMonitor.StatusChanged += _Monitor_StatusChanged;
			Program._NetworkMonitor.ProcessChanged += _NetworkMonitor_ProcessChanged;
			ControlsHelper.ApplyImageStyle(MessagesTabControl);
		}

		private void _NetworkMonitor_ProcessChanged(object sender, ClassLibrary.EventArgs<string> e)
		{
			ProcessStatusLabel.Text = string.IsNullOrEmpty(e.Data)
				? "Process: None"
				: string.Format("{0}: Running", e.Data);
		}

		private void Global_EffectsPresetSelected(object sender, ClassLibrary.EventArgs<string> e)
		{
			SelectEffectsPreset(e.Data);
		}
		private void AudioGlobal_HelpSuggested(object sender, ClassLibrary.EventArgs<string> e)
		{
			MainHelpLabel.Text = e.Data;
		}
		private void AudioGlobal_ProcessedMessage(object sender, ClassLibrary.EventArgs<Capturing.message> e)
		{
			InPartTextBox.Text = string.Format("{0}", e.Data.part);
			InLanguageTextBox.Text = string.Format("{0}", e.Data.language);
			InNameTextBox.Text = string.Format("{0}", e.Data.name);
			InGenderTextBox.Text = string.Format("{0}", e.Data.gender);
			InEffectTextBox.Text = string.Format("{0}", e.Data.effect);
			InGroupTextBox.Text = string.Format("{0}", e.Data.group);
			InPitchTextBox.Text = string.Format("{0}", e.Data.pitch);
			InRateTextBox.Text = string.Format("{0}", e.Data.rate);
			InVolumeTextBox.Text = string.Format("{0}", e.Data.volume);
			InCommandTextBox.Text = string.Format("{0}", e.Data.command);
			int rate;
			RateTextBox.Text = int.TryParse(e.Data.rate, out rate) ? rate.ToString() : "";
			int pitch;
			PitchTextBox.Text = int.TryParse(e.Data.pitch, out pitch) ? pitch.ToString() : "";
		}
		private void EffectsPlayer_BeforePlay(object sender, EventArgs e)
		{
			var player = (AudioPlayer)sender;
			EffectPresetsEditorSoundEffectsControl.ApplyEffects(player.ApplicationBuffer);
		}

		private void Playlist_ListChanged(object sender, ListChangedEventArgs e)
		{
			var added = e.ListChangedType == ListChangedType.ItemAdded;
			var deleted = e.ListChangedType == ListChangedType.ItemDeleted;
			var reset = e.ListChangedType == ListChangedType.Reset;
			if (added || deleted || reset)
			{
				ControlsHelper.BeginInvoke(() =>
				{
					var count = Audio.Global.playlist.Count;
					PlayListTabPage.Text = count == 0
						? "Play List"
						: string.Format("Play List: {0}", count);
				});
			}
		}

		object PacketsStateStatusLabelLock = new object();
		private void _Monitor_StatusChanged(object sender, Capturing.Monitors.MonitorEventArgs e)
		{
			// Don't update control from two threads at the same time.
			lock (PacketsStateStatusLabelLock)
			{
				if (e.Error != null)
					ErrorStatusLabel.Text = e.Error;
				if (e.Filter != null)
					ErrorStatusLabel.Text = e.Filter;
				if (e.Packets != null)
					ErrorStatusLabel.Text = e.Packets;
				if (e.State != null)
					ErrorStatusLabel.Text = e.State;
			}
		}

		public bool IsDesignMode
		{
			get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
		}

		#region Settings

		void LoadSettings()
		{
			// Monitor Clipboard
			MonitorClipboardComboBox.DataSource = new string[] { "Disabled", "For<message> tags", "For all text" };
			ControlsHelper.SetSelectedItem(MonitorClipboardComboBox, SettingsManager.Options.MonitorClipboardComboBoxText);
			MonitorClipboardComboBox.SelectedIndexChanged += MonitorClipboardComboBox_SelectedIndexChanged;
			// Default Intro sound.
			DefaultIntroSoundComboBox.DataSource = Global.GetIntroSoundNames();
			ControlsHelper.SetSelectedItem(DefaultIntroSoundComboBox, SettingsManager.Options.DefaultIntroSoundComboBox);
			DefaultIntroSoundComboBox.SelectedIndexChanged += DefaultIntroSoundComboBox_SelectedIndexChanged;
			// Audio Channels.
			AudioChannelsComboBox.DataSource = Enum.GetValues(typeof(AudioChannel));
			ControlsHelper.SetSelectedItem(AudioChannelsComboBox, SettingsManager.Options.AudioChannels);
			AudioChannelsComboBox.SelectedIndexChanged += AudioChannelsComboBox_SelectedIndexChanged;
			// Audio Sample Rate.
			AudioSampleRateComboBox.DataSource = new int[] { 11025, 22050, 44100, 48000 };
			ControlsHelper.SetSelectedItem(AudioSampleRateComboBox, SettingsManager.Options.AudioSampleRate);
			AudioSampleRateComboBox.SelectedIndexChanged += AudioSampleRateComboBox_SelectedIndexChanged;
			// Audio Bits Per Sample.
			AudioBitsPerSampleComboBox.DataSource = new int[] { 8, 16 };
			ControlsHelper.SetSelectedItem(AudioBitsPerSampleComboBox, SettingsManager.Options.AudioBitsPerSample);
			AudioBitsPerSampleComboBox.SelectedIndexChanged += AudioBitsPerSampleComboBox_SelectedIndexChanged;
			// Gender.
			GenderComboBox.DataSource = new object[] { MessageGender.Male, MessageGender.Female, MessageGender.Neutral };
			GenderComboBox.SelectedItem = SettingsManager.Options.GenderComboBoxText;
			GenderComboBox.SelectedIndexChanged += GenderComboBox_SelectedIndexChanged;
			// Pitch Min.
			PitchMinComboBox.DataSource = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10 };
			ControlsHelper.SetSelectedItem(PitchMinComboBox, SettingsManager.Options.PitchMin);
			PitchMinComboBox.SelectedIndexChanged += PitchMinComboBox_SelectedIndexChanged;
			// Pitch Max.
			PitchMaxComboBox.DataSource = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10 };
			ControlsHelper.SetSelectedItem(PitchMaxComboBox, SettingsManager.Options.PitchMax);
			PitchMaxComboBox.SelectedIndexChanged += PitchMaxComboBox_SelectedIndexChanged;
			// Rate Min.
			RateMinComboBox.DataSource = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10 };
			ControlsHelper.SetSelectedItem(RateMinComboBox, SettingsManager.Options.RateMin);
			RateMinComboBox.SelectedIndexChanged += RateMinComboBox_SelectedIndexChanged;
			// Rate Max.
			RateMaxComboBox.DataSource = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10 };
			ControlsHelper.SetSelectedItem(RateMaxComboBox, SettingsManager.Options.RateMax);
			RateMaxComboBox.SelectedIndexChanged += RateMaxComboBox_SelectedIndexChanged;
			// Volume.
			VolumeTrackBar.Value = SettingsManager.Options.Volume;
			VolumeTrackBar.ValueChanged += VolumeTrackBar_ValueChanged;
			VolumeTrackBar_ValueChanged(null, null);
		}

		private void MonitorClipboardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.MonitorClipboardComboBoxText = MonitorClipboardComboBox.Text;
			UpdateClipboardMonitor();
		}

		private void DefaultIntroSoundComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.DefaultIntroSoundComboBox = DefaultIntroSoundComboBox.Text;
			Global.PlayCurrentIntroSound();
		}

		private void AudioChannelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.AudioChannels = (AudioChannel)AudioChannelsComboBox.SelectedItem;
		}

		private void AudioSampleRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.AudioSampleRate = (int)AudioSampleRateComboBox.SelectedItem;
		}

		private void AudioBitsPerSampleComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.AudioBitsPerSample = (int)AudioBitsPerSampleComboBox.SelectedItem;
		}

		private void GenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.GenderComboBoxText = (MessageGender)GenderComboBox.SelectedItem;
		}

		private void PitchMinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.PitchMin = (int)PitchMinComboBox.SelectedItem;
			if (SettingsManager.Options.PitchMin > SettingsManager.Options.PitchMax)
				ControlsHelper.SetSelectedItem(PitchMaxComboBox, SettingsManager.Options.PitchMin);
		}

		private void PitchMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.PitchMax = (int)PitchMaxComboBox.SelectedItem;
			if (SettingsManager.Options.PitchMax < SettingsManager.Options.PitchMin)
				ControlsHelper.SetSelectedItem(PitchMinComboBox, SettingsManager.Options.PitchMax);
		}

		private void RateMinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.RateMin = (int)RateMinComboBox.SelectedItem;
			if (SettingsManager.Options.RateMin > SettingsManager.Options.RateMax)
				ControlsHelper.SetSelectedItem(RateMaxComboBox, SettingsManager.Options.RateMin);
		}

		private void RateMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.RateMax = (int)RateMaxComboBox.SelectedItem;
			if (SettingsManager.Options.RateMax < SettingsManager.Options.RateMin)
				ControlsHelper.SetSelectedItem(RateMinComboBox, SettingsManager.Options.RateMax);
		}

		private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.Volume = VolumeTrackBar.Value;
			VolumeLabel.Text = string.Format("{0}% Volume", SettingsManager.Options.Volume);
		}

		#endregion

		Exception _LastException;
		Exception LastException
		{
			get { return _LastException; }
			set
			{
				_LastException = value;
				if (Disposing || IsDisposed || !IsHandleCreated)
				{
					var errorText = _LastException.ToString();
					System.IO.File.WriteAllText("JocysCom.TextToSpeech.Monitor.Error2.txt", errorText);
				}
				else
				{
					ErrorStatusLabel.Text = value == null ? "" : MainHelper.CropText(value.Message, 64);
					ErrorStatusLabel.Visible = value != null;
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (ControlsHelper.IsDesignMode(this))
				return;
			SettingsFile.Current.Load();
			LastException = null;
			MessagesDataGridView.AutoGenerateColumns = false;
			EffectsPresetsDataGridView.AutoGenerateColumns = false;
			MessagesDataGridView.DataSource = MessagesVoiceItems;
			// Load voices from settings.
			Global.LoadSettings();
			refreshPresets();
			VoicesPanel.InitializeVoices();
			if (MonitorsEnabledCheckBox.Checked)
				ProgramComboBox.Enabled = false;
			UpdateClipboardMonitor();
			// Load "JocysCom.TextToSpeech.Monitor.rtf" file
			var stream = MainHelper.GetResource("JocysCom.TextToSpeech.Monitor.rtf");
			var sr = new StreamReader(stream);
			AboutRichTextBox.Rtf = sr.ReadToEnd();
			sr.Close();
			ResetHelpToDefault();
		}

		//Make the recognizer ready
		SpeechRecognitionEngine recognitionEngine = null;

		private void StopButton_Click(object sender, EventArgs e)
		{
			Global.StopPlayer();
		}

		private void TextXmlTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			var en = (TextXmlTabControl.SelectedTab != SsmlXmlTabPage);
			RateMinComboBox.Enabled = en;
			RateMaxComboBox.Enabled = en;
			PitchMinComboBox.Enabled = en;
			PitchMaxComboBox.Enabled = en;
			VolumeTrackBar.Enabled = en;
			VoicesPanel.EnableGrid(en);
			//Fill SandBox Tab if it is empty
			if (string.IsNullOrEmpty(SandBoxTextBox.Text))
			{
				SandBoxTextBox.Text = "<message command=\"Play\" language=\"809\" name=\"Marshal McBride\" gender=\"Male\" effect=\"Humanoid\" group=\"Quest\" pitch=\"0\" rate=\"1\" volume=\"100\"><part>Test text to speech. [comment]Test text to speech.[/comment]</part></message>";
			}
			var ipo = Global.GetPlayOptions();
			//Fill SAPI Tab
			if (string.IsNullOrEmpty(InPartTextBox.Text))
			{

				SapiTextBox.Text = Global.ConvertTextToXml(ipo, "Test text to speech.");
			}
			else
			{
				var blocks = Global.AddTextToPlaylist(ipo, ProgramComboBox.Text, InPartTextBox.Text, false, "TextBox");
				SapiTextBox.Text = string.Join("\r\n\r\n", blocks.Select(x => x.Xml));
			}
		}

		private void MessagesClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessagesVoiceItems.Clear();
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
				DisposeRecognitionEngine();
				Global.DisposeGlobal();
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
			InPartTextBox.Text += text;
		}

		void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			string text = "";
			foreach (RecognizedWordUnit wordUnit in e.Result.Words)
			{
				text += "Recognized: " + wordUnit.Text + ", " + wordUnit.LexicalForm + ", " + wordUnit.Pronunciation + "\r\n";
			}
			InPartTextBox.Text += text;
		}

		private void aboutControl_Load(object sender, EventArgs e)
		{
		}

		// Open URL Links in AboutRichTextBox (About.rtf) when LinkClicked.
		private void AboutRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
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

		private void EnableMonitorsCheckBox_MouseEnter(object sender, EventArgs e)
		{
			MainHelpLabel.Text = "Disable or enable monitoring. Uncheck to edit.";
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

		string _lastData;

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_CLIPBOARDUPDATE)
			{
				// Clipboard's data.
				var iData = Clipboard.GetDataObject();
				/* Depending on the clipboard's current data format we can process the data differently.
				 * Feel free to add more checks if you want to process more formats. */
				if (iData.GetDataPresent(DataFormats.Text))
				{
					var text = (string)iData.GetData(DataFormats.Text);
					// Do not process same data.
					if (text == _lastData)
						return;
					_lastData = text;
					if (MonitorClipboardComboBox.SelectedIndex == 2 && !text.Contains("<message"))
						text = "<message command=\"Play\"><part>" + text + "</part></message>";
					if (string.IsNullOrEmpty(text) || !text.Contains("<message"))
						return;
					var voiceItem = (VoiceListItem)Activator.CreateInstance(Program.MonitorItem.GetType());
					voiceItem.Load(text);
					Global.addVoiceListItem(voiceItem);
					// do something with it
				}
				else if (iData.GetDataPresent(DataFormats.Bitmap))
				{
					//Bitmap image = (Bitmap)iData.GetData(DataFormats.Bitmap);
					// do something with it
				}
			}
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
			Global.SaveSettings();
		}


		#endregion

		private void FilterStatusLabel_Click(object sender, EventArgs e)
		{
			var filters = string.Join(" and \r\n", Program._NetworkMonitor.LastFilters);
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

		private void UpdateTabPage_Click(object sender, EventArgs e)
		{

		}


		void SpeakButton_Click(object sender, EventArgs e)
		{
			// if [ Formatted SAPI XML Text ] tab selected.
			if (TextXmlTabControl.SelectedTab == SsmlXmlTabPage)
			{
				Global.AddMessageToPlay(SapiTextBox.Text);
			}
			// if [ SandBox ] tab selected.
			else if (TextXmlTabControl.SelectedTab == SandBoxTabPage)
			{
				Global.AddMessageToPlay(SandBoxTextBox.Text);
			}
			// if [ Incoming Messages ] tab selected.
			else if (TextXmlTabControl.SelectedTab == MessagesTabPage)
			{
				var gridRow = MessagesDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
				if (gridRow != null)
				{
					var item = (PlugIns.VoiceListItem)gridRow.DataBoundItem;
					Global.ProcessVoiceTextMessage(item.VoiceXml);
				}
			}
			else
			{
				var ipo = Global.GetPlayOptions();
				Global.AddTextToPlaylist(ipo, ProgramComboBox.Text, InPartTextBox.Text, true, "TextBox");
			}
		}

		/// <summary>
		/// Update sandbox automatically if user use mouse to select effect.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EffectsPresetsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			var row = EffectsPresetsDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
			if (row == null)
				return;
			var preset = (EffectsPreset)row.DataBoundItem;
			try
			{
				var message = Serializer.DeserializeFromXmlString<message>(SandBoxTextBox.Text);
				message.effect = preset.Name;
				var messageXml = Serializer.SerializeToXmlString(message, null, true);
				SandBoxTextBox.Text = messageXml;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
