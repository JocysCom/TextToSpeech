using System.ComponentModel;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public enum CacheFileFormat
	{
		[Description("MPEG Audio Layer III (*.mp3")]
		MP3,
		/// <summary>
		/// Data communication over the telephone network.
		/// https://www.itu.int/rec/T-REC-G.711
		/// </summary>
		[Description("ITU-T G.711 µ-law (US, Japan)")]
		ULaw,
		/// <summary>
		/// Data communication over the telephone network.
		/// https://www.itu.int/rec/T-REC-G.711
		/// </summary>
		[Description("ITU-T G.711 A-law (Europe)")]
		ALaw
	}
}
