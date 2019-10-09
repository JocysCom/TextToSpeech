namespace JocysCom.TextToSpeech.Monitor.Google
{
	public class ListResponse
	{

		/// <summary>
		/// The languages that this voice supports, expressed as BCP-47
		/// language tags (e.g. "en-US", "es-419", "cmn-tw").
		/// </summary>
		public string[] languageCodes { get; set; }

		/// <summary>The gender of this voice.</summary>
		public SsmlVoiceGender ssmlGender { get; set; }

		/// <summary>The name of this voice.Each distinct voice has a unique name.</summary>
		public string name { get; set; }

		/// <summary>The natural sample rate(in hertz) for this voice.</summary>
		public int naturalSampleRateHertz { get; set; }

	}
}
