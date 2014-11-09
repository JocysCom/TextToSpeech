using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor
{
    public class InstalledVoiceEx
    {
        public InstalledVoiceEx()
        {
        }

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
            Neutral = 100;
            switch (Gender)
            {
                case VoiceGender.Female: Female = 100; break;
                case VoiceGender.Male: Male = 100; break;
                default: break;
            }
        }

        public int Female { get; set; }
        public int Male { get; set; }
        public int Neutral { get; set; }
        public VoiceGender Gender { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string CultureName { get; set; }
        public VoiceAge Age { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public int CultureLCID { get; set; }
    }
}
