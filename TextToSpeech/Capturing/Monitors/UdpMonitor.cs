using System.Net;
using System.Net.Sockets;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public class UdpMonitor : MonitorBase
	{

		Socket serverSocket;
		object serverSocketLock = new object();
		public int PortNumber
		{
			get { return _PortNumber; }
			set
			{
				var isRunning = IsRunning;
				if (isRunning)
					Stop();
				_PortNumber = value;
				if (isRunning)
					Start();
				OnPropertyChanged();
			}
		}

		int _PortNumber;

		public override void Start()
		{
			lock (serverSocketLock)
			{
				if (IsDisposing) return;
				// If server is already running then return.
				if (serverSocket != null)
					return;
				serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				serverSocket.ExclusiveAddressUse = false;
				serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
				var address = IPAddress.Parse("127.0.0.1");
				var localEP = new IPEndPoint(address, PortNumber);
				serverSocket.Bind(localEP);
				StartReceive();
				_IsRunning = true;
			}
		}

		public override void Stop()
		{
			lock (serverSocketLock)
			{
				// If server is running then...
				if (serverSocket != null)
				{
					serverSocket.Close();
					serverSocket = null;
				}
				_IsRunning = false;
			}
		}

		void StartReceive()
		{
			byte[] buffer = new byte[8096];
			var sockArgs = new SocketAsyncEventArgs();
			sockArgs.AcceptSocket = serverSocket;
			// Receive only from 127.0.0.1.
			var address = IPAddress.Parse("127.0.0.1");
			var remoteEP = new IPEndPoint(address, 0);
			sockArgs.RemoteEndPoint = remoteEP;
			sockArgs.SetBuffer(buffer, 0, buffer.Length);
			sockArgs.Completed += sockArgs_Completed;
			var willRaiseEvent = serverSocket.ReceiveFromAsync(sockArgs);
			if (!willRaiseEvent) CompleteReceive(sockArgs);
		}

		void sockArgs_Completed(object sender, SocketAsyncEventArgs e)
		{
			CompleteReceive(e);
		}

		void CompleteReceive(SocketAsyncEventArgs args)
		{
			string text = null;
			// If success then process text.
			if (args.SocketError == SocketError.Success)
				text = System.Text.Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);
			if (!string.IsNullOrEmpty(text))
				OnMessageReceived(text);
			lock (serverSocketLock)
			{
				if (IsDisposing)
					return;
				// If socket stopped then return.
				if (serverSocket == null)
					return;
				StartReceive();
			}
		}

	}
}
