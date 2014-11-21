using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.Network;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JocysCom.TextToSpeech.Monitor
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
            InstalledVoices = new BindingList<InstalledVoiceEx>();
        }

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
                    ErrorToolStripStatusLabel.Visible = value != null;

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

        string _Gender;
        VoiceGender gender;
        int _Pitch;
        int _PitchComment;
        int _Rate;
        int _Volume;
        string buffer = "";

        void resetBuffer()
        {
            buffer = "";
        }

        void ProcessWowMessage(string text)
        {
            // If <voice.
            if (!text.Contains("<voice")) return;
            var v = MainHelper.DeserializeFromXmlString<voice>(text);

            //Set gender. "Male"(1), "Female"(2), "Neutral"(3).
            _Gender = string.IsNullOrEmpty(v.gender) || v.gender != "Male" && v.gender != "Female" && v.gender != "Neutral" ? GenderComboBox.Text : v.gender;
            gender = (VoiceGender)Enum.Parse(typeof(VoiceGender), _Gender);
            IncomingGenderTextBox.Text = string.IsNullOrEmpty(v.gender) ? "" : "gender=\"" + _Gender + "\"";

            // Set voice. ----------------------------------------------------------------------------------------------------
            SelectVoice(v.name, gender);
            IncomingNameTextBox.Text = string.IsNullOrEmpty(v.name) ? "" : "name=\"" + v.name + "\"";

            // Set pitch.
            var pitchIsValid = int.TryParse(v.pitch, out _Pitch);
            if (!pitchIsValid)
            {
                if (int.Parse(PitchMinComboBox.Text) > int.Parse(PitchMaxComboBox.Text))
                {
                    _Pitch = MainHelper.GetNumber(int.Parse(PitchMaxComboBox.Text), int.Parse(PitchMinComboBox.Text), "pitch", v.name);
                }
                else
                {
                    _Pitch = MainHelper.GetNumber(int.Parse(PitchMinComboBox.Text), int.Parse(PitchMaxComboBox.Text), "pitch", v.name);
                }
            }
            if (_Pitch < -10) _Pitch = -10;
            if (_Pitch > 10) _Pitch = 10;
            IncomingPitchTextBox.Text = pitchIsValid ? "pitch=\"" + _Pitch + "\"" : "";
            PitchTextBox.Text = pitchIsValid ? PitchTextBox.Text = "" : _Pitch.ToString();

            // Set PitchComment.
            _PitchComment = _Pitch >= 0 ? -8 : 8;

            // Set rate.
            var rateIsValid = int.TryParse(v.rate, out _Rate);
            if (!rateIsValid)
            {
                if (int.Parse(RateMinComboBox.Text) > int.Parse(RateMaxComboBox.Text))
                {
                    _Rate = MainHelper.GetNumber(int.Parse(RateMaxComboBox.Text), int.Parse(RateMinComboBox.Text), "rate", v.name);
                }
                else
                {
                    _Rate = MainHelper.GetNumber(int.Parse(RateMinComboBox.Text), int.Parse(RateMaxComboBox.Text), "rate", v.name);
                }
            }
            if (_Rate < -10) _Rate = -10;
            if (_Rate > 10) _Rate = 10;
            IncomingRateTextBox.Text = rateIsValid ? "rate=\"" + _Rate + "\"" : "";
            RateTextBox.Text = rateIsValid ? RateTextBox.Text = "" : _Rate.ToString();

            // Set effect.
            var effectValue = string.IsNullOrEmpty(v.effect) ? "Default" : v.effect;
            SelectEffectsPreset(effectValue);
            IncomingEffectTextBox.Text = !string.IsNullOrEmpty(v.effect) ? "effect=\"" + v.effect + "\"" : "";

            // Set volume.
            var volumeIsValid = int.TryParse(v.volume, out _Volume);
            if (_Volume < 0) _Volume = 0;
            if (_Volume > 100) _Volume = 100;
            if (!volumeIsValid) _Volume = VolumeTrackBar.Value;
            IncomingVolumeTextBox.Text = volumeIsValid ? "volume=\"" + _Volume + "\"" : "";

            // Set command.
            IncomingCommandTextBox.Text = (string.IsNullOrEmpty(v.command)) ? "command value was not submited!" : "command=\"" + v.command + "\"";
            if (string.IsNullOrEmpty(v.command)) return;

            // commands.
            switch (v.command.ToLower())
            {
                case "copy":
                    break;
                case "play":
                    if (v.parts != null) buffer += string.Join("", v.parts);
                    var decodedText = System.Web.HttpUtility.HtmlDecode(buffer);
                    IncomingTextTextBox.Text = decodedText;
                    //TextXmlTabControl.SelectedTab = TextTabPage;
                    StopPlayer();
                    AddTextToPlaylist(decodedText);
                    break;
                case "stop":
                    text = "";
                    StopPlayer();
                    IncomingRateTextBox.Text = "";
                    IncomingPitchTextBox.Text = "";
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

        BindingList<InstalledVoiceEx> InstalledVoices;

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
            InstalledVoice[] voices = ssmlSynthesizer.GetInstalledVoices().OrderBy(x => x.VoiceInfo.Culture.Name).ThenBy(x => x.VoiceInfo.Gender).ThenBy(x => x.VoiceInfo.Name).ToArray();
            var voicesEx = voices.Select(x => new InstalledVoiceEx(x.VoiceInfo)).ToArray();
            LoadSettings(voicesEx);
            foreach (var voiceEx in voicesEx) InstalledVoices.Add(voiceEx);
            ssmlSynthesizer.Dispose();
            ssmlSynthesizer = null;
            VoicesDataGridView.DataSource = InstalledVoices;
            refreshPresets();
        }

        string GetXmlText(string text, InstalledVoiceEx vi, int volume, int pitch, int rate)
        {
            string xml;
            string name = vi.Name;
            var sw = new StringWriter();
            var w = new XmlTextWriter(sw);
            w.Formatting = Formatting.Indented;
            w.WriteStartElement("voice");
            w.WriteAttributeString("required", "name=" + name + ";language=" + vi.CultureLCID.ToString("X3"));
            w.WriteStartElement("volume");
            w.WriteAttributeString("level", volume.ToString());
            w.WriteStartElement("pitch");
            w.WriteAttributeString("absmiddle", pitch.ToString());
            w.WriteStartElement("rate");
            w.WriteAttributeString("absspeed", rate.ToString());

            //// Replace [comment] tags to SAPI.
            //text = text.Replace("[comment]", "<pitch absmiddle=\"" + _PitchComment.ToString() + "\">").ToString();
            //text = text.Replace("[/comment]", "</pitch>").ToString();

            w.WriteRaw(text);
            w.WriteEndElement();
            w.WriteEndElement();
            w.WriteEndElement();
            w.WriteEndElement();
            xml = sw.ToString();
            w.Close();
            return xml;
        }

        // Demonstrates SetText, ContainsText, and GetText. 
        public String SwapClipboardHtmlText(String replacementHtmlText)
        {
            String returnHtmlText = null;
            if (Clipboard.ContainsText(TextDataFormat.Html))
            {
                returnHtmlText = Clipboard.GetText(TextDataFormat.Html);
                Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
            }
            return returnHtmlText;
        }

        void AddTextToPlaylist(string text)
        {

            //SwapClipboardHtmlText(text);


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
            var vi = SelectedVoice;
            // Split text into blocks.

            // Replace [comment] tags to SAPI.
            text = text.Replace("[comment]", "<pitch absmiddle=\"" + _PitchComment.ToString() + "\" />").ToString();
            text = text.Replace("[/comment]", "<pitch absmiddle=\"" + _Pitch.ToString() + "\" />").ToString().ToString();

            return GetXmlText(text, vi, _Volume, _Pitch, _Rate);
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
            else if (TextXmlTabControl.SelectedTab == SandBoxTabPage)
            {
                var text = SandBoxTextBox.Text;
                if (string.IsNullOrEmpty(text) || !text.Contains("<voice")) return;
                var wowItem = new WowListItem(text);
                addWowListItem(wowItem);
            }
            else
            {
                AddTextToPlaylist(IncomingTextTextBox.Text);
            }
        }

        //void ShowVoice(InstalledVoice voice)
        //{
        //    var s = "";
        //    if (voice == null)
        //    {
        //        VoiceDetailsTabPage.ImageKey = "";
        //    }
        //    else
        //    {
        //        var vi = voice.VoiceInfo;
        //        Dictionary<string, object> info = new Dictionary<string, object>();
        //        info.Add("Name", vi.Name);
        //        info.Add("ID", vi.Id);
        //        info.Add("Age", vi.Age);
        //        info.Add("Gender", vi.Gender);
        //        info.Add("Culture", vi.Culture);
        //        info.Add("Enabled", voice.Enabled);
        //        var audioFormats = vi.SupportedAudioFormats.Select(x => x.EncodingFormat).ToArray();
        //        if (audioFormats.Length > 0)
        //        {
        //            info.Add("AudioFormats", string.Join(", ", audioFormats));
        //        }
        //        foreach (string key in vi.AdditionalInfo.Keys)
        //        {
        //            if (info.ContainsKey(key)) continue;
        //            var value = string.Format("{0}", vi.AdditionalInfo[key]);
        //            if (string.IsNullOrEmpty(value)) continue;
        //            info.Add(key, value);
        //        }
        //        info.Add("Description", vi.Description);
        //        var lines = info.Select(x => string.Format("{0,-14}{1}", x.Key + ":", x.Value)).ToArray();
        //        s = string.Join("\r\n", lines);
        //        VoiceDetailsTabPage.ImageKey = voice.VoiceInfo.Gender == VoiceGender.Female ? "businesswoman.png" : "businessman2.png";
        //    }
        //    VoiceDetailsTextBox.Text = s;
        //}

        #endregion

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
            ValidateList();
            InstalledVoices.ListChanged += InstalledVoices_ListChanged;
            BeginMonitoring();
            UpdateClipboardMonitor();
            // Load "JocysCom.TextToSpeech.Monitor.rtf" file
            var stream = MainHelper.GetResource("JocysCom.TextToSpeech.Monitor.rtf");
            var sr = new StreamReader(stream);
            AboutRichTextBox.Rtf = sr.ReadToEnd();
            sr.Close();
        }

        //Make the recognizer ready
        SpeechRecognitionEngine recognitionEngine = null;

        // Set voice.
        void SelectVoice(string name, VoiceGender gender)
        {
            int popularity;
            InstalledVoiceEx[] choice;
            // Get only enabled voices.
            var data = InstalledVoices.Where(x => x.Enabled).ToArray();
            InstalledVoiceEx voice = null;

            // Set voice if only gender value is submited ("Male", "Female", "Neutral").           
            if (string.IsNullOrEmpty(name))
            {
                // Select first most popular voice in "FemaleColumn", "MaleColumn" or "NeutralColumn".
                if (gender == VoiceGender.Male)
                {
                    voice = data.OrderByDescending(x => x.Male).FirstOrDefault();
                    popularity = voice.Male;
                }
                else if (gender == VoiceGender.Female)
                {
                    voice = data.OrderByDescending(x => x.Female).FirstOrDefault();
                    popularity = voice.Female;
                }
                else
                {
                    voice = data.OrderByDescending(x => x.Neutral).FirstOrDefault();
                    popularity = voice.Neutral;
                }
                if (popularity == 0) MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" category ( column ). Set popularity value to 100 ( normal usage ) or 101 ( normal usage / default - favourite )  for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
            }
            // Select voice if name and gender values are submited... ("IVONA 2 Amy") or ("Marshal McBride" and "Male", "Female" or "Neutral").
            else
            {
                // Select specific voice if it exists ("IVONA 2 Amy").
                voice = data.FirstOrDefault(x => x.Name == name);

                // If voice was not found then... generate voice number and assign voice ("Marshal McBride" and "Male", "Female" or "Neutral").
                if (voice == null)
                {
                    // Select enabled (with value heigher than 0) voices.
                    if (gender == VoiceGender.Male) choice = data.Where(x => x.Male > 0).ToArray();
                    else if (gender == VoiceGender.Female) choice = data.Where(x => x.Female > 0).ToArray();
                    else choice = data.Where(x => x.Neutral > 0).ToArray();

                    // If nothing to choose from then try all.
                    if (choice.Length == 0)
                    {
                        MainHelpLabel.Text = string.Format("There are no voices enabled in \"{0}\" category ( column ). Set popularity value to 100 ( normal usage ) or 101 ( normal usage / default - favourite )  for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
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
            var en = (TextXmlTabControl.SelectedTab != SapiTabPage);
            RateMinComboBox.Enabled = en;
            RateMaxComboBox.Enabled = en;
            PitchMinComboBox.Enabled = en;
            PitchMaxComboBox.Enabled = en;
            VolumeTrackBar.Enabled = en;
            VoicesDataGridView.Enabled = en;
            VoicesDataGridView.DefaultCellStyle.SelectionBackColor = en
                ? System.Drawing.SystemColors.Highlight
                : System.Drawing.SystemColors.ControlDark;
            //Fill SandBox Tab if it is empty
            if (string.IsNullOrEmpty(SandBoxTextBox.Text))
            {
                SandBoxTextBox.Text = "<voice name=\"Marshal McBride\" gender=\"Male\" pitch=\"-5\" rate=\"1\" effect=\"Humanoid\" volume=\"100\" command=\"Play\"><part>Test text to speech. [comment]Test text to speech.[/comment]</part></voice>";
            }
            //Fill SAPI Tab
            if (string.IsNullOrEmpty(IncomingTextTextBox.Text))
            {
                SapiTextBox.Text = ConvertTextToSapiXml("Test text to speech.");
            }
            else
            {
                SapiTextBox.Text = ConvertTextToSapiXml(IncomingTextTextBox.Text);
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

        //Tooltip Main
        private void MouseLeave_MainHelpLabel(object sender, EventArgs e)
        {
            MainHelpLabel.Text = "Please download this tool only from trustworthy sources. Make sure that this tool is always signed by verified publisher ( Jocys.com ) with signature issue by trusted certificate authority.";
        }

        //Tooltip MouseHover
        private void MouseHover_RateMin(object sender, EventArgs e)
        {
            MainHelpLabel.Text = "Minimum voice rate ( speed ). Default value is 0.";
        }

        private void MouseHover_RateMax(object sender, EventArgs e)
        {
            MainHelpLabel.Text = "Maximum voice rate ( speed ). Default value is 2.";
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
            MainHelpLabel.Text = "Incoming values have priority. If you will submit specific values, Monitor will use them instead of local setup.";
        }

        private void MouseHover_GenderComboBox(object sender, EventArgs e)
        {
            MainHelpLabel.Text = "This gender will be used if gender value is... not submitted or submitted value is not \"Male\", \"Female\" or \"Neutral\".";
        }

        private void MouseHover_MonitorClipboardComboBox(object sender, EventArgs e)
        {
            MainHelpLabel.Text = "[ Disabled ] - clipboard monitor is disabled. [ For <voice> tags ] - will read text in clipboard between <voice><part>...</part></voice> tags only. [ For all text ] - will read all text in clipboard.";
        }

        private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            VolumeTextBox.Text = VolumeTrackBar.Value.ToString() + "%";
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

                    if (MonitorClipboardComboBox.SelectedIndex == 2 && !text.Contains("<voice")) text = "<voice command=\"Play\"><part>" + text + "</part></voice>";
                    if (string.IsNullOrEmpty(text) || !text.Contains("<voice")) return;
                    var wowItem = new WowListItem(text);
                    addWowListItem(wowItem);
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

        void UpdateClipboardMonitor()
        {
            if (MonitorClipboardComboBox.SelectedIndex > 0)
            {
                // Add our window to the clipboard's format listener list.
                NativeMethods.AddClipboardFormatListener(this.Handle);
            }
            else
            {
                // Remove our window from the clipboard's format listener list.
                NativeMethods.RemoveClipboardFormatListener(this.Handle);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove our window from the clipboard's format listener list.
            NativeMethods.RemoveClipboardFormatListener(this.Handle);
            SaveSettings();
            // Save settings
            Properties.Settings.Default.Save();
        }


        #endregion

        #region Load/Save Settings

        public void SaveSettings()
        {
            if (InstalledVoices == null) return;
            var xml = MainHelper.SerializeToXmlString(InstalledVoices);
            Properties.Settings.Default.VoicesData = xml;
            //Properties.Settings.Default.Save();
        }

        public void LoadSettings(InstalledVoiceEx[] voices)
        {
            var xml = Properties.Settings.Default.VoicesData;
            if (string.IsNullOrEmpty(xml)) return;
            InstalledVoiceEx[] savedVoices = null;
            try { savedVoices = MainHelper.DeserializeFromXmlString<InstalledVoiceEx[]>(xml); }
            catch (Exception) { }
            if (savedVoices == null) return;
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
                var newVoice = newVoices.FirstOrDefault(x => x.Name.StartsWith("Microsoft"));
                if (newVoice == null) newVoice = newVoices.First();
                // If list doesn't have female voices then use first new voice.
                if (!voices.Any(x => x.Female > 0)) newVoice.Female = InstalledVoiceEx.MaxVoice;
                // If list doesn't have male voices then use first new voice.
                if (!voices.Any(x => x.Male > 0)) newVoice.Male = InstalledVoiceEx.MaxVoice;
                // If list doesn't have neutral voices then use first voice.
                if (!voices.Any(x => x.Neutral > 0)) newVoice.Neutral = InstalledVoiceEx.MaxVoice;
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

    }
}
