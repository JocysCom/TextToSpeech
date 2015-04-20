using System.ComponentModel;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Network
{
	public class voice : INotifyPropertyChanged
    {

		[XmlAttribute, DefaultValue(true)]
		public bool enabled { get { return _enabled; } set { _enabled = value; NotifyPropertyChanged("_enabled"); } }
		bool _enabled = true;

		[XmlAttribute]
		public string name { get { return _name; } set { _name = value; NotifyPropertyChanged("name"); } }
		string _name;
		
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
		
		[XmlElementAttribute("part")]
		public string[] parts { get { return _parts; } set { _parts = value; NotifyPropertyChanged("parts"); } }
		string[] _parts;
		
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

