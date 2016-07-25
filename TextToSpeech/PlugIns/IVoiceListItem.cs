using JocysCom.TextToSpeech.Monitor.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public interface IVoiceListItem
	{
		int PortNumber { get; }

		string VoiceXml { get; }

		string Name { get; }
		void Load(IpHeader ipHeader, TcpHeader tcpHeader);
		void Load(string text);

	}
}
