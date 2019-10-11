using JocysCom.ClassLibrary.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor
{
	public class InstalledVoiceEx : INotifyPropertyChanged
	{
		public InstalledVoiceEx()
		{
		}

		[NonSerialized]
		public const int MaxVoice = 100;

		public InstalledVoiceEx(VoiceInfo voice)
		{
			Voice = voice;
			Source = VoiceSource.Local;
			// Set culture properties.
			var culture = voice.Culture;
			CultureLCID = culture.LCID;
			CultureName = culture.Name;
			Language = culture.TwoLetterISOLanguageName;
			// Set voice properties.
			Gender = voice.Gender;
			Name = voice.Name;
			Age = voice.Age;
			Description = voice.Description;
			Version = voice.AdditionalInfo.Where(x => x.Key == "Version").Select(x => x.Value).FirstOrDefault() ?? "";
			Enabled = true;
			switch (Gender)
			{
				case VoiceGender.Female: Female = MaxVoice; break;
				case VoiceGender.Male: Male = MaxVoice; break;
				case VoiceGender.Neutral: Neutral = MaxVoice; break;
				default: break;
			}
		}

		public override string ToString()
		{
			return Description;
		}

		public InstalledVoiceEx(Amazon.Polly.Model.Voice voice)
		{
			Voice = voice;
			Source = VoiceSource.Amazon;
			// Set culture properties.
			var culture = new System.Globalization.CultureInfo(voice.LanguageCode, false);
			CultureLCID = culture.LCID;
			CultureName = culture.Name;
			Language = culture.TwoLetterISOLanguageName;
			// Set voice properties.
			Name = voice.Name;
			Age = VoiceAge.NotSet;
			Description = string.Format("{0} {1} - {2} - {3}: {4}", Source, Name, CultureName, Gender, string.Join(", ", voice.SupportedEngines));
			Version = "";
			Enabled = true;
			if (voice.Gender == Amazon.Polly.Gender.Female)
			{
				Gender = VoiceGender.Female;
				Female = MaxVoice;
			}
			else if (voice.Gender == Amazon.Polly.Gender.Male)
			{
				Gender = VoiceGender.Male;
				Male = MaxVoice;
			}
		}

		bool _Enabled;
		int _Female;
		int _Male;
		int _Neutral;

		public bool Enabled { get { return _Enabled; } set { _Enabled = value; OnPropertyChanged(); } }
		public int Female { get { return _Female; } set { _Female = value; OnPropertyChanged(); } }
		public int Male { get { return _Male; } set { _Male = value; OnPropertyChanged(); } }
		public int Neutral { get { return _Neutral; } set { _Neutral = value; OnPropertyChanged(); } }

		[XmlIgnore]
		public object Voice { get { return _Voice; } set { _Voice = value; } }
		object _Voice;

		public VoiceSource Source { get { return _Source; } set { _Source = value; OnPropertyChanged(); } }
		VoiceSource _Source;

		public VoiceGender Gender { get; set; }
		public string Name { get; set; }
		public VoiceAge Age { get; set; }
		public string Description { get; set; }
		public string Version { get; set; }
		public string Language { get; set; }
		public string CultureName { get; set; }
		public int CultureLCID { get; set; }

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;
			ControlsHelper.Invoke(() => { handler(this, new PropertyChangedEventArgs(propertyName)); });
		}

		#endregion

	}
}
