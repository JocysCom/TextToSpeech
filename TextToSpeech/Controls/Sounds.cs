using System.ComponentModel;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class sound : INotifyPropertyChanged
    {

        [XmlAttribute, DefaultValue(true)]
        public bool enabled { get { return _enabled; } set { _enabled = value; NotifyPropertyChanged("_enabled"); } }
        bool _enabled = true;

        [XmlAttribute]
        public string group { get { return _group; } set { _group = value; NotifyPropertyChanged("group"); } }
        string _group;

        [XmlAttribute]
        public string file { get { return _file; } set { _file = value; NotifyPropertyChanged("file"); } }
        string _file;

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

