using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class voice
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string command;
        [XmlAttribute]
        public string pitch;
        [XmlAttribute]
        public string rate;
        [XmlAttribute]
        public string gender;
        [XmlAttribute]
        public string effect;
        [XmlAttribute]
        public string volume;
        [XmlElementAttribute("part")]
        public string[] parts;
    }

}

