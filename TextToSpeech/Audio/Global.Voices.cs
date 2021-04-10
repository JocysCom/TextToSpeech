using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		#region Installed Voices

		public static BindingList<InstalledVoiceEx> InstalledVoices { get; set; }

		public static BindingList<InstalledVoiceEx> LocalVoices { get; set; }
		public static BindingList<InstalledVoiceEx> AmazonNeuralVoices { get; set; }
		public static BindingList<InstalledVoiceEx> AmazonStandardVoices { get; set; }

		public static InstalledVoiceEx SelectedVoice { get { return _SelectedVoice; } set { _SelectedVoice = value; } }
		private static InstalledVoiceEx _SelectedVoice;

		public static string SelectedEffect { get { return _SelectedEffect; } set { _SelectedEffect = value; } }
		private static string _SelectedEffect;

		public static string ValidateInstalledVoices()
		{
			var iv = InstalledVoices;
			string error = "";
			if (iv.Count == 0)
				error = "No voices were found";
			else if (!iv.Any(x => x.Female > 0))
				error = "Please set popularity value higher than 0 for at least one voice in \"Female\" column to use it as female voice ( recommended value: 100 ).";
			else if (!iv.Any(x => x.Female > 0 && x.Enabled))
				error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Female\" column for at least one voice to use it as female voice.";
			else if (!iv.Any(x => x.Male > 0))
				error = "Please set popularity value higher than 0 for at least one voice in \"Male\" column to use it as male voice ( recommended value: 100 ).";
			else if (!iv.Any(x => x.Male > 0 && x.Enabled))
				error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Male\" column for at least one voice to use it as male voice.";
			else if (!iv.Any(x => x.Neutral > 0))
				error = "Please set popularity value higher than 0 for at least one voice in \"Neutral\" column to use it as neutral voice ( recommended value: 100 ).";
			else if (!iv.Any(x => x.Neutral > 0 && x.Enabled))
				error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Neutral\" column for at least one voice to use it as neutral voice.";
			return error;
		}

		#endregion

		#region Save and Load Settings

		public static void SaveSettings()
		{
			if (InstalledVoices == null)
				return;
			// Check if settings are writable.
			//var path = SettingsFile.Current.FolderPath;
			//var rights = FileSystemRights.Write | FileSystemRights.Modify;
			//var hasRights = JocysCom.ClassLibrary.Security.PermissionHelper.HasRights(path, rights);
			//DialogResult result = DialogResult.OK;
			//if (!hasRights)
			//{
			//	var caption = string.Format("Folder Access Denied - {0}", path);
			//	var text = "Old settings were written with administrator permissions.\r\n";
			//	text += "You'll need to provide administrator permissions to fix access and save settings.";
			//	var form = new MessageBoxForm();
			//	form.StartPosition = FormStartPosition.CenterParent;
			//	result = form.ShowForm(text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			//	form.Dispose();
			//	if (result == DialogResult.OK)
			//		Program.RunElevated(AdminCommand.FixProgramSettingsPermissions);
			//}
			try
			{
				var xml = Serializer.SerializeToXmlString(Global.InstalledVoices);
				SettingsManager.Options.VoicesData = xml;
				SettingsFile.Current.Save();
				SettingsManager.Current.Save();
			}
			catch (Exception ex)
			{
				var form2 = new MessageBoxForm();
				form2.StartPosition = FormStartPosition.CenterParent;
				form2.ShowForm(ex.ToString(), ex.Message);
				form2.Dispose();
			}
		}

		public static void LoadSettings()
		{
			var xml = SettingsManager.Options.VoicesData;
			InstalledVoiceEx[] savedVoices = null;
			if (!string.IsNullOrEmpty(xml))
			{
				try
				{ savedVoices = Serializer.DeserializeFromXmlString<InstalledVoiceEx[]>(xml); }
				catch (Exception) { }
			}
			if (savedVoices == null)
				savedVoices = new InstalledVoiceEx[0];
			foreach (var voice in savedVoices)
			{
				// If voice is not missing information then...
				if (!string.IsNullOrEmpty(voice.SourceKeys))
					InstalledVoices.Add(voice);
			}
		}

		public static void ImportVoices(BindingList<InstalledVoiceEx> toVoices, List<InstalledVoiceEx> fromVoices)
		{
			if (toVoices == null)
				throw new ArgumentNullException(nameof(toVoices));
			if (fromVoices == null)
				throw new ArgumentNullException(nameof(fromVoices));
			var newVoices = new List<InstalledVoiceEx>();
			var oldVoices = new List<InstalledVoiceEx>();
			foreach (var fromVoice in fromVoices)
			{
				// Find existing voice.
				var voice = toVoices.FirstOrDefault(x => x.IsSameVoice(fromVoice));
				if (voice == null)
				{
					//fromVoice.Enabled = true;
					newVoices.Add(fromVoice);
					toVoices.Add(fromVoice);
				}
				else
				{
					oldVoices.Add(fromVoice);
					fromVoice.Enabled = voice.Enabled;
					fromVoice.Female = voice.Female;
					fromVoice.Male = voice.Male;
					fromVoice.Neutral = voice.Neutral;
				}
			}
			// If new voices added then...
			if (newVoices.Count > 0)
			{
				// reorder list 
				var newOrder = toVoices.OrderBy(x => x.CultureName).ThenBy(x => x.Name).ThenBy(x => x.Gender).ToArray();
				toVoices.Clear();
				foreach (var item in newOrder)
					toVoices.Add(item);

				var maleIvonaFound = toVoices.Any(x => x.Name.StartsWith("IVONA") && x.Gender == VoiceGender.Male);
				var femaleIvonaFound = toVoices.Any(x => x.Name.StartsWith("IVONA") && x.Gender == VoiceGender.Female);
				foreach (var newVoice in newVoices)
				{
					// If new voice is Microsoft then...
					if (newVoice.Name.StartsWith("Microsoft"))
					{
						if (newVoice.Gender == VoiceGender.Male && maleIvonaFound)
							newVoice.Enabled = false;
						if (newVoice.Gender == VoiceGender.Female && femaleIvonaFound)
							newVoice.Enabled = false;
					}
				}
				var firstVoiceVoice = newVoices.First();
				// If list doesn't have female voices then use first new voice.
				if (!toVoices.Any(x => x.Female > 0))
					firstVoiceVoice.Female = InstalledVoiceEx.MaxVoice;
				// If list doesn't have male voices then use first new voice.
				if (!toVoices.Any(x => x.Male > 0))
					firstVoiceVoice.Male = InstalledVoiceEx.MaxVoice;
				// If list doesn't have neutral voices then use first voice.
				if (!toVoices.Any(x => x.Neutral > 0))
				{
					var neutralVoices = toVoices.Where(x => x.Gender == VoiceGender.Neutral);
					foreach (var neutralVoice in neutralVoices)
						neutralVoice.Neutral = InstalledVoiceEx.MaxVoice;
					if (neutralVoices.Count() == 0)
					{
						var maleVoices = toVoices.Where(x => x.Gender == VoiceGender.Male);
						foreach (var maleVoice in maleVoices)
							maleVoice.Neutral = InstalledVoiceEx.MaxVoice;
						if (maleVoices.Count() == 0)
						{
							var femaleVoices = toVoices.Where(x => x.Gender == VoiceGender.Female);
							foreach (var femaleVoice in femaleVoices)
								femaleVoice.Neutral = InstalledVoiceEx.MaxVoice;
						}
					}
				}
			}
		}

		#endregion


		static string GetSsmlXml(string text, InstalledVoiceEx vi, int volume, int pitch, int rate, bool isComment)
		{
			// <speak>
			//     <amazon:breath duration="long" volume="x-loud"/> <prosody rate="120%"> <prosody volume="loud"> 
			//     Wow! <amazon:breath duration="long" volume="loud"/> </prosody> That was quite fast <amazon:breath 
			//     duration="medium" volume="x-loud"/>. I almost beat my personal best time on this track. </prosody>
			// </speak>;
			var query = System.Web.HttpUtility.ParseQueryString(vi.SourceKeys ?? "");
			var isNeural = query[InstalledVoiceEx._KeyEngine] == Amazon.Polly.Engine.Neural;
			// Amazon Tags: https://docs.aws.amazon.com/polly/latest/dg/supportedtags.html		
			var isAmazon = vi.Source == VoiceSource.Amazon;
			string xml;
			string name = vi.Name;
			var sw = new StringWriter();
			var w = new XmlTextWriter(sw);
			w.Formatting = Formatting.Indented;
			w.WriteStartElement("speak");
			w.WriteAttributeString("version", "1.0");
			w.WriteAttributeString("xmlns", "http://www.w3.org/2001/10/synthesis");
			w.WriteAttributeString("xml:lang", vi.CultureName);
			// <amazon:auto-breaths>
			//if (isAmazon && !isNeural)
			//	w.WriteStartElement("amazon:auto-breaths");
			// <amazon:domain name="news">
			// Note: "News" effect supported by Neural Matthew or Joanna voices only.
			//if (isAmazon && isNeural && vi.Gender == VoiceGender.Neutral)
			//{
			//	w.WriteStartElement("amazon:domain");
			//	w.WriteAttributeString("name", "news");
			//}
			//w.WriteStartElement("lang");
			//w.WriteAttributeString("xml:lang", vi.Language);
			// Modify: Pitch.
			string pitchString = null;
			if (!SettingsManager.Options.ModifyLocallyPitch && !isNeural && pitch != 0)
			{
				if (isAmazon)
					pitchString = string.Format("{0:+0;-0;0}%", pitch * 10);
				else
					pitchString = string.Format("{0:+0;-0;0}", pitch);
			}
			// Modify: Rate.
			string rateString = null;
			if (!SettingsManager.Options.ModifyLocallyRate && rate != 0)
			{
				// Convert -10 0 +10 to 50% 100% 400%
				rateString = string.Format("{0:+0;-0;0}%", rate * 10);
			}
			// Neural voices do not support pitch.
			string volumeString = null;
			if (!SettingsManager.Options.ModifyLocallyVolume && volume < 100)
			{
				// Convert -10 0 +10 to -100% +100%
				// Convert 0 100 to -10dB 0dB
				if (isAmazon)
					volumeString = string.Format("-{0}db", (100 - volume) / 10);
				else
					volumeString = string.Format("-{0}db", (100 - volume) / 10);
			}
			if (rateString != null || pitchString != null || volumeString != null)
			{
				w.WriteStartElement("prosody");
				if (pitchString != null)
					w.WriteAttributeString("pitch", pitchString);
				if (rateString != null)
					w.WriteAttributeString("rate", rateString);
				if (volumeString != null)
					w.WriteAttributeString("volume", volumeString);
			}
			// Replace acronyms with full values.
			text = SettingsManager.Current.ReplaceAcronyms(text);
			w.WriteRaw(text);
			if (rateString != null || pitchString != null)
				w.WriteEndElement();
			// </amazon:domain>
			//if (isAmazon && isNeural && vi.Gender == VoiceGender.Neutral)
			//	w.WriteEndElement();
			// </amazon:auto-breaths>		
			//if (isAmazon && !isNeural)
			//	w.WriteEndElement();
			w.WriteEndElement();
			xml = sw.ToString();
			w.Close();
			return xml;
		}


		//static string GetSapiXml(string text, InstalledVoiceEx vi, int volume, int pitch, int rate, bool isComment)
		//{
		//	string xml;
		//	string name = vi.Name;
		//	var sw = new StringWriter();
		//	var w = new XmlTextWriter(sw);
		//	w.Formatting = Formatting.Indented;
		//	w.WriteStartElement("voice");
		//	if (string.IsNullOrEmpty(language) || vi.CultureLCID.ToString("X3") != language)
		//	{
		//		w.WriteAttributeString("required", "name=" + name);
		//	}
		//	else
		//	{
		//		w.WriteAttributeString("required", "name=" + name + ";language=" + language); //+ vi.CultureLCID.ToString("X3"));
		//	}
		//	w.WriteStartElement("volume");
		//	w.WriteAttributeString("level", volume.ToString());
		//	w.WriteStartElement("rate");
		//	w.WriteAttributeString("absspeed", rate.ToString());
		//	w.WriteStartElement("pitch");
		//	w.WriteAttributeString("absmiddle", (isComment ? _PitchComment : pitch).ToString());
		//	// Replace acronyms with full values.
		//	text = SettingsManager.Current.ReplaceAcronyms(text);
		//	w.WriteRaw(text);
		//	w.WriteEndElement();
		//	w.WriteEndElement();
		//	w.WriteEndElement();
		//	w.WriteEndElement();
		//	xml = sw.ToString();
		//	w.Close();
		//	return xml;
		//}

		// Demonstrates SetText, ContainsText, and GetText. 
		public static string SwapClipboardHtmlText(string replacementHtmlText)
		{
			string returnHtmlText = null;
			if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				returnHtmlText = Clipboard.GetText(TextDataFormat.Html);
				Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
			}
			return returnHtmlText;
		}

		public static List<PlayItem> AddTextToPlaylist(ItemPlayOptions ipo, string game, string text, bool addToPlaylist, string voiceGroup,
			// Optional properties for NPC character.
			string name = null,
			string effect = null,
			// Optional propertied for player character
			string playerName = null,
			string playerNameChanged = null,
			string playerClass = null,
			// Keys which will be used to which voice source (Local, Amazon or Google).
			string voiceSourceKeys = null
		)
		{

			// It will take too long to convert large amount of text to WAV data and apply all filters.
			// This function will split text into smaller sentences.
			var cs = "[comment]";
			var ce = "[/comment]";
			var items = new List<PlayItem>();
			// Split sentences if option is enabled.
			var splitItems = SettingsManager.Options.SplitMessageIntoSentences
				? MainHelper.SplitText(text, new string[] { ". ", "! ", "? ", cs, ce })
				: MainHelper.SplitText(text, new string[] { cs, ce });
			var sentences = splitItems.Where(x => (x.Value + x.Key).Trim().Length > 0).ToArray();
			bool isComment = false;
			// Loop trough each sentence.
			for (int i = 0; i < sentences.Length; i++)
			{
				var block = sentences[i];
				// Combine sentence and separator.
				var sentence = block.Value + block.Key.Replace(cs, "").Replace(ce, "") + "";
				if (!string.IsNullOrEmpty(sentence.Trim('\r', '\n', ' ')))
				{
					var item = new PlayItem();
					item.Game = game;
					// Set Player properties
					item.PlayerName = playerName;
					item.PlayerNameChanged = playerNameChanged;
					item.PlayerClass = playerClass;
					// Set NPC properties.
					//string commentator = FindVoiceForNameInTheList("Commentator");
					//name = isComment ? "Microsoft Zira Desktop" : name;
					item.Name = name;
					item.Gender = ipo.Gender;
					item.Effect = effect;
					item.Volume = ipo.Volume;
					// Set data properties.
					item.Status = JobStatusType.Parsed;
					item.IsComment = isComment;
					item.Group = voiceGroup;
					item.VoiceSourceKeys = voiceSourceKeys;
					item.Text = sentence;
					if (SettingsManager.Options.CacheDataGeneralize)
						item.Text = item.GetGeneralizedText();
						item.Xml = ConvertTextToXml(ipo, item.Text, isComment, ipo.Volume);
						items.Add(item);
					if (addToPlaylist)
						lock (playlistLock)
						{ playlist.Add(item); }
				};
				// If comment started.
				if (block.Key == cs)
					isComment = true;
				// If comment ended.
				if (block.Key == ce)
					isComment = false;
			}
			return items;
		}

		public static string ConvertTextToXml(ItemPlayOptions ipo, string text, bool isComment = false, int volume = 100)
		{
			// Apply 'message' volume and playback volume.
			//var vol = (int)(((decimal)volume / 100m) * (decimal)SettingsManager.Options.Volume);
			var xml = GetSsmlXml(text, ipo.Voice, volume, ipo.Pitch, ipo.Rate, isComment);
			return xml;
		}

		/// <summary>
		/// Convert XML to WAV bytes. WAV won't have the header, so you have to add it separately.
		/// </summary>
		static void ConvertXmlToWav(PlayItem item)
		{
			var query = System.Web.HttpUtility.ParseQueryString(item.VoiceSourceKeys ?? "");
			// Default is local.
			var source = VoiceSource.Local;
			Enum.TryParse(query[InstalledVoiceEx._KeySource], true, out source);
			var voiceId = query[InstalledVoiceEx._KeyVoiceId];
			byte[] wavBytes;
			if (source == VoiceSource.Amazon)
			{
				var region = query[InstalledVoiceEx._KeyRegion];
				var engine = query[InstalledVoiceEx._KeyEngine];
				var client = new Voices.AmazonPolly(
					SettingsManager.Options.AmazonAccessKey,
					SettingsManager.Options.AmazonSecretKey,
					region
				);
				wavBytes = client.SynthesizeSpeech(voiceId, item.Xml, Amazon.Polly.OutputFormat.Mp3, engine);
				if (wavBytes != null && wavBytes.Length > 0)
				{
					var pi = DecodeToPlayItem(wavBytes);
					item.WavHead = pi.WavHead;
					item.WavData = pi.WavData;
					item.Duration = pi.Duration;
					pi.Dispose();
				}
			}
			else
			{
				wavBytes = ConvertSsmlXmlToWav(voiceId, item.Xml, item.WavHead);
				if (wavBytes != null && wavBytes.Length > 0)
				{
					item.WavData = wavBytes;
					item.Duration = AudioHelper.GetDuration(wavBytes.Length, item.WavHead.SampleRate, item.WavHead.BitsPerSample, item.WavHead.Channels);
				}
			}
		}


		public static PlayItem DecodeToPlayItem(byte[] audioFileBytes)
		{
			var item = new PlayItem();
			// Audio file bytes into memory stream.
			using (var stream = new MemoryStream(audioFileBytes))
			{
				// Load existing XML and WAV data into PlayItem.
				using (var ms = new MemoryStream())
				{
					// Loading stream into decoder.
					using (var ad = new SharpDX.MediaFoundation.AudioDecoder(stream))
					{
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
				}
			}
			item.Status = JobStatusType.Synthesized;
			return item;
		}


		/// <summary>
		/// Convert XML to WAV bytes. WAV won't have the header, so you have to add it separately.
		/// </summary>
		static byte[] ConvertSsmlXmlToWav(string voiceId, string xml, WaveFormat format)
		{
			using (var ms = new MemoryStream())
			{
				using (var synthesizer = new SpeechSynthesizer())
				{
					//var format = new SpeechAudioFormatInfo(
					if (format != null)
					{
						//var bps = format.BitsPerSample == 8 ? AudioBitsPerSample.Eight : AudioBitsPerSample.Sixteen;
						var blockAlignment = format.BitsPerSample / 8 * format.Channels;
						var averagerBytesPerSecond = format.SampleRate * format.BitsPerSample / 8 * format.Channels;
						var formatInfo = new SpeechAudioFormatInfo(EncodingFormat.Pcm, format.SampleRate, format.BitsPerSample, format.Channels, averagerBytesPerSecond, blockAlignment, new byte[0]);
						// Returns WAV data only.
						synthesizer.SetOutputToAudioStream(ms, formatInfo);
					}
					try
					{
						var voice = synthesizer.GetInstalledVoices().Cast<InstalledVoice>().FirstOrDefault(x => x.VoiceInfo.Id == voiceId);
						synthesizer.SelectVoice(voice.VoiceInfo.Name);
						synthesizer.SpeakSsml(xml);
						return ms.ToArray();
					}
					catch (Exception ex)
					{
						ex.Data.Add("Voice", "voiceName");
						OnEvent(Exception, ex);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Thread which will process all play items and convert XML to WAV bytes.
		/// </summary>
		/// <param name="status"></param>
		static void ProcessPlayItems(object status)
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
						DirectoryInfo dir = null;
						string uniqueName = null;
						// XML file location.
						string xmlFile = null;
						string xmlFullPath = null;
						FileInfo xmlFi = null;
						// WAV file location.
						string wavFile = null;
						string wavFullPath = null;
						FileInfo wavFi = null;
						if (SettingsManager.Options.CacheDataWrite || SettingsManager.Options.CacheDataRead || SettingsManager.Options.CacheAudioConvert)
						{
							dir = MainHelper.GetCreateCacheFolder();
							// Look for generalized file first.
							uniqueName = item.GetUniqueFilePath(true);
							// Get XML file path.
							xmlFile = string.Format("{0}.xml", uniqueName);
							xmlFullPath = Path.Combine(dir.FullName, xmlFile);
							xmlFi = new FileInfo(xmlFullPath);
							// Get WAV file path.
							wavFile = string.Format("{0}.wav", uniqueName);
							wavFullPath = Path.Combine(dir.FullName, wavFile);
							wavFi = new FileInfo(wavFullPath);
						}
						if (SettingsManager.Options.CacheDataRead)
						{
							// If generalized file do not exists then...
							if (!xmlFi.Exists)
							{
								// Look for normal file.
								uniqueName = item.GetUniqueFilePath(false);
								// Get XML file path.
								xmlFile = string.Format("{0}.xml", uniqueName);
								xmlFullPath = Path.Combine(dir.FullName, xmlFile);
								xmlFi = new FileInfo(xmlFullPath);
							}
							// Read WAV file by default.
							var audioFi = wavFi;
							// Look for MP3 audio file (custom recorded file).
							var mp3File = string.Format("{0}.mp3", uniqueName);
							var mp3FullPath = Path.Combine(dir.FullName, wavFile);
							var mp3Fi = new FileInfo(wavFullPath);
							// If MP3 audio file was found then...
							if (mp3Fi.Exists && mp3Fi.Length > 0)
							{
								// Prefer to read custon file.
								audioFi = mp3Fi;
							}
							// If both files exists and audio file is valid then...
							if (xmlFi.Exists && audioFi.Exists && audioFi.Length > 0)
							{
								using (Stream stream = new FileStream(audioFi.FullName, FileMode.Open, FileAccess.Read))
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
							item.WavHead = new SharpDX.Multimedia.WaveFormat(
								SettingsManager.Options.AudioSampleRate,
								SettingsManager.Options.AudioBitsPerSample,
								(int)SettingsManager.Options.AudioChannels);
							// WavHead could change.
							ConvertXmlToWav(item);
						}
						if (item.WavData != null)
						{
							var ipo = Global.GetPlayOptions();
							var applyRate = SettingsManager.Options.ModifyLocallyRate && ipo.Rate != 0;
							var applyPitch = SettingsManager.Options.ModifyLocallyPitch && ipo.Pitch != 0;
							var applyVolume = SettingsManager.Options.ModifyLocallyVolume && item.Volume < 100;
							if (applyRate || applyPitch)
							{
								var parameters = new SoundStretch.RunParameters();
								parameters.TempoDelta = GetTempoDeltaFromRate(ipo.Rate);
								parameters.PitchDelta = ipo.Pitch;
								parameters.Speech = true;
								var inStream = new MemoryStream();
								AudioHelper.Write(item, inStream);
								inStream.Position = 0;
								var outStream = new MemoryStream();
								SoundStretch.SoundTouchHelper.Process(inStream, outStream, parameters);
								var outBytes = outStream.ToArray();
								var pi = DecodeToPlayItem(outBytes);
								item.WavHead = pi.WavHead;
								item.WavData = pi.WavData;
								item.Duration = pi.Duration;
								pi.Dispose();
								inStream.Dispose();
								outStream.Dispose();
							}
							if (applyVolume)
							{
								var inStream = new MemoryStream();
								AudioHelper.Write(item, inStream);
								inStream.Position = 0;
								var vol = (float)item.Volume / 100f;
								var outStream = new MemoryStream();
								AudioHelper.ChangeVolume(vol, inStream, outStream);
								var outBytes = outStream.ToArray();
								var pi = DecodeToPlayItem(outBytes);
								item.WavHead = pi.WavHead;
								item.WavData = pi.WavData;
								item.Duration = pi.Duration;
								pi.Dispose();
								inStream.Dispose();
								outStream.Dispose();
							}
							if (SettingsManager.Options.CacheDataWrite || SettingsManager.Options.CacheAudioConvert)
							{
								// Create directory if not exists.
								if (!xmlFi.Directory.Exists)
									xmlFi.Directory.Create();
								if (!xmlFi.Exists)
									// Write XML.
									System.IO.File.WriteAllText(xmlFi.FullName, item.Xml, encoding);
							}
							// If data was synthesized i.e. was not loaded from the file then...
							if (synthesize && SettingsManager.Options.CacheDataWrite)
								AudioHelper.Write(item, wavFi);
							// If must convert data to other formats.
							if (SettingsManager.Options.CacheAudioConvert)
								AudioHelper.Convert(item, wavFi);
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
					OnEvent(Exception, ex);
					item.Status = JobStatusType.Error;
					// Exit thread.
					threadIsRunning = false;
					return;
				}
			}
		}

		/// <summary>
		/// Convert [-10,+10] to [-50,+100] percent.
		/// var deltaString = string.Format("{0:+#\"%\";-#\"%\";", delta);
		/// </summary>
		public static int GetTempoDeltaFromRate(int rate)
		{
			// Increase/Decrease each step by 10%.
			// Convert [-10,+10] to [-61,+159] percent.
			// var delta = ((float)Math.Pow(1.1, rate) - 1f) * 100f -100%
			//
			int delta = 0;
			// Decrease tempo by -5% to -50%
			if (rate < 0)
				delta = rate * 5;
			// Increase tempo by +10% to +100%.
			else if (rate > 0)
				delta = rate * 10;
			return delta;
		}

		/// <summary>Filter voices by language.</summary>
		static InstalledVoiceEx[] FilterVoicesByCulture(InstalledVoiceEx[] voices, CultureInfo culture)
		{
			// Get voice match by name. Example: en-GB.
			var choice = voices.Where(x => x.CultureName == culture.Name).ToArray();
			// If voice not found then get match by language. Example: en
			if (choice.Length > 0)
				return choice;
			choice = voices
				.Where(x => string.Equals(x.Language, culture.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase))
				.ToArray();
			return choice;
		}

		/// <summary>Filter voices by language.</summary>
		static InstalledVoiceEx[] FilterVoicesByGender(InstalledVoiceEx[] voices, MessageGender gender)
		{
			if (gender == MessageGender.Male)
				return voices.Where(x => x.Male > 0).ToArray();
			else if (gender == MessageGender.Female)
				return voices.Where(x => x.Female > 0).ToArray();
			else
				return voices.Where(x => x.Neutral > 0).ToArray();
		}

		/// <summary>Reorder voices by gender.</summary>
		static void OrderVoicesByGender(ref InstalledVoiceEx[] voices, MessageGender gender)
		{
			if (gender == MessageGender.Male)
				voices = voices.OrderByDescending(x => x.Male).ToArray();
			else if (gender == MessageGender.Female)
				voices = voices.OrderByDescending(x => x.Female).ToArray();
			else
				voices = voices.OrderByDescending(x => x.Neutral).ToArray();
		}

		/// <summary>
		/// Select Computer voice by NPC name, language and gender.
		/// </summary>
		/// <param name="name">NPC Name</param>
		/// <param name="language">NPC Language</param>
		/// <param name="gender">NPC Gender</param>
		/// <returns>Computer Voice</returns>
		static InstalledVoiceEx SelectVoice(string name, string language, MessageGender gender)
		{
			// Get only enabled voices.
			var data = InstalledVoices.Where(x => x.Enabled).ToArray();
			InstalledVoiceEx voice = null;
			var culture = Capturing.message.GetCultureInfo(language);
			// Order voices by putting matching gender with highest value first.
			OrderVoicesByGender(ref data, gender);
			var missing = "";
			missing += "There are no voices enabled in \"{0}\" column with value \"{1}\". ";
			missing += "Set popularity value to 100 ( normal usage ) or 101 ( normal usage / favourite ) for at least one voice.";
			// Initial choice will be all enabled voices.
			InstalledVoiceEx[] choice = data;
			InstalledVoiceEx[] tmp;
			// If voice name was supplied then...
			if (!string.IsNullOrEmpty(name))
			{
				// Select voices by name if exists ("IVONA 2 Amy").
				tmp = data.Where(x => string.Equals(x.Name, name, StringComparison.InvariantCulture)).ToArray();
				// If choice available then...
				if (tmp.Length > 0)
					choice = tmp;
				else
					OnHelp(missing, "Name", name);
			}
			// Filter by gender (more important than by culture).
			tmp = FilterVoicesByGender(choice, gender);
			// If choice available then...
			if (tmp.Length > 0)
				choice = tmp;
			else
			{
				OnHelp(missing, "Gender", gender);
				// Order by Male as default.
				OrderVoicesByGender(ref data, MessageGender.Male);
			}
			// If culture supplied.
			if (culture != null)
			{
				tmp = FilterVoicesByCulture(choice, culture);
				// If choice available then...
				if (tmp.Length > 0)
					choice = tmp;
				else
					OnHelp(missing, "Culture", language);
			}
			// If nothing to choose from then...
			if (choice.Length == 0)
				return null;
			// Generate number for selecting voice.
			var number = MainHelper.GetNumber(0, choice.Length - 1, "name", name);
			voice = choice[number];
			if (SelectedVoice != voice)
				OnEvent(VoiceChanged, voice);
			return voice;
		}

		public static CancellationTokenSource token;

	}
}
