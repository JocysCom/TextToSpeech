using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

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
            Gender = voice.Gender;
            Name = voice.Name;
            Language = voice.AdditionalInfo.Where(x => x.Key == "Language").Select(x => x.Value).FirstOrDefault() ?? "";
            CultureName = voice.Culture.Name;
            Age = voice.Age;
            Description = voice.Description;
            Version = voice.AdditionalInfo.Where(x => x.Key == "Version").Select(x => x.Value).FirstOrDefault() ?? "";
            CultureLCID = voice.Culture.LCID;
            Enabled = true;
            switch (Gender)
            {
                case VoiceGender.Female: Female = MaxVoice; break;
                case VoiceGender.Male: Male = MaxVoice; break;
                case VoiceGender.Neutral: Neutral = MaxVoice; break;
                default: break;
            }
        }

        bool _Enabled;
        int _Female;
        int _Male;
        int _Neutral;

        public bool Enabled { get { return _Enabled; } set { _Enabled = value; NotifyPropertyChanged("Enabled"); } }
        public int Female { get { return _Female; } set { _Female = value; NotifyPropertyChanged("Female"); } }
        public int Male { get { return _Male; } set { _Male = value; NotifyPropertyChanged("Male"); } }
        public int Neutral { get { return _Neutral; } set { _Neutral = value; NotifyPropertyChanged("Neutral"); } }
        public VoiceGender Gender { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string CultureName { get; set; }
        public VoiceAge Age { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public int CultureLCID { get; set; }

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
