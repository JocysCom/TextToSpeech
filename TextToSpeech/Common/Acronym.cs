using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor
{
    public class Acronym : INotifyPropertyChanged
	{

		[XmlAttribute, DefaultValue(true)]
		public bool Enabled { get { return _Enabled; } set { _Enabled = value; NotifyPropertyChanged("Enabled"); } }
		bool _Enabled = true;

		[XmlAttribute]
		public string Key { get { return _Key; } set { _Key = value; NotifyPropertyChanged("Key"); } }
		string _Key;

		[XmlAttribute]
		public string Value { get { return _Value; } set { _Value = value; NotifyPropertyChanged("Value"); } }
		string _Value;

		[XmlAttribute]
		public string Group { get { return _Group; } set { _Group = value; NotifyPropertyChanged("Group"); } }
		string _Group;

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
