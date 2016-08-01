using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

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
