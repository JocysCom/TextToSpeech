using System;
namespace JocysCom.WoW.TextToSpeech.Network
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
