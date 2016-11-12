using JocysCom.TextToSpeech.Monitor.Network;
using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class VoiceListItem
	{

		internal IpPacket _IpHeader;
		internal TcpPacket _TcpHeader;

		#region GridView columns.

		public int TotalLength { get { return _IpHeader == null ? 0 : _IpHeader.TotalLength; } }
		public IPAddress SourceAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.SourceAddress; } }
		public ushort SourcePort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.SourcePort; } }
		public IPAddress DestinationAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.DestinationAddress; } }
		public ushort DestinationPort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.DestinationPort; } }
		public int DataLength { get { return _TcpHeader == null ? 0 : _TcpHeader.PayloadData.Length; } }

		#endregion

		public bool IsVoiceItem { get { return _IsVoiceItem; } }
		internal bool _IsVoiceItem;

		public string VoiceXml { get { return _VoiceXml; } }
		internal string _VoiceXml;

		public string Name { get; set; }

		public string[] Process { get; set; }

		#region Listen Filter

		public int FilterDestinationPort { get; set; }
		public int FilterSourcePort { get; set; }
		public TrafficDirection FilterDirection { get; set; }
		public ProtocolType? FilterProtocol { get; set; } 
		public string FilterProcessName { get; set; }

		#endregion

		public virtual void Load(string text, byte[] data = null) { }

		public virtual void Load(IpPacket ipHeader, TcpPacket tcpHeader) { }

	}

}
