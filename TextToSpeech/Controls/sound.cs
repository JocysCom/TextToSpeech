using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	[DataContract]
	public class sound : INotifyPropertyChanged
	{

		[DataMember, XmlAttribute, DefaultValue(true)]
		public bool enabled { get { return _enabled; } set { _enabled = value; OnPropertyChanged(); } }
		bool _enabled = true;

		[DataMember, XmlAttribute]
		public string group { get { return _group; } set { _group = value; OnPropertyChanged(); } }
		string _group;

		[DataMember, XmlAttribute]
		public string file { get { return _file; } set { _file = value; OnPropertyChanged(); } }
		string _file;

		[DataMember, XmlElement("part")]
		public string[] parts { get { return _parts; } set { _parts = value; OnPropertyChanged(); } }
		string[] _parts;

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

