using Microsoft.Win32;
using System;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class WinPcapHelper
	{

		public static Version GetWinPcapVersion()
		{
			Version version = null;
			using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\WinPcapInst"))
			{
				if (key != null)
				{
					string ver = key.GetValue("DisplayVersion") as string;
					if (!string.IsNullOrEmpty(ver))
					{
						Version.TryParse(ver, out version);
					}
				}
			}
			return version;
		}

	}
}
