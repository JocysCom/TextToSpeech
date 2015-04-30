using JocysCom.TextToSpeech.Monitor.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class WowListItem
    {

        IpHeader _IpHeader;
        TcpHeader _TcpHeader;

        public WowListItem(string text)
        {
            loadFromText(text);
        }

        public WowListItem(IpHeader ipHeader, TcpHeader tcpHeader)
        {
            _IpHeader = ipHeader;
            _TcpHeader = tcpHeader;
            // Convert bytes to ASCII text. ASCII encoding is used in order to find propert position of <message></message> tags inside byte array.
            var text = System.Text.Encoding.ASCII.GetString(ipHeader.Data);
            loadFromText(text, ipHeader.Data);
        }

        void loadFromText(string text, byte[] data = null)
        {
            if (text.Contains("<message"))
            {
                var endTag = "</message>";
                // Find start of the voice tag.
                var start = text.IndexOf("<message");
                // Find end of the voice tag.
                var end = text.IndexOf(endTag, start);
                if (end == -1)
                {
                    endTag = " />";
                    end = text.IndexOf(endTag, start);
                }
                if (end > start)
                {
                    _IsVoiceItem = true;
                    // If original bytes are not available then...
                    if (data == null)
                    {
                        // Get voice text from original text block.
                        _VoiceXml = text.Substring(start, end - start + endTag.Length);
                    }
                    else
                    {
                        // Get voice text from original bytes as UTF8 text.
                        _VoiceXml = System.Text.Encoding.UTF8.GetString(data, start, end - start + endTag.Length);
                    }
                }
            }
        }

        public int TotalLength { get { return _IpHeader == null ? 0 : _IpHeader.TotalLength; } }

        public IPAddress SourceAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.SourceAddress; } }
        public ushort SourcePort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.SourcePort; } }
        public IPAddress DestinationAddress { get { return _IpHeader == null ? IPAddress.None : _IpHeader.DestinationAddress; } }
        public ushort DestinationPort { get { return _TcpHeader == null ? (ushort)0 : _TcpHeader.DestinationPort; } }
        public uint SequenceNumber { get { return _TcpHeader == null ? 0 : _TcpHeader.SequenceNumber; } }
        public TcpHeaderFlags Flags { get { return _TcpHeader == null ? TcpHeaderFlags.None : _TcpHeader.Flags; } }
        public int WowDataLength { get { return _TcpHeader == null ? 0: _TcpHeader.Data.Length; } }

        bool _IsVoiceItem;
        public bool IsVoiceItem { get { return _IsVoiceItem; } }

        string _VoiceXml;
        public string VoiceXml { get { return _VoiceXml; } }

    }
}
