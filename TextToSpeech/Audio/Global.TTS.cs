using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.IO;
using System.Linq;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

		static string language;
		static int _Pitch;
		static int _PitchComment;
		static int _Rate;

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

		static message addMessage;

		public static void ProcessVoiceTextMessage(string text)
		{
			// If <message.
			if (!text.Contains("<message"))
				return;
			var v = Serializer.DeserializeFromXmlString<message>(text);
			// Get default values by NPC or Player name.
			var overrideVoice = SettingsFile.Current.Defaults.FirstOrDefault(x => string.Equals(x.name, v.name, StringComparison.InvariantCultureIgnoreCase));
			// If defaults were found then fill missing values.
			if (overrideVoice != null)
				v.UpdateMissingValuesFrom(overrideVoice);

			// commands.
			var cmd = v.command.ToLower();
			sound snd;
			switch (cmd)
			{
				case MessageCommands.Player:
					if (v.name != null)
					{
						var playerNames = v.name.Split(',').Select(x => x.Trim()).ToArray();
						_PlayerName = playerNames.FirstOrDefault();
						_PlayerNameChanged = playerNames.Skip(1).FirstOrDefault();
						_PlayerClass = playerNames.Skip(2).FirstOrDefault();
					}
					break;
				case MessageCommands.Add:
					if (addMessage == null)
						addMessage = new message();
					addMessage.UpdateMissingAndChangedValuesFrom(v, true);
					break;
				case MessageCommands.Sound:
					var groupName2 = string.IsNullOrEmpty(v.group)
						? SettingsManager.Options.DefaultIntroSoundComboBox ?? ""
						: v.group;
					// Find intro sound.
					snd = SettingsFile.Current.Sounds.FirstOrDefault(x => x.group.ToUpper() == groupName2.ToUpper());
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
						var wavToPlay = string.IsNullOrEmpty(snd.file) ? groupName2 : snd.file;
						wavToPlay = MainHelper.ConvertFromSpecialFoldersPattern(wavToPlay);
						var stream = GetIntroSound(wavToPlay);
						if (stream == null && System.IO.File.Exists(wavToPlay))
							stream = new System.IO.FileStream(wavToPlay, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						OnEvent(IntroSoundSelected, snd);
						if (stream != null)
							AddIntroSoundToPlayList(wavToPlay, v.group, stream);
					}
					break;
				case MessageCommands.Play:
					if (addMessage == null)
						addMessage = new message();
					addMessage.UpdateMissingAndChangedValuesFrom(v, true);
					var am = addMessage;
					// Clear buffer.
					addMessage = null;

					// Set gender: Male, Female or Neutral.
					MessageGender amGender;
					if (!Enum.TryParse(am.gender, out amGender))
						amGender = SettingsManager.Options.GenderComboBoxText;
					am.gender = amGender.ToString();

					// Select voice. ----------------------------------------------------------------------------------------------------
					var selectedVoice = SelectVoice(am.name, am.language, amGender);

					// Set pitch.
					var pitchIsValid = int.TryParse(am.pitch, out _Pitch);
					if (!pitchIsValid)
						_Pitch = MainHelper.GetNumber(SettingsManager.Options.PitchMin, SettingsManager.Options.PitchMax, "pitch", am.name);
					if (_Pitch < -10) _Pitch = -10;
					if (_Pitch > 10) _Pitch = 10;
					// Set PitchComment.
					_PitchComment = _Pitch >= 0 ? -1 : 1;

					// Set rate.
					var rateIsValid = int.TryParse(am.rate, out _Rate);
					if (!rateIsValid)
						_Rate = MainHelper.GetNumber(SettingsManager.Options.RateMin, SettingsManager.Options.RateMax, "rate", am.name);
					if (_Rate < -10) _Rate = -10;
					if (_Rate > 10) _Rate = 10;

					// Set effect.
					var _effect = string.IsNullOrEmpty(am.effect) ? "Default" : am.effect;
					if (cmd != MessageCommands.Save)
						OnEvent(EffectsPresetSelected, _effect);

					// Set volume.
					int _volume;
					var volumeIsValid = int.TryParse(am.volume, out _volume);
					if (!volumeIsValid)
						_volume = 100;
					if (_volume < 0) _volume = 0;
					if (_volume > 100) _volume = 100;

					bool amSound;
					bool.TryParse(am.sound, out amSound);
					if (amSound)
					{
						var groupName = string.IsNullOrEmpty(am.group)
							? SettingsManager.Options.DefaultIntroSoundComboBox ?? ""
							: am.group;
						// Find intro sound.
						snd = SettingsFile.Current.Sounds.FirstOrDefault(x => x.group.ToUpper() == groupName.ToUpper());
						// If sound was not found then...
						if (snd == null)
						{
							// Add sound to the list.
							snd = new sound();
							snd.group = am.group;
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
								AddIntroSoundToPlayList(wavToPlay, am.group, stream);
						}
					}
					// Decode complete message part.
					var allText = System.Web.HttpUtility.HtmlDecode(am.part);
					// Add silence before message.
					var programName = Program.MonitorItem.Name;
					var silenceIntBefore = SettingsManager.Options.AddSilenceBeforeMessage;
					if (silenceIntBefore > 0)
						AddTextToPlaylist(programName, "<silence msec=\"" + silenceIntBefore.ToString() + "\" />", true, am.group);
					// Add actual message to the playlist
					AddTextToPlaylist(programName, allText, true, am.group,
						// Supply NCP properties.
						am.name, am.gender, am.effect, _volume,
						// Supply Player properties.
						_PlayerName, _PlayerNameChanged, _PlayerClass,
						// Pass information required to pick correct synthesizer i.e. Local, Amazon or Google voice.
						selectedVoice == null ? null : selectedVoice.SourceKeys
					);
					// Add silence after message.
					var silenceIntAfter = (int)SettingsManager.Options.AddSilenceAfterMessage;
					if (silenceIntAfter > 0)
						AddTextToPlaylist(programName, "<silence msec=\"" + silenceIntAfter.ToString() + "\" />", true, am.group);
					break;
				case MessageCommands.Stop:
					StopPlayer(v.group);
					break;
				case MessageCommands.Save:
					// If name supplied then...
					if (!string.IsNullOrEmpty(v.name))
						// Save into default 
						SettingsManager.Current.UpsertDefaultsRecord(v);
					break;
				default:
					break;
			}
			OnEvent(ProcessedMessage, v);
		}

	}
}
