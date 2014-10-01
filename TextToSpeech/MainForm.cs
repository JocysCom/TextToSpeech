using JocysCom.WoW.TextToSpeech.Audio;
using JocysCom.WoW.TextToSpeech.Network;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JocysCom.WoW.TextToSpeech
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			playlist = new BindingList<PlayItem>();
			playlist.ListChanged += playlist_ListChanged;
			PlayListDataGridView.AutoGenerateColumns = false;
			PlayListDataGridView.DataSource = playlist;
            Text = MainHelper.GetProductFullName();
		}

		/// <summary>
		/// This event will fire when item is added, removed or change in the list.
		/// </summary>
		void playlist_ListChanged(object sender, ListChangedEventArgs e)
		{
			var items = playlist.ToArray();
			var added = e.ListChangedType == ListChangedType.ItemAdded;
			var deleted = e.ListChangedType == ListChangedType.ItemDeleted;
			var reset = e.ListChangedType == ListChangedType.Reset;
			var changed = (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "Status");
			if (added || deleted || reset)
			{
				BeginInvoke((Action)(() =>
				{
					PlayListTabPage.Text = items.Length == 0
						? "Play List"
						: string.Format("Play List: {0}", items.Length);
				}));
			}
			// If new item was added or item status changed then...
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
						// Must be outside begin invoke.
						var ms = new MemoryStream(pitchedItem.Data);
						EffectPresetsEditorSoundEffectsControl.LoadSoundFile(ms);
						EffectPresetsEditorSoundEffectsControl.PlaySound();
						// Start timer which will reset status to Played
						pitchedItem.StartPlayTimer();
					}
				}
				// If last item finished playing or any item resulted in error then clear then..
				var lastItem = items.LastOrDefault();
				if ((lastItem != null && lastItem.Status == JobStatusType.Played) || (items.Any(x => x.Status == JobStatusType.Error)))
				{
					BeginInvoke((Action)(() =>
					{
						lock (playlistLock) { ClearPlayList(); }
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

		void ClearPlayList()
		{
			var items = playlist.ToArray();
			foreach (var item in playlist)
			{
				item.Dispose();
			}
			playlist.Clear();
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
						var validStates = new JobStatusType[] { JobStatusType.Parsed, JobStatusType.Synthesized };
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
						item.Data = ConvertSapiXmlToWav(item.Xml);
						if (item.Data == null)
						{
							item.Status = JobStatusType.Error;
						}
						else
						{
							item.Status = JobStatusType.Synthesized;
						}
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
			var ms = new MemoryStream();
			var writer = new System.IO.BinaryWriter(ms);
			int channelCount = 0;
			int sampleRate = 0;
			int bitsPerSample = 0;
			bool applyEffects = false;
			float pitchShift = 1.0F;
			Invoke((Action)(() =>
			{
				channelCount = (int)(AudioChannel)AudioChannelsComboBox.SelectedItem;
				sampleRate = (int)AudioSampleRateComboBox.SelectedItem;
				bitsPerSample = (int)AudioBitsPerSampleComboBox.SelectedItem;
				applyEffects = EffectPresetsEditorSoundEffectsControl.GeneralCheckBox.Checked;
				pitchShift = ((float)EffectPresetsEditorSoundEffectsControl.GeneralPitchTrackBar.Value / 100F);

			}));
			var bytes = item.Data;
			// Add 100 milisecconds at the start.
			var silenceStart = 100;
			// Add 200 milisecconds at the end.
			var silenceEnd = 200;
			var silenceBytes = AudioHelper.GetSilenceByteCount(channelCount, sampleRate, bitsPerSample, silenceStart + silenceEnd);
			AudioHelper.WriteHeader(writer, bytes.Length + silenceBytes, channelCount, sampleRate, bitsPerSample);
			if (applyEffects)
			{
				token = new CancellationTokenSource();
				// This part could take long time.
				bytes = EffectsGeneral.ApplyPitchShift(bytes, channelCount, sampleRate, bitsPerSample, pitchShift, token);
				// If pitch shift was cancelled then...
				if (token.IsCancellationRequested) return;
			}
			// Add silence at the start to make room for effects.
			Audio.AudioHelper.WriteSilenceBytes(writer, channelCount, sampleRate, bitsPerSample, silenceStart);
			writer.Write(bytes);
			// Add silence at the back to make room for effects.
			Audio.AudioHelper.WriteSilenceBytes(writer, channelCount, sampleRate, bitsPerSample, silenceEnd);
			// Add result to playlist.
			item.Data = ms.GetBuffer();
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
				BeginInvoke((Action)(() =>
				{
					ErrorToolStripStatusLabel.Text = value == null ? "" : value.Message;
				}));
			}
		}

		#region Network

		// The socket which monitors all incoming packets.
		private List<Socket> mainSockets = new List<Socket>();
		private byte[] byteData = new byte[0xFFFF];
		// A flag to check if packets are to be monitored or not.
		private bool continueMonitoring = false;

		private void BeginMonitoring()
		{
			var ipAddresses = new List<IPAddress>();
			IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
			if (HosyEntry.AddressList.Length > 0)
			{
				foreach (IPAddress ip in HosyEntry.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork)
					{
						ipAddresses.Add(ip);
					}
				}
			}
			if (ipAddresses.Count == 0)
			{
				MonitoringStateStatusLabel.Text = "Error: No Interface to monitor the packets!";
				return;
			}
			else
			{
				MonitoringStateStatusLabel.Text = string.Format("Monitoring: {0}", string.Join(", ", ipAddresses));
			}
			try
			{
				continueMonitoring = true;
				foreach (var ip in ipAddresses)
				{
					// For sniffing the socket to monitor the packets has to be a raw socket, with the
					// address family being of type internetwork, and protocol being IP
					var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
					//Bind the socket to the selected IP address
					socket.Bind(new IPEndPoint(ip, 0));
					//Set the socket  options: Applies only to IP packets, Set the include the header, option to true.
					socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
					byte[] optionInValue = new byte[4] { 1, 0, 0, 0 };
					// Monitor outgoing packets
					byte[] optionOutValue = new byte[4] { 1, 0, 0, 0 };
					// Socket.IOControl is analogous to the WSAIoctl method of Winsock 2: Equivalent to SIO_RCVALL constant, of Winsock 2
					socket.IOControl(IOControlCode.ReceiveAll, optionInValue, optionOutValue);
					// Start receiving the packets asynchronously.
					socket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), socket);
					mainSockets.Add(socket);
				}
			}
			catch (Exception ex)
			{
				LastException = ex;
			}
		}

		object PacketsStateStatusLabelLock = new object();
		long PacketsCount = 0;

		private void BeginReceive_Callback(IAsyncResult ar)
		{
			try
			{
				Interlocked.Add(ref PacketsCount, 1);
				BeginInvoke((Action)(() =>
				{
					lock (PacketsStateStatusLabelLock)
					{
						PacketsCount++;
						PacketsStateStatusLabel.Text = string.Format("Packets: {0}", PacketsCount);
					}
				}));
				var socket = (Socket)ar.AsyncState;
				SocketError errorCode;
				int received = socket.EndReceive(ar, out errorCode);
				if (errorCode == SocketError.Success)
				{
					//Analyze the bytes received...
					ParseData(byteData, received);
				}
				if (continueMonitoring)
				{
					// Maximum length of an IP datagram is 65,535 bytes
					byteData = new byte[0xFFFF];
					//Another call to BeginReceive so that we continue to receive the incoming packets.
					socket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), socket);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				LastException = ex;
			}
		}

		BindingList<WowListItem> WowMessageList = new BindingList<WowListItem>();
		public const ushort WoWPortNumber = 3724;

		private void ParseData(byte[] byteData, int nReceived)
		{
			// All protocol packets are encapsulated in the IP datagram.
			// Parse IP header and see what protocol data is being carried by it.
			IpHeader ipHeader = new IpHeader(byteData, 0, nReceived);
			// If IP datagram contains carries TCP data then...
			if (ipHeader.Protocol == ProtocolType.Tcp)
			{
				// IPHeader.Data stores the data being carried by the IP datagram.
				TcpHeader tcpHeader = new TcpHeader(ipHeader.Data, 0, ipHeader.Data.Length);
				// If destination port number belongs to World of Warcraft
				if (tcpHeader.DestinationPort == WoWPortNumber)
				{
					var wowItem = new WowListItem(ipHeader, tcpHeader);
					// If data contains voice XML.
					if (wowItem.IsVoiceItem)
					{
						// Thread safe adding of the nodes.
						this.Invoke((Action<WowListItem>)addWowListItem, new object[] { wowItem });
					}
				}
			}
		}

		private void addWowListItem(WowListItem wowItem)
		{
			// Leave maximum 9 items in the list.
			while (WowMessageList.Count > 9)
			{
				WowMessageList.RemoveAt(0);
			}
			// Add new item at the bottom.
			WowMessageList.Add(wowItem);
			ProcessWowMessage(wowItem.VoiceXml);
		}

		private void SnifferForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (continueMonitoring)
			{
				foreach (var socket in mainSockets)
				{
					socket.Close();
				}
			}
		}

		string buffer = "";
        VoiceGender gender;

		void resetBuffer()
		{
			buffer = "";
		}

		void ProcessWowMessage(string text)
		{
			// If message doesn't contain voice then do nothing.
			if (!text.Contains("<voice")) return;
			var v = MainHelper.DeserializeFromXmlString<voice>(text);
            if (v.gender != null) gender = (VoiceGender)Enum.Parse(typeof(VoiceGender), v.gender);
            if (v.volume != null) VolumeNumericUpDown.Value = decimal.Parse(v.volume);
            if (v.effect != null) SelectEffectsPreset(v.effect);
            if (v.pitch != null) PitchNumericUpDown.Value = decimal.Parse(v.pitch);
            if (v.rate != null) RateNumericUpDown.Value = decimal.Parse(v.rate);
            if (v.name != null)
            {
                SelectVoice(v.name, gender);
            }
			switch (v.command)
			{
				case "copy":
					break;
				case "play":
					if (v.parts != null) buffer += string.Join("", v.parts);
					var decodedText = System.Web.HttpUtility.HtmlDecode(buffer);
					TextTextBox.Text = decodedText;
					TextXmlTabControl.SelectedTab = TextTabPage;
                    StopPlayer();
					AddTextToPlaylist(decodedText);
					break;
				case "stop":
					text = "";
					StopPlayer();
                    PitchNumericUpDown.Value = 0;
                    RateNumericUpDown.Value = 0;
                    VolumeNumericUpDown.Value = 100;
					break;
				case "add":
					if (v.parts != null) buffer += string.Join("", v.parts);
					break;
				default:
					break;
			}
		}

		#endregion

		#region Speech

		void InitializeSpeech()
		{
			AudioChannelsComboBox.DataSource = Enum.GetValues(typeof(AudioChannel));
			AudioChannelsComboBox.SelectedItem = AudioChannel.Mono;
			AudioSampleRateComboBox.DataSource = new int[] { 11025, 22050, 44100, 48000 };
            AudioSampleRateComboBox.SelectedItem = 22050;
			AudioBitsPerSampleComboBox.DataSource = new int[] { 8, 16 };
			AudioBitsPerSampleComboBox.SelectedItem = 16;
			// Create synthesizer which will be used to create WAV files from SAPI XML.
			sapiSynthesizer = new SpVoice();
			// Fill grid with voices.
			// Create synthesizer which will be used to create WAV files from SSML XML.
			var ssmlSynthesizer = new SpeechSynthesizer();
			var voices = ssmlSynthesizer.GetInstalledVoices().OrderBy(x => x.VoiceInfo.Culture.Name).ThenBy(x => x.VoiceInfo.Gender).ThenBy(x => x.VoiceInfo.Name).ToArray();
			ssmlSynthesizer.Dispose();
			ssmlSynthesizer = null;
			VoicesDataGridView.DataSource = voices;
			refreshPresets();
		}

		string GetXmlText(string text, VoiceInfo vi, int volume, int pitch, int rate)
		{
			string xml;
			string name = vi.Name;
			var sw = new StringWriter();
			var w = new XmlTextWriter(sw);
			w.Formatting = Formatting.Indented;
			w.WriteStartElement("voice");
			w.WriteAttributeString("required", "name=" + name + ";language=" + vi.Culture.LCID.ToString("X3"));
			w.WriteStartElement("volume");
			w.WriteAttributeString("level", volume.ToString());
			w.WriteStartElement("pitch");
			w.WriteAttributeString("absmiddle", pitch.ToString());
			w.WriteStartElement("rate");
			w.WriteAttributeString("absspeed", rate.ToString());
			w.WriteRaw(text);
			w.WriteEndElement();
			w.WriteEndElement();
			w.WriteEndElement();
			w.WriteEndElement();
			xml = sw.ToString();
			w.Close();
			return xml;
		}

		void AddTextToPlaylist(string text)
		{
			var blocks = MainHelper.SplitText(text);
			foreach (var block in blocks)
			{
				var item = new PlayItem(this)
				{
					Text = block,
					Xml = ConvertTextToSapiXml(block),
					Status = JobStatusType.Parsed,
				};
				lock (playlistLock) { playlist.Add(item); }
			}
		}

		string ConvertTextToSapiXml(string text)
		{
			var vi = SelectedVoice.VoiceInfo;
			var volume = (int)VolumeNumericUpDown.Value;
			var pitch = (int)PitchNumericUpDown.Value;
			var rate = (int)RateNumericUpDown.Value;
			// Split text into blocks.
			return GetXmlText(text, vi, volume, pitch, rate);
		}

		byte[] ConvertSapiXmlToWav(string xml)
		{
			int bitsPerSample = 0;
			int sampleRate = 0;
			int channelCount = 0;
			Invoke((Action)(() =>
			{
				LastException = null;
				channelCount = (int)(AudioChannel)AudioChannelsComboBox.SelectedItem;
				sampleRate = (int)AudioSampleRateComboBox.SelectedItem;
				bitsPerSample = (int)AudioBitsPerSampleComboBox.SelectedItem;
			}));
			SpeechAudioFormatType t = SpeechAudioFormatType.SAFT48kHz16BitMono;
			switch (channelCount)
			{
				case 1: // Mono
					switch (sampleRate)
					{
						case 11025: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT11kHz8BitMono : SpeechAudioFormatType.SAFT11kHz16BitMono; break;
						case 22050: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT22kHz8BitMono : SpeechAudioFormatType.SAFT22kHz16BitMono; break;
						case 44100: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT44kHz8BitMono : SpeechAudioFormatType.SAFT44kHz16BitMono; break;
						case 48000: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT48kHz8BitMono : SpeechAudioFormatType.SAFT48kHz16BitMono; break;
					}
					break;
				case 2: // Stereo
					switch (sampleRate)
					{
						case 11025: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT11kHz8BitStereo : SpeechAudioFormatType.SAFT11kHz16BitStereo; break;
						case 22050: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT22kHz8BitStereo : SpeechAudioFormatType.SAFT22kHz16BitStereo; break;
						case 44100: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT44kHz8BitStereo : SpeechAudioFormatType.SAFT44kHz16BitStereo; break;
						case 48000: t = bitsPerSample == 8 ? SpeechAudioFormatType.SAFT48kHz8BitStereo : SpeechAudioFormatType.SAFT48kHz16BitStereo; break;
					}
					break;
			}
			var stream = new SpMemoryStream();
			stream.Format.Type = t;
			sapiSynthesizer.AudioOutputStream = stream;
			try
			{
				sapiSynthesizer.Speak(xml, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
			}
			catch (Exception ex)
			{
				LastException = ex;
				return null;
			}
			var spStream = (SpMemoryStream)sapiSynthesizer.AudioOutputStream;
			spStream.Seek(0, SpeechStreamSeekPositionType.SSSPTRelativeToStart);
			var bytes = (byte[])(object)spStream.GetData();
			return bytes;
		}

		SpVoice sapiSynthesizer;
		BindingList<PlayItem> playlist;
		object playlistLock = new object();
		CancellationTokenSource token;

		void SpeakButton_Click(object sender, EventArgs e)
		{
			if (TextXmlTabControl.SelectedTab == SapiTabPage)
			{
				var item = new PlayItem(this)
				{
					Xml = SapiTextBox.Text,
					Status = JobStatusType.Parsed,
				};
				lock (playlistLock) { playlist.Add(item); }
			}
			else
			{
				AddTextToPlaylist(TextTextBox.Text);
			}
		}

		void ShowVoice(InstalledVoice voice)
		{
			var s = "";
			if (voice == null)
			{
				VoiceDetailsTabPage.ImageKey = "";
			}
			else
			{
				var vi = voice.VoiceInfo;
				Dictionary<string, object> info = new Dictionary<string, object>();
				info.Add("Name", vi.Name);
				info.Add("ID", vi.Id);
				info.Add("Age", vi.Age);
				info.Add("Gender", vi.Gender);
				info.Add("Culture", vi.Culture);
				info.Add("Enabled", voice.Enabled);
				var audioFormats = vi.SupportedAudioFormats.Select(x => x.EncodingFormat).ToArray();
				if (audioFormats.Length > 0)
				{
					info.Add("AudioFormats", string.Join(", ", audioFormats));
				}
				foreach (string key in vi.AdditionalInfo.Keys)
				{
                    if (info.ContainsKey(key)) continue;
                    var value = string.Format("{0}", vi.AdditionalInfo[key]);
                    if (string.IsNullOrEmpty(value)) continue;
					info.Add(key, value);
				}
				info.Add("Description", vi.Description);
				var lines = info.Select(x => string.Format("{0,-14}{1}", x.Key + ":", x.Value)).ToArray();
				s = string.Join("\r\n", lines);
                VoiceDetailsTabPage.ImageKey = voice.VoiceInfo.Gender == VoiceGender.Female ? "businesswoman.png" : "businessman2.png";
			}
			VoiceDetailsTextBox.Text = s;
		}

		#endregion

		private void VoicesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var voice = (InstalledVoice)grid.Rows[e.RowIndex].DataBoundItem;
			var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[CultureColumn.Name].Index)
			{
				e.Value = voice.VoiceInfo.Culture.Name;
			}
			else if (e.ColumnIndex == grid.Columns[GenderColumn.Name].Index)
			{
				e.Value = voice.VoiceInfo.Gender;
			}
			else if (e.ColumnIndex == grid.Columns[NameColumn.Name].Index)
			{
				e.Value = voice.VoiceInfo.Name;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (DesignMode) return;
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
			BeginMonitoring();
		}

		private void VoicesDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			ShowVoice(SelectedVoice);
		}

		//Make the recognizer ready
		SpeechRecognitionEngine recognitionEngine = null;

		void SelectVoice(string name, VoiceGender gender)
		{
            var data = (InstalledVoice[])VoicesDataGridView.DataSource;
            // Get voice which will be selected.
            var voice = data.FirstOrDefault(x => x.VoiceInfo.Name == name);
            // If voice was not found then...
            if (voice == null)
            {
                // Get alternative.
                var choice = data.Where(x => x.VoiceInfo.Gender == gender).ToArray();
                // If nothing to choose from then try all.
                if (choice.Length == 0) choice = data;
                if (choice.Length == 0)
                {
                    var message = string.Format("Voice '{0}' was not found", name);
                    LastException = new Exception(message);
                }
                else
                {
                    // Generate number for picking voice.
                    var number = MainHelper.GetNumber(0, choice.Count(), "voice", name);
                    voice = choice[number];
                    var message = string.Format("Voice '{0}' was not found. Voice '{1}' was used.", name, voice.VoiceInfo.Name);
                    LastException = new Exception(message);
                }
            }
            else
            {
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
		}

		InstalledVoice SelectedVoice
		{
			get
			{
				var selectedItem = VoicesDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
				if (selectedItem == null) return null;
				return (InstalledVoice)selectedItem.DataBoundItem;
			}
		}

		void StopPlayer()
		{
			lock (playlistLock) { ClearPlayList(); }
			resetBuffer();
			if (token != null) token.Cancel();
			EffectPresetsEditorSoundEffectsControl.StopSound();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			StopPlayer();
		}

		private void MainStatusStrip_Click(object sender, EventArgs e)
		{
			if (LastException != null)
			{
				MessageBox.Show(LastException.ToString(), "Last Exception");
			}
		}

		private void TextXmlTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			var en = (TextXmlTabControl.SelectedTab == TextTabPage);
			RateNumericUpDown.Enabled = en;
			PitchNumericUpDown.Enabled = en;
			VolumeNumericUpDown.Enabled = en;
			VoicesDataGridView.Enabled = en;
			VoicesDataGridView.DefaultCellStyle.SelectionBackColor = en
				? System.Drawing.SystemColors.Highlight
				: System.Drawing.SystemColors.ControlDark;
			if (string.IsNullOrEmpty(SapiTextBox.Text) && !string.IsNullOrEmpty(TextTextBox.Text))
			{
				SapiTextBox.Text = ConvertTextToSapiXml(TextTextBox.Text);
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
			recognitionEngine = new SpeechRecognitionEngine(SelectedVoice.VoiceInfo.Culture);
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
			TextTextBox.Text += text;
		}

		void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			string text = "";
			foreach (RecognizedWordUnit wordUnit in e.Result.Words)
			{
				text += "Recognized: " + wordUnit.Text + ", " + wordUnit.LexicalForm + ", " + wordUnit.Pronunciation + "\r\n";
			}
			TextTextBox.Text += text;
		}


	}
}
