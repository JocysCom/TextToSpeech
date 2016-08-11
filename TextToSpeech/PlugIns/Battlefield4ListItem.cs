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
			Name = "Battlefield 4";
			FilterDestinationPort = 0;
			FilterDirection = TrafficDirection.In;
			FilterProtocol = System.Net.Sockets.ProtocolType.Tcp;
		}

		public override void Load(IpHeader ipHeader, ITcpUdpHeader tcpHeader)
		{
			_IpHeader = ipHeader;
			_TcpHeader = tcpHeader;
			// Parse battlefield message data.
			var data = ipHeader.Data;
		}
	}
}
