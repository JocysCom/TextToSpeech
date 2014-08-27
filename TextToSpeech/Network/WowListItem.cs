using JocysCom.WoW.TextToSpeech.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.WoW.TextToSpeech.Network
{
    public class WowListItem
    {

        IpHeader _IpHeader;
        TcpHeader _TcpHeader;

        public WowListItem(IpHeader ipHeader, TcpHeader tcpHeader)
        {
            _IpHeader = ipHeader;
            _TcpHeader = tcpHeader;
            var text = System.Text.Encoding.ASCII.GetString(ipHeader.Data);
            if (text.Contains("<voice"))
            {
                // Find end of the voice tag.
                var endTag = "</voice>";
                var start = text.IndexOf("<voice");
                var end = text.IndexOf(endTag, start);
                if (end == -1)
                {
                    endTag = " />";
                    end = text.IndexOf(endTag, start);
                }
                if (end > start)
                {
                    _IsVoiceItem = true;
                    _VoiceXml = text.Substring(start, end - start + endTag.Length);
                }
            }
        }

        public int TotalLength { get { return _IpHeader.TotalLength; } }

        public IPAddress SourceAddress { get { return _IpHeader.SourceAddress; } }
        public ushort SourcePort { get { return _TcpHeader.SourcePort; } }
        public IPAddress DestinationAddress { get { return _IpHeader.DestinationAddress; } }
        public ushort DestinationPort { get { return _TcpHeader.DestinationPort; } }
        public uint SequenceNumber { get { return _TcpHeader.SequenceNumber; } }
        public TcpHeaderFlags Flags { get { return _TcpHeader.Flags; } }
        public int WowDataLength { get { return _TcpHeader.Data.Length; } }

        bool _IsVoiceItem;
        public bool IsVoiceItem { get { return _IsVoiceItem; } }

        string _VoiceXml;
        public string VoiceXml { get { return _VoiceXml; } }

    }
}
