using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	[DataContract]
	public class message : INotifyPropertyChanged
	{

		[DataMember, XmlAttribute, DefaultValue(true)]
		public bool enabled { get { return _enabled; } set { _enabled = value; OnPropertyChanged(); } }
		bool _enabled = true;

		[DataMember, XmlAttribute]
		public string name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
		string _name;

		[DataMember, XmlAttribute]
		public string voice { get { return _voice; } set { _voice = value; OnPropertyChanged(); } }
		string _voice;

		/// <summary>
		/// Culture. Can be sent in 2 formats:
		///		LCID HEX value: '419' = 0x0419 = ru-RU = Russian - Russia // Regex: ^[0-9a-fA-F]{1,4}$
		///		Language[-Location] value: 'en-GB' = English - Great Britain
		/// var ci = new System.Globalization.CultureInfo("en-GB", false);
		/// var ci = new System.Globalization.CultureInfo(0x040A, false);
		/// </summary>
		[DataMember, XmlAttribute, DefaultValue("")]
		public string language { get { return _language; } set { _language = value; OnPropertyChanged(); } }
		string _language;

		[DataMember, XmlAttribute]
		public string command { get { return _command; } set { _command = value; OnPropertyChanged(); } }
		string _command;

		[DataMember, XmlAttribute]
		public string pitch { get { return _pitch; } set { _pitch = value; OnPropertyChanged(); } }
		string _pitch;

		[DataMember, XmlAttribute]
		public string rate { get { return _rate; } set { _rate = value; OnPropertyChanged(); } }
		string _rate;

		[DataMember, XmlAttribute]
		public string gender { get { return _gender; } set { _gender = value; OnPropertyChanged(); } }
		string _gender;

		[DataMember, XmlAttribute]
		public string effect { get { return _effect; } set { _effect = value; OnPropertyChanged(); } }
		string _effect;

		[DataMember, XmlAttribute]
		public string group { get { return _group; } set { _group = value; OnPropertyChanged(); } }
		string _group;

		[DataMember, XmlAttribute]
		public string volume { get { return _volume; } set { _volume = value; OnPropertyChanged(); } }
		string _volume;

		[DataMember, XmlElement]
		public string part { get { return _parts; } set { _parts = value; OnPropertyChanged(); } }
		string _parts;

		/// <summary>
		/// Set current voice values from other voice object.
		/// </summary>
		/// <param name="v">voice which will be used as a source of values.</param>
		public void UpdateMissingValuesFrom(message v)
		{
			if (v == null)
				throw new ArgumentNullException(nameof(v));
			if (!v.enabled)
				return;
			// if value supplied and current is empty then...
			if (!string.IsNullOrEmpty(v.language) && string.IsNullOrEmpty(language)) language = v.language;
			if (!string.IsNullOrEmpty(v.gender) && string.IsNullOrEmpty(gender)) gender = v.gender;
			if (!string.IsNullOrEmpty(v.effect) && string.IsNullOrEmpty(effect)) effect = v.effect;
			if (!string.IsNullOrEmpty(v.pitch) && string.IsNullOrEmpty(pitch)) pitch = v.pitch;
			if (!string.IsNullOrEmpty(v.rate) && string.IsNullOrEmpty(rate)) rate = v.rate;
			if (!string.IsNullOrEmpty(v.group) && string.IsNullOrEmpty(group)) group = v.group;
			if (!string.IsNullOrEmpty(v.volume) && string.IsNullOrEmpty(volume)) volume = v.volume;
			if (!string.IsNullOrEmpty(v.voice) && string.IsNullOrEmpty(voice)) name = v.voice;
		}

		public void UpdateMissingAndChangedValuesFrom(message v)
		{
			if (v == null)
				throw new ArgumentNullException(nameof(v));
			// if value supplied and current is not the same then...
			if (!string.IsNullOrEmpty(v.language) && language != v.language) language = v.language;
			if (!string.IsNullOrEmpty(v.gender) && gender != v.gender) gender = v.gender;
			if (!string.IsNullOrEmpty(v.effect) && effect != v.effect) effect = v.effect;
			if (!string.IsNullOrEmpty(v.pitch) && pitch != v.pitch) pitch = v.pitch;
			if (!string.IsNullOrEmpty(v.rate) && rate != v.rate) rate = v.rate;
			if (!string.IsNullOrEmpty(v.group) && group != v.group) group = v.group;
			if (!string.IsNullOrEmpty(v.volume) && volume != v.volume) volume = v.volume;
		}

		public void UpdateFrom(message v)
		{
			if (v == null)
				throw new ArgumentNullException(nameof(v));
			language = v.language;
			gender = v.gender;
			effect = v.effect;
			pitch = v.pitch;
			rate = v.rate;
			group = v.group;
			volume = v.volume;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;
			handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}

}

