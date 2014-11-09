using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    [Flags]
    public enum TcpHeaderFlags : byte
    {
        None = 0,
        FIN = 1,
        SYN = 2,
        RTS = 4,
        PSH = 8,
        ACK = 16,
        URG = 32,
    }
}
