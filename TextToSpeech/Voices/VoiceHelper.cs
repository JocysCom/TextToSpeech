using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public static class VoiceHelper
	{
		public static List<InstalledVoiceEx> GetLocalVoices()
		{
			// Fill grid with voices.
			// Create synthesizer which will be used to create WAV files from SSML XML.
			var ssmlSynthesizer = new SpeechSynthesizer();
			var voices = ssmlSynthesizer.GetInstalledVoices()
				.OrderBy(x => x.VoiceInfo.Culture.Name)
				.ThenBy(x => x.VoiceInfo.Gender)
				.ThenBy(x => x.VoiceInfo.Name)
				.ToList();
			var voicesEx = voices.Select(x => new InstalledVoiceEx(x.VoiceInfo)).ToList();
			ssmlSynthesizer.Dispose();
			return voicesEx;
		}

	
	}
}
