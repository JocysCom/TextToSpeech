using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		static string _Gender;
		static string language;
		static VoiceGender gender;
		static int _Pitch;
		static int _PitchComment;
		static int _Rate;
		public static int _Volume;

		static string _PlayerName;
		static string _PlayerNameChanged;
		static string _PlayerClass;

		public static event EventHandler<EventArgs<message>> ProcessedMessage;
		public static event EventHandler<EventArgs<string>> HelpSuggested;
		public static event EventHandler<EventArgs<sound>> IntroSoundSelected;
		public static event EventHandler<EventArgs<string>> EffectsPresetSelected;
		static void OnHelp(string format, params object[] args)
		{
			var message = args.Length > 0 ? string.Format(format, args) : format;
			OnEvent(HelpSuggested, message);
		}
		public static void ProcessVoiceTextMessage(string text)
		{
			var im = new message();

			// If <message.
			if (!text.Contains("<message"))
				return;
			var v = Serializer.DeserializeFromXmlString<message>(text);
			// Override voice values.
			var name = v.name;
			var overrideVoice = SettingsFile.Current.Defaults.FirstOrDefault(x => x.name == name);
			// if override was found and enabled then...
			if (overrideVoice != null)
				v.UpdateMissingValuesFrom(overrideVoice);

			//Set gender. "Male"(1), "Female"(2), "Neutral"(3).
			_Gender = string.IsNullOrEmpty(v.gender) || v.gender != "Male" && v.gender != "Female" && v.gender != "Neutral" ? SettingsManager.Options.GenderComboBoxText : v.gender;
			var success = Enum.TryParse(_Gender, out gender);
			im.gender = v.gender;

			// Set language. ----------------------------------------------------------------------------------------------------
			language = v.language;
			im.language = v.language;

			// Set voice. ----------------------------------------------------------------------------------------------------
			SelectVoice(v.name, language, gender);
			im.name = v.name;

			// Set pitch.
			var pitchIsValid = int.TryParse(v.pitch, out _Pitch);
			if (!pitchIsValid)
				_Pitch = MainHelper.GetNumber(SettingsManager.Options.PitchMin, SettingsManager.Options.PitchMax, "pitch", v.name);
			if (_Pitch < -10) _Pitch = -10;
			if (_Pitch > 10) _Pitch = 10;
			im.pitch = v.pitch;
			// Set PitchComment.
			_PitchComment = _Pitch >= 0 ? -1 : 1;

			// Set rate.
			var rateIsValid = int.TryParse(v.rate, out _Rate);
			if (!rateIsValid)
				_Rate = MainHelper.GetNumber(SettingsManager.Options.RateMin, SettingsManager.Options.RateMax, "rate", v.name);
			if (_Rate < -10) _Rate = -10;
			if (_Rate > 10) _Rate = 10;
			im.rate = v.rate;

			// Set effect.
			im.effect = string.IsNullOrEmpty(v.effect) ? "Default" : v.effect;
			if (v.command.ToLower() != "save")
				OnEvent(EffectsPresetSelected, im.effect);

			// Set volume.
			var volumeIsValid = int.TryParse(v.volume, out _Volume);
			if (!volumeIsValid)
				_Volume = SettingsManager.Options.Volume;
			if (_Volume < 0) _Volume = 0;
			if (_Volume > 100) _Volume = 100;
			im.volume = v.volume;

			// Set group.
			im.group = string.IsNullOrEmpty(v.group) ? "" : v.group;

			// Set command.
			im.command = v.command;
			if (string.IsNullOrEmpty(v.command))
				return;

			// commands.
			switch (v.command.ToLower())
			{
				case "copy":
					break;
				case "player":
					if (v.name != null)
					{
						var playerNames = v.name.Split(',').Select(x => x.Trim()).ToArray();
						_PlayerName = playerNames.FirstOrDefault();
						_PlayerNameChanged = playerNames.Skip(1).FirstOrDefault();
						_PlayerClass = playerNames.Skip(2).FirstOrDefault();
					}
					break;
				case "add":
					if (v.part != null)
						partsBuffer += v.part;
					break;
				case "sound":
					var groupName = string.IsNullOrEmpty(v.group)
						? SettingsManager.Options.DefaultIntroSoundComboBox ?? ""
						: v.group;
					// Find intro sound.
					var snd = SettingsFile.Current.Sounds.FirstOrDefault(x => x.group.ToUpper() == groupName.ToUpper());
					// If sound was not found then...
					if (snd == null)
					{
						// Add sound to the list.
						snd = new sound();
						snd.group = v.group;
						SettingsFile.Current.Sounds.Add(snd);
					}
					if (snd.enabled)
					{
						// Get WAV name/path.
						var wavToPlay = string.IsNullOrEmpty(snd.file) ? groupName : snd.file;
						wavToPlay = MainHelper.ConvertFromSpecialFoldersPattern(wavToPlay);
						var stream = GetIntroSound(wavToPlay);
						if (stream == null && System.IO.File.Exists(wavToPlay))
							stream = new System.IO.FileStream(wavToPlay, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						OnEvent(IntroSoundSelected, snd);
						if (stream != null)
							AddIntroSoundToPlayList(wavToPlay, v.group, stream);
					}
					break;
				case "play":
					// Add last part to the buffer.
					if (v.part != null)
						partsBuffer += v.part;
					// Decode complete message part.
					im.part = System.Web.HttpUtility.HtmlDecode(partsBuffer);
					// Clean buffer.
					partsBuffer = "";
					// Add silence before message.
					var programName = Program.MonitorItem.Name;
					var silenceIntBefore = (int)SettingsManager.Options.AddSilenceBeforeMessage;
					if (silenceIntBefore > 0)
						AddTextToPlaylist(programName, "<silence msec=\"" + silenceIntBefore.ToString() + "\" />", true, v.group);
					// Add actual message to the playlist
					AddTextToPlaylist(programName, im.part, true, v.group,
						// Supply NCP properties.
						v.name, v.gender, v.effect,
						// Supply Player properties.
						_PlayerName, _PlayerNameChanged, _PlayerClass
					);
					// Add silence after message.
					var silenceIntAfter = (int)SettingsManager.Options.AddSilenceAfterMessage;
					if (silenceIntAfter > 0)
						AddTextToPlaylist(programName, "<silence msec=\"" + silenceIntAfter.ToString() + "\" />", true, v.group);
					break;
				case "stop":
					text = "";
					StopPlayer(v.group);
					im.rate = "";
					im.pitch = "";
					break;
				case "save":
					if (!string.IsNullOrEmpty(v.name))
						SettingsManager.Current.UpsertDefaultsRecord(v);
					break;
				default:
					break;
			}
			OnEvent(ProcessedMessage, im);
		}

	}
}
