using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class Options
	{

		[DefaultValue("")]
		public string GoogleWebAppClientId { get; set; }

		[DefaultValue("")]
		public string GoogleWebAppClientSecret { get; set; }

	}
}
