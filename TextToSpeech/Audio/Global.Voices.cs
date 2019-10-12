using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		public static void InitializeSpeech()
		{
			RefreshVoicesGrid();
		}

		#region Installed Voices

		public static BindingList<InstalledVoiceEx> InstalledVoices { get; set; }
		public static InstalledVoiceEx SelectedVoice { get { return _SelectedVoice; } set { _SelectedVoice = value; } }
		static InstalledVoiceEx _SelectedVoice;
		public static string ValidateInstalledVoices()
		{
			string error = "";
			if (InstalledVoices.Count == 0) error = "No voices were found";
			else if (!InstalledVoices.Any(x => x.Female > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Female\" column to use it as female voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Female > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Female\" column for at least one voice to use it as female voice.";
			else if (!InstalledVoices.Any(x => x.Male > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Male\" column to use it as male voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Male > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Male\" column for at least one voice to use it as male voice.";
			else if (!InstalledVoices.Any(x => x.Neutral > 0)) error = "Please set popularity value higher than 0 for at least one voice in \"Neutral\" column to use it as neutral voice ( recommended value: 100 ).";
			else if (!InstalledVoices.Any(x => x.Neutral > 0 && x.Enabled)) error = "Please enable and set popularity value higher than 0 ( recommended value: 100 ) in \"Neutral\" column for at least one voice to use it as neutral voice.";
			return error;
		}

		#endregion

		public static void RefreshVoicesGrid()
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
		}

		public static void LoadSettings(InstalledVoiceEx[] voices)
		{
			var xml = SettingsManager.Options.VoicesData;
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

		static string GetXmlText(string text, InstalledVoiceEx vi, int volume, int pitch, int rate, bool isComment)
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

		public static List<PlayItem> AddTextToPlaylist(string game, string text, bool addToPlaylist, string voiceGroup,
			// Optional properties for NPC character.
			string name = null,
			string gender = null,
			string effect = null,
			int volume = 100,
			// Optional propertied for player character
			string playerName = null,
			string playerNameChanged = null,
			string playerClass = null
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
				var sentence = block.Value + block.Key.Replace(cs, "").Replace(ce, "") + "";
				if (!string.IsNullOrEmpty(sentence.Trim('\r', '\n', ' ')))
				{
					MessageGender _gender;
					Enum.TryParse(gender, out _gender);
					var item = new PlayItem()
					{
						Game = game,
						// Set Player properties
						PlayerName = playerName,
						PlayerNameChanged = playerNameChanged,
						PlayerClass = playerClass,
						// Set NPC properties.
						Name = name,
						Gender = _gender,
						Effect = effect,
						// Set data properties.
						Status = JobStatusType.Parsed,
						IsComment = comment,
						Group = voiceGroup,
					};
					item.Text = sentence;
					if (SettingsManager.Options.CacheDataGeneralize)
					{
						item.Text = item.GetGeneralizedText();
					}
					item.Xml = ConvertTextToSapiXml(item.Text, comment, volume);
					items.Add(item);
					if (addToPlaylist) lock (playlistLock) { playlist.Add(item); }
				};
				if (block.Key == cs) comment = true;
				if (block.Key == ce) comment = false;
			}
			return items;
		}

		public static string ConvertTextToSapiXml(string text, bool isComment = false, int volume = 100)
		{
			var vi = SelectedVoice;
			var vol = (int)(((decimal)volume / 100m) * (decimal)SettingsManager.Options.Volume);
			return GetXmlText(text, vi, vol, _Pitch, _Rate, isComment);
		}

		/// <summary>
		/// Convert xml to WAV bytes. WAV won't have the header, so you have to add it separatelly.
		/// </summary>
		static byte[] ConvertSapiXmlToWav(string xml, int sampleRate, int bitsPerSample, int channelCount)
		{
			var t = SpeechAudioFormatType.SAFT48kHz16BitMono;
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
				OnEvent(Exception, ex);
				return null;
			}
			var spStream = (SpMemoryStream)voice.AudioOutputStream;
			spStream.Seek(0, SpeechStreamSeekPositionType.SSSPTRelativeToStart);
			bytes = (byte[])(object)spStream.GetData();
			return bytes;
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
						FileInfo xmlFi = null;
						FileInfo wavFi = null;
						if (SettingsManager.Options.CacheDataRead)
						{
							var dir = MainHelper.GetCreateCacheFolder();

							// Look for generalized file first.
							var uniqueName = item.GetUniqueFilePath(true);
							// Get XML file path.
							var xmlFile = string.Format("{0}.xml", uniqueName);
							var xmlFullPath = Path.Combine(dir.FullName, xmlFile);
							xmlFi = new FileInfo(xmlFullPath);
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
							var bitsPerSample = SettingsManager.Options.AudioBitsPerSample;
							int sampleRate = SettingsManager.Options.AudioSampleRate;
							int channelCount = (int)SettingsManager.Options.AudioChannels;
							item.WavData = ConvertSapiXmlToWav(item.Xml, sampleRate, bitsPerSample, channelCount);
							item.WavHead = new SharpDX.Multimedia.WaveFormat(sampleRate, bitsPerSample, channelCount);
							item.Duration = AudioHelper.GetDuration(item.WavData.Length, item.WavHead.SampleRate, item.WavHead.BitsPerSample, item.WavHead.Channels);
							if (SettingsManager.Options.CacheDataWrite && item.WavData != null)
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
					OnEvent(Exception, ex);
					item.Status = JobStatusType.Error;
					// Exit thread.
					threadIsRunning = false;
					return;
				}
			}
		}

		/// <summary>Filter voices by language.</summary>
		static InstalledVoiceEx[] FilterVoicesByCulture(InstalledVoiceEx[] voices, CultureInfo culture)
		{
			// Get voice match by name. Example: en-GB.
			var choice = voices.Where(x => x.CultureName == culture.Name).ToArray();
			// If voice not found then get match by language. Example: en
			if (choice != null)
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

		// Set voice.
		static void SelectVoice(string name, string language, MessageGender gender)
		{
			int popularity;
			InstalledVoiceEx[] choice;
			// Get only enabled voices.
			var data = InstalledVoices.Where(x => x.Enabled).ToArray();
			InstalledVoiceEx voice = null;
			var culture = Capturing.message.GetCultureInfo(language);
			// Set voice if only gender value is submitted ("Male", "Female", "Neutral").           
			if (string.IsNullOrEmpty(name))
			{
				// Select first most popular voice in "FemaleColumn", "MaleColumn" or "NeutralColumn".
				OrderVoicesByGender(ref data, gender);
				voice = culture == null
					? data.FirstOrDefault()
					: FilterVoicesByCulture(data, culture).FirstOrDefault();
				popularity = voice == null ? 0 : voice.Male;
				if (string.IsNullOrEmpty(language))
				{
					if (popularity == 0)
						OnHelp("There are no voices enabled in \"{0}\" column. Set popularity value to 100 ( normal usage ) or 101 ( normal usage / favourite ) for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
				}
				else
				{
					if (popularity == 0)
						OnHelp("There are no voices enabled in \"{0}\" column with \"Language\" value \"{1}\". Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column with \"Language\" value \"{1}\".", gender, language);
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
					choice = FilterVoicesByGender(data, gender);
					if (choice.Length == 0)
					{
						OnHelp("There are no voices enabled in \"{0}\" column. Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column, to use it as \"{0}\" voice.", gender);
					}
					// If culture supplied.
					else if (culture != null)
					{
						choice = FilterVoicesByCulture(choice, culture);
						if (choice.Length == 0)
							OnHelp("There are no voices enabled in \"{0}\" column with \"Language\" value \"{1}\". Set popularity value to 100 ( normal usage ) for one voice at least in \"{0}\" column with \"Language\" value \"{1}\".", gender, language);
					}
					// If nothing to choose from then try all.
					if (choice.Length == 0)
						choice = data.ToArray();
					if (choice.Length == 0)
						return;
					// Generate number for selecting voice.
					var number = MainHelper.GetNumber(0, choice.Count() - 1, "name", name);
					voice = choice[number];
				}
			}
			if (SelectedVoice != voice)
				OnEvent(VoiceChanged, voice);
		}

		public static CancellationTokenSource token;

	}
}
