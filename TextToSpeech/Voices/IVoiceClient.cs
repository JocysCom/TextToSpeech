using System;
using System.Collections.Generic;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public interface IVoiceClient<TVoice> where TVoice : class
	{
		List<TVoice> GetVoices(string cultureName = null, bool isNeural = false, int timeout = 20000);

		InstalledVoiceEx Convert(TVoice voice);

        Exception LastException { get; set; }

    }
}
