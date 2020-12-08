
namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class WowListItem : VoiceListItem
	{
		public WowListItem(): base()
		{
			Name = "WoW";
			Process = new string[] { "wow.exe", "wow-64.exe" };
			FilterDestinationPort = 3724;
		}

	}
}
