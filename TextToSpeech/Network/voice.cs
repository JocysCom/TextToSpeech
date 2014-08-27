using System.Xml.Serialization;

namespace JocysCom.WoW.TextToSpeech.Network
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
        public string effect;
        [XmlAttribute]
        public string volume;
        [XmlElementAttribute("part")]
        public string[] parts;
    }

}

