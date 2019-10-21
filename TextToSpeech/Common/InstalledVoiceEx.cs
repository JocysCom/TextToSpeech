using Amazon.Polly;
using JocysCom.ClassLibrary.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
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

		[NonSerialized] public const int MaxVoice = 100;

		#region Amazon Properties

		// Amazon Source Keys.
		[NonSerialized] public const string _KeySource = "Source";
		[NonSerialized] public const string _KeyRegion = "Region";
		[NonSerialized] public const string _KeyCulture = "Culture";
		[NonSerialized] public const string _KeyEngine = "Engine";
		[NonSerialized] public const string _KeyVoiceId = "VoiceId";

		[XmlIgnore]
		public Engine AmazonEngine
		{
			get
			{
				var query = System.Web.HttpUtility.ParseQueryString(SourceKeys ?? "");
				Engine engine = query[_KeyEngine];
				return engine;
			}
		}

		#endregion

		public InstalledVoiceEx(VoiceInfo voice)
		{
			Voice = voice;
			Source = VoiceSource.Local;
			// Set culture properties.
			SetCulture(voice.Culture);
			// Set voice properties.
			Gender = voice.Gender;
			Name = voice.Name;
			Age = voice.Age;
			Description = voice.Description;
			Version = voice.AdditionalInfo.Where(x => x.Key == "Version").Select(x => x.Value).FirstOrDefault() ?? "";
			switch (Gender)
			{
				case VoiceGender.Female: Female = MaxVoice; break;
				case VoiceGender.Male: Male = MaxVoice; break;
				case VoiceGender.Neutral: Neutral = MaxVoice; break;
				default: break;
			}
			var keys = System.Web.HttpUtility.ParseQueryString("");
			keys.Add(_KeySource, VoiceSource.Local.ToString());
			keys.Add(_KeyCulture, CultureName);
			keys.Add(_KeyVoiceId, voice.Id);
			SourceKeys = keys.ToString();
		}

		public override string ToString()
		{
			return Description;
		}

		public void SetCulture(CultureInfo culture)
		{
			// Set culture properties.
			CultureLCID = culture.LCID;
			CultureName = culture.Name;
			Language = culture.TwoLetterISOLanguageName;
		}

		public bool IsSameVoice(InstalledVoiceEx voice)
		{
			var isSame =
				Source == voice.Source &&
				CultureName == voice.CultureName &&
				Name == voice.Name &&
				Gender == voice.Gender;
			// If source is amazon then compare engine too.
			if (Source == VoiceSource.Amazon)
			{
				var query = System.Web.HttpUtility.ParseQueryString(SourceKeys ?? "");
				var voiceQuery = System.Web.HttpUtility.ParseQueryString(voice.SourceKeys ?? "");
				Engine engine = query[_KeyEngine];
				Engine voiceEngine = voiceQuery[_KeyEngine];
				isSame &= engine == voiceEngine;
			}
			return isSame;
		}

		public InstalledVoiceEx(Amazon.Polly.Model.Voice voice)
		{
			Voice = voice;
			Source = VoiceSource.Amazon;
			// Set culture properties.
			var culture = new CultureInfo(voice.LanguageCode);
			SetCulture(culture);
			// Set voice properties.
			Name = voice.Name;
			Age = VoiceAge.NotSet;
			Description = string.Format("{0} {1} - {2} - {3}: {4}", Source, Name, CultureName, Gender, string.Join(", ", voice.SupportedEngines));
			Version = "";
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
		public object Voice { get; set; }

		/// <summary>Used to pick fastest server.</summary>
		[XmlIgnore]
		public TimeSpan SourceRequestSpeed { get; set; }

		public VoiceSource Source { get { return _Source; } set { _Source = value; OnPropertyChanged(); } }
		VoiceSource _Source;

		public string SourceKeys { get; set; }

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
			if (handler != null)
				ControlsHelper.Invoke(handler, this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}
}
