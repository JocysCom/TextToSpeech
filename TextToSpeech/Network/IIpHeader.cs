using System.Net;
using System.Net.Sockets;

namespace JocysCom.TextToSpeech.Monitor.Network
{
	public interface IIpHeader
	{
		IPAddress SourceAddress { get; }
		IPAddress DestinationAddress { get; }
		ProtocolType Protocol { get; }
		ushort TotalLength { get; }
		byte[] Data { get; }
	}
}
