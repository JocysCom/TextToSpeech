using System;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{

	[Flags]
	public enum CapturingType
	{
		Display = 0,
		WinPcap = 1,
		SocPcap = 2,
	}
}
