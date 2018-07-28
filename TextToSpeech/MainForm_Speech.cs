using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm
	{

		BindingList<InstalledVoiceEx> InstalledVoices;

		void InitializeSpeech()
		{
			AudioChannelsComboBox.DataSource = Enum.GetValues(typeof(AudioChannel));
			AudioChannelsComboBox.SelectedItem = AudioChannel.Mono;
			AudioSampleRateComboBox.DataSource = new int[] { 11025, 22050, 44100, 48000 };
			AudioSampleRateComboBox.SelectedItem = 22050;
			AudioBitsPerSampleComboBox.DataSource = new int[] { 16 };
			AudioBitsPerSampleComboBox.SelectedItem = 16;
			RefreshVoicesGrid();
			refreshPresets();
		}

		public void RefreshVoicesGrid()
		{
			InstalledVoices.Clear();
			// Fill grid with voices.
			// Create synthesizer which will be used to create WAV files from SSML XML.
			var ssmlSynthesizer = new SpeechSynthesizer();
			InstalledVoice[] voices = ssmlSynthesizer.GetInstalledVoices().OrderBy(x => x.VoiceInfo.Culture.Name).ThenBy(x => x.VoiceInfo.Gender).ThenBy(x => x.VoiceInfo.Name).ToArray();
			var voicesEx = voices.Select(x => new InstalledVoiceEx(x.VoiceInfo)).ToArray();
			LoadSettings(voicesEx);
			foreach (var voiceEx in voicesEx) InstalledVoices.Add(voiceEx);
			ssmlSynthesizer.Dispose();
			VoicesDataGridView.DataSource = InstalledVoices;
		}

		string GetXmlText(string text, InstalledVoiceEx vi, int volume, int pitch, int rate, bool isComment)
		{
			string xml;
			string name = vi.Name;
			var sw = new StringWriter();
			var w = new XmlTextWriter(sw);
			w.Formatting = Formatting.Indented;
			w.WriteStartElement("voice");
			if (string.IsNullOrEmpty(language) || vi.CultureLCID.ToString("X3") != language)
			{
				w.WriteAttributeString("required", "name=" + name);
			}
			else
			{
				w.WriteAttributeString("required", "name=" + name + ";language=" + language); //+ vi.CultureLCID.ToString("X3"));
			}
			w.WriteStartElement("volume");
			w.WriteAttributeString("level", volume.ToString());
			w.WriteStartElement("rate");
			w.WriteAttributeString("absspeed", rate.ToString());
			w.WriteStartElement("pitch");
			w.WriteAttributeString("absmiddle", (isComment ? _PitchComment : pitch).ToString());
			// Replace acronyms with full values.
			text = SettingsManager.Current.ReplaceAcronyms(text);
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

		List<PlayItem> AddTextToPlaylist(string game, string text, bool addToPlaylist, string voiceGroup,
			// Optional properties for NPC character.
			string name = null,
			string gender = null,
			string effect = null
		)
		{
			// It will take too long to convert large amount of text to WAV data and apply all filters.
			// This function will split text into smaller sentences.
			var cs = "[comment]";
			var ce = "[/comment]";
			var items = new List<PlayItem>();
			var splitItems = MainHelper.SplitText(text, new string[] { ". ", "! ", "? ", cs, ce });
			var sentences = splitItems.Where(x => (x.Value + x.Key).Trim().Length > 0).ToArray();
			bool comment = false;
			// Loop trough each sentence.
			for (int i = 0; i < sentences.Length; i++)
			{
				var block = sentences[i];
				// Combine sentence and separator.
				var sentence = block.Value + block.Key.Replace(cs, "").Replace(ce, "");
				if (!string.IsNullOrEmpty(sentence))
				{
					var item = new PlayItem(this)
					{
						Game = game,
						// Set NPC properties.
						Name = name,
						Gender = gender,
						Effect = effect,
						// Set data properties.
						Text = sentence,
						Xml = ConvertTextToSapiXml(sentence, comment),
						Status = JobStatusType.Parsed,
						IsComment = comment,
						Group = voiceGroup,
					};
					items.Add(item);
					if (addToPlaylist) lock (playlistLock) { playlist.Add(item); }
				};
				if (block.Key == cs) comment = true;
				if (block.Key == ce) comment = false;
			}
			return items;
		}

		string ConvertTextToSapiXml(string text, bool isComment = false)
		{
			var vi = SelectedVoice;
			return GetXmlText(text, vi, _Volume, _Pitch, _Rate, isComment);
		}

		/// <summary>
		/// Convert xml to WAV bytes. WAV won't have the header, so you have to add it separatelly.
		/// </summary>
		byte[] ConvertSapiXmlToWav(string xml, int sampleRate, int bitsPerSample, int channelCount)
		{
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
			byte[] bytes;
			var voice = new SpeechLib.SpVoice();
			// Write into memory.
			var stream = new SpeechLib.SpMemoryStream();
			stream.Format.Type = t;
			voice.AudioOutputStream = stream;
			try
			{
				voice.Speak(xml, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
			}
			catch (Exception ex)
			{
				ex.Data.Add("Voice", "voiceName");
				LastException = ex;
				return null;
			}
			var spStream = (SpMemoryStream)voice.AudioOutputStream;
			spStream.Seek(0, SpeechStreamSeekPositionType.SSSPTRelativeToStart);
			bytes = (byte[])(object)spStream.GetData();
			return bytes;
		}

		BindingList<PlayItem> playlist;
		object playlistLock = new object();
		CancellationTokenSource token;

		public void AddMessageToPlay(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("<message"))
				{
					var voiceItem = (VoiceListItem)Activator.CreateInstance(MonitorItem.GetType());
					voiceItem.Load(text);
					addVoiceListItem(voiceItem);
				}
				else
				{
					var item = new PlayItem(this)
					{
						Text = "SAPI XML",
						Xml = text,
						Status = JobStatusType.Parsed,
					};
					lock (playlistLock) { playlist.Add(item); }
				}
			}
		}

		void SpeakButton_Click(object sender, EventArgs e)
		{
			// if [ Formatted SAPI XML Text ] tab selected.
			if (TextXmlTabControl.SelectedTab == SapiTabPage)
			{
				AddMessageToPlay(SapiTextBox.Text);
			}
			// if [ SandBox ] tab selected.
			else if (TextXmlTabControl.SelectedTab == SandBoxTabPage)
			{
				AddMessageToPlay(SandBoxTextBox.Text);
			}
			// if [ Incoming Messages ] tab selected.
			else if (TextXmlTabControl.SelectedTab == MessagesTabPage)
			{
				var gridRow = MessagesDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
				if (gridRow != null)
				{
					var item = (PlugIns.VoiceListItem)gridRow.DataBoundItem;
					ProcessVoiceTextMessage(item.VoiceXml);
				}
			}
			else
			{
				AddTextToPlaylist(ProgramComboBox.Text, IncomingTextTextBox.Text, true, "TextBox");
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

	}
}
