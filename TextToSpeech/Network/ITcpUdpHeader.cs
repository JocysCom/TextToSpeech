using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public interface ITcpUdpHeader
    {
        ushort SourcePort { get; }
        ushort DestinationPort { get; }
		byte[] Data { get; }
    }
}
