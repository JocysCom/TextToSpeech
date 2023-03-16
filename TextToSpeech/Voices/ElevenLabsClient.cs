using System;
using System.Collections.Generic;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
    public class ElevenLabsClient : IVoiceClient<InstalledVoiceEx>
    {
        public ElevenLabsClient(string apiKey)
        {
        }

        public InstalledVoiceEx Convert(InstalledVoiceEx voice)
        {
            throw new System.NotImplementedException();
        }

        public List<InstalledVoiceEx> GetVoices(string cultureName = null, bool isNeural = false, int timeout = 20000)
        {
            return new List<InstalledVoiceEx>();
        }

        public Exception LastException { get; set; }
    }
}
