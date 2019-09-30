using System;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public class MonitorEventArgs: EventArgs
	{
		public string Error { get; set; }
		public string Filter { get; set; }
		public string Packets { get; set; }
		public string State { get; set; }
	}
}
