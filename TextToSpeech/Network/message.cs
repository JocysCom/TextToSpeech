using System.ComponentModel;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class message : INotifyPropertyChanged
    {

        [XmlAttribute, DefaultValue(true)]
        public bool enabled { get { return _enabled; } set { _enabled = value; NotifyPropertyChanged("_enabled"); } }
        bool _enabled = true;
      
        [XmlAttribute]
        public string name { get { return _name; } set { _name = value; NotifyPropertyChanged("name"); } }
        string _name;

        [XmlAttribute]
        public string voice { get { return _voice; } set { _voice = value; NotifyPropertyChanged("voice"); } }
        string _voice;

        [XmlAttribute]
        public string language { get { return _language; } set { _language = value; NotifyPropertyChanged("language"); } }
        string _language;

        [XmlAttribute]
        public string command { get { return _command; } set { _command = value; NotifyPropertyChanged("command"); } }
        string _command;

        [XmlAttribute]
        public string pitch { get { return _pitch; } set { _pitch = value; NotifyPropertyChanged("pitch"); } }
        string _pitch;

        [XmlAttribute]
        public string rate { get { return _rate; } set { _rate = value; NotifyPropertyChanged("rate"); } }
        string _rate;

        [XmlAttribute]
        public string gender { get { return _gender; } set { _gender = value; NotifyPropertyChanged("gender"); } }
        string _gender;

        [XmlAttribute]
        public string effect { get { return _effect; } set { _effect = value; NotifyPropertyChanged("effect"); } }
        string _effect;

        [XmlAttribute]
        public string group { get { return _group; } set { _group = value; NotifyPropertyChanged("group"); } }
        string _group;

        [XmlAttribute]
        public string volume { get { return _volume; } set { _volume = value; NotifyPropertyChanged("volume"); } }
        string _volume;

        [XmlElement("part")]
        public string[] parts { get { return _parts; } set { _parts = value; NotifyPropertyChanged("parts"); } }
        string[] _parts;

        /// <summary>
        /// Set current voice values from other voice object.
        /// </summary>
        /// <param name="v">voice which will be used as a source of values.</param>
        public void UpdateMissingValuesFrom(message v)
        {
            
            if (v.enabled) { 
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
        }

		public void UpdateMissingAndCangedValuesFrom(message v)
		{
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

        private void NotifyPropertyChanged(string propertyName = "")
        {
            var ev = PropertyChanged;
            if (ev == null) return;
            ev(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }

}

