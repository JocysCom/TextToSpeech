using System.ComponentModel;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class Options
	{

		[DefaultValue("")]
		public string GoogleWebAppClientId { get; set; }

		[DefaultValue("")]
		public string GoogleWebAppClientSecret { get; set; }

		[DefaultValue("")]
		public string GoogleTtsApiKey { get; set; }

	}
}
