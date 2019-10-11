using System.Net.Sockets;

namespace JocysCom.TextToSpeech.Monitor
{
	public class SocketState
	{
		public SocketState(Socket socket, int bufferSize)
		{
			Socket = socket;
			Buffer = new byte[bufferSize];
		}

		public Socket Socket { get; }
		public byte[] Buffer { get; }
	}
}
