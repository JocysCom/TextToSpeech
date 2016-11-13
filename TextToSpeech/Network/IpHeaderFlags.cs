using System;
namespace JocysCom.TextToSpeech.Monitor.Network
{
    [Flags]
    public enum IpHeaderFlags: byte
    {
        None = 0, 
        DF = 1,
        MF = 2,
        Reserved = 4,
    }
}
