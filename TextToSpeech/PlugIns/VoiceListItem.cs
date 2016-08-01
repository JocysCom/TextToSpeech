using JocysCom.TextToSpeech.Monitor.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class VoiceListItem
	{

		internal IpHeader _IpHeader;
		internal TcpHeader _TcpHeader;

		#region GridView columns.

		public int TotalLength { get { return _IpHeader == null ? 0 : _IpHeader.TotalLength; } }
		public IPAddress SourceAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.SourceAddress; } }
		public ushort SourcePort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.SourcePort; } }
		public IPAddress DestinationAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.DestinationAddress; } }
		public ushort DestinationPort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.DestinationPort; } }
		public uint SequenceNumber { get { return _TcpHeader == null ? 0 : _TcpHeader.SequenceNumber; } }
		public TcpHeaderFlags Flags { get { return _TcpHeader == null ? TcpHeaderFlags.None : _TcpHeader.Flags; } }
		public int DataLength { get { return _TcpHeader == null ? 0 : _TcpHeader.Data.Length; } }

		#endregion

		public bool IsVoiceItem { get { return _IsVoiceItem; } }
		internal bool _IsVoiceItem;

		public string VoiceXml { get { return _VoiceXml; } }
		internal string _VoiceXml;

        #region Listen Filter

        public int PortNumber { get; set; }
		public string Name { get; set; }
        public bool Incomming { get; set; }
        public bool Outgoing { get; set; }

        #endregion

        public virtual void Load(string text) { }

		public virtual void Load(IpHeader ipHeader, TcpHeader tcpHeader) { }

	}

}
