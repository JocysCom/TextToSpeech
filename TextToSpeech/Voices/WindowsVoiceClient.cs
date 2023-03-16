using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public class WindowsVoiceClient: IVoiceClient<VoiceInfo>
	{
        public Exception LastException { get; set; }

        public InstalledVoiceEx Convert(VoiceInfo voice)
		{
			return new InstalledVoiceEx(voice);
		}

		public List<VoiceInfo> GetVoices(string cultureName = null, bool isNeural = false, int timeout = 20000)
		{
			// Fill grid with voices.
			// Create synthesizer which will be used to create WAV files from SSML XML.
			var ssmlSynthesizer = new SpeechSynthesizer();
			var voices = ssmlSynthesizer.GetInstalledVoices()
				.OrderBy(x => x.VoiceInfo.Culture.Name)
				.ThenBy(x => x.VoiceInfo.Gender)
				.ThenBy(x => x.VoiceInfo.Name)
				.Select(x => x.VoiceInfo)
				.ToList();
			ssmlSynthesizer.Dispose();
			return voices;
		}
	}
}
