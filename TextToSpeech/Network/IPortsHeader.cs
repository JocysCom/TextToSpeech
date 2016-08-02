using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public interface IPortsHeader
    {
        ushort SourcePort { get; }
        ushort DestinationPort { get; }
        byte[] Data { get; }
    }
}
