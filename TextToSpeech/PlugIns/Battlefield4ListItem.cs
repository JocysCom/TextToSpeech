using JocysCom.TextToSpeech.Monitor.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class Battlefield4ListItem : VoiceListItem
	{
		public Battlefield4ListItem()
		{
			PortNumber = 0;
			Name = "Battlefield 4";
		}

		public override void Load(IpHeader ipHeader, TcpHeader tcpHeader)
		{
			_IpHeader = ipHeader;
			_TcpHeader = tcpHeader;
			// Parse battlefield message data.
			var data = ipHeader.Data;
		}
	}
}
