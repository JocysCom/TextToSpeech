using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.IO;
using System.Linq;
//using System.Text.RegularExpressions;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public static partial class Global
	{

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

		public static ItemPlayOptions GetPlayOptions(string name = null, string language = null, string gender = null,
			string overridePitch = null,
			string overrideRate = null,
			string overrideVolume = null,
			string effect = null
		)
		{
			var ipo = new ItemPlayOptions();
			// Set gender: Male, Female or Neutral.
			MessageGender amGender;
			if (!Enum.TryParse(gender, out amGender))
				amGender = SettingsManager.Options.GenderComboBoxText;
			ipo.Gender = amGender;
			if (name == null)
				ipo.Voice = SelectedVoice;
			else
				ipo.Voice = SelectVoice(name, language, amGender);
			// Set pitch.
			int _pitch;
			var pitchIsValid = int.TryParse(overridePitch, out _pitch);
			if (!pitchIsValid)
				_pitch = MainHelper.GetNumber(SettingsManager.Options.PitchMin, SettingsManager.Options.PitchMax, "pitch", name);
			if (_pitch < -10)
				_pitch = -10;
			if (_pitch > 10)
				_pitch = 10;
			ipo.Pitch = _pitch;
			// Set rate.
			int _rate;
			var rateIsValid = int.TryParse(overrideRate, out _rate);
			if (!rateIsValid)
				_rate = MainHelper.GetNumber(SettingsManager.Options.RateMin, SettingsManager.Options.RateMax, "rate", name);
			if (_rate < -10)
				_rate = -10;
			if (_rate > 10)
				_rate = 10;
			ipo.Rate = _rate;
			// Set volume.
			int _volume;
			var volumeIsValid = int.TryParse(overrideVolume, out _volume);
			if (!volumeIsValid)
				_volume = 100;
			if (_volume < 0)
				_volume = 0;
			if (_volume > 100)
				_volume = 100;
			ipo.Volume = _volume;
			// Set effect.
			string _effect;
			if (string.IsNullOrEmpty(effect))
				_effect = "Default";
			else
			{
				_effect = effect;
				if (SelectedEffect != effect)
					OnEvent(EffectsPresetSelected, effect);
			}
			ipo.Effect = _effect;
			return ipo;
		}

		public static void ProcessVoiceTextMessage(string text)
		{
			// If <message.
			if (!text.Contains("<message")) return;

			// Insert name="Undefined" attribute if not submited.
			text = !text.Contains("name=") ? text.Replace("<message", "<message name=\"Undefined\"") : text;

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

					// Select voice. ----------------------------------------------------------------------------------------------------
					var ipo = GetPlayOptions(am.name, am.language, am.gender, am.pitch, am.rate, am.volume, am.effect);


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
						AddTextToPlaylist(ipo, programName, "<silence msec=\"" + silenceIntBefore.ToString() + "\" />", true, am.group);
						// Add actual message to the playlist
						AddTextToPlaylist(ipo, programName, allText, true, am.group,
						// Supply NCP properties.
						am.name, am.effect,
						// Supply Player properties.
						_PlayerName, _PlayerNameChanged, _PlayerClass,
						// Pass information required to pick correct synthesizer i.e. Local, Amazon or Google voice.
						ipo.Voice?.SourceKeys
					);
					// Add silence after message.
					var silenceIntAfter = (int)SettingsManager.Options.AddSilenceAfterMessage;
					if (silenceIntAfter > 0)
						AddTextToPlaylist(ipo, programName, "<silence msec=\"" + silenceIntAfter.ToString() + "\" />", true, am.group);
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
