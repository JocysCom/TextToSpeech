using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class SocPcapDevice : ICaptureDevice
	{

		public event PacketArrivalEventHandler OnPacketArrival;
		public event CaptureStoppedEventHandler OnCaptureStopped;
		public ushort bufferSize = 0xFFFF;

		object monitorLock = new object();
		private List<Socket> CaptureMonitors = new List<Socket>();
		List<IPAddress> IpAddresses = new List<IPAddress>();
		public Exception LastException;
		bool continueMonitoring;

		public void Open()
		{

			lock (monitorLock)
			{
				// If monitor is running already then...
				if (CaptureMonitors.Count > 0)
				{
					return;
				}
				IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
				IpAddresses.Clear();
				if (HosyEntry.AddressList.Length > 0)
				{
					foreach (IPAddress ip in HosyEntry.AddressList)
					{
						if (ip.AddressFamily == AddressFamily.InterNetwork || ip.AddressFamily == AddressFamily.InterNetworkV6)
						{
							// If IP address is not in the list then...
							if (!IpAddresses.Contains(ip))
							{
								// Add IP Address.
								IpAddresses.Add(ip);
							}
						}
					}
				}
				if (IpAddresses.Count == 0)
				{
					return;
				}
				else
				{
					if (IpAddresses.Count == 1)
					{
					}
					else
					{
						var ip4c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetwork);
						var ip6c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetworkV6);
					}
				}
				try
				{
					foreach (var ip in IpAddresses)
					{
						var isIP6 = ip.AddressFamily == AddressFamily.InterNetworkV6;
						var optionLevel = isIP6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP;
						var protocolType = isIP6 ? ProtocolType.IP : ProtocolType.IP;

						//IPv6MulticastOption multicastOption = new IPv6MulticastOption(IPAddress.Parse("FF02::1:2"));
						//socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, multicastOption);

						// For sniffing the socket to monitor the packets has to be a raw socket, with the
						// address family being of type internetwork, and protocol being IP
						var socket = new Socket(ip.AddressFamily, SocketType.Raw, protocolType);
						//Bind the socket to the selected IP address.
						// Note: it looks like monitorPort value is ignored and all ports will be monitored.
						socket.Bind(new IPEndPoint(ip, 0));
						//Set the socket  options: Applies only to TCP packets, Set the include the header, option to true.
						socket.SetSocketOption(optionLevel, SocketOptionName.HeaderIncluded, true);
						// Input data required by the operation. 
						byte[] optionInValue = new byte[4] { 1, 0, 0, 0 };
						// Output data returned by the operation. 
						byte[] optionOutValue = new byte[4];
						// Socket.IOControl is analogous to the WSAIoctl method of Winsock 2: Equivalent to SIO_RCVALL constant, of Winsock 2
						socket.IOControl(IOControlCode.ReceiveAll, optionInValue, optionOutValue);
						// The default socket buffer size in Windows sockets is 8192 bytes.
						// Increase the receive buffer to 65535 bytes or some UDP data packets will be lost.
						socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, bufferSize);
						CaptureMonitors.Add(socket);
					}
				}
				catch (Exception ex)
				{
					LastException = ex;
				}
			}
		}

		private void BeginReceive_Callback(IAsyncResult ar)
		{
			try
			{
				var state = (SocketState)ar.AsyncState;
				var isIp6 = state.Socket.AddressFamily == AddressFamily.InterNetworkV6;
				SocketError errorCode;
				int bytesReceived = state.Socket.EndReceive(ar, out errorCode);
				if (errorCode == SocketError.Success && bytesReceived > 0)
				{
					try
					{
						var data = new byte[bytesReceived];
						Array.Copy(state.Buffer, data, bytesReceived);
						var sha = new PhysicalAddress(new byte[6]);
						var dha = new PhysicalAddress(new byte[6]);
						EthernetPacket ep = null;
						if (state.Socket.LocalEndPoint.AddressFamily == AddressFamily.InterNetwork)
						{
							// Create ethernet packet.
							ep = new EthernetPacket(sha, dha, EthernetPacketType.IpV4);
							// Add IP packet data.
							ep.PayloadData = data;
						}
						if (state.Socket.LocalEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
						{
							// Create missing IP packet.
							var sa = IPAddress.Any;
							var da = IPAddress.Any;
							var la = ((IPEndPoint)state.Socket.LocalEndPoint).Address;
							if (IpAddresses.Contains(la))
							{
								sa = la;
							}
							var ip = new IPv6Packet(sa, da);
							// Add TCP/UDP data
							ip.PayloadData = data;
							// Create ethernet packet.
							ep = new EthernetPacket(sha, dha, EthernetPacketType.IpV6);
							// Add IP packet data.
							ep.PayloadData = ip.Bytes;
						}
						var ev = OnPacketArrival;
						if (ev != null && ep != null)
						{
							var ti = new PosixTimeval();
							// Get ethernet packet data.
							var packet = new RawCapture(LinkLayers.Ethernet, ti, ep.Bytes);
							var e = new CaptureEventArgs(packet, this);
							ev(this, e);
						}
					}
					catch (Exception ex)
					{
						LastException = ex;
					}
					// Analyze the bytes received...
					//ParseData(state.Buffer, bytesReceived);
				}
				if (continueMonitoring)
				{
					//Another call to BeginReceive so that we continue to receive the incoming packets.
					state.Socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), state);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				LastException = ex;
			}
		}


		public void Close()
		{
			lock (monitorLock)
			{
				StopCapture();
				foreach (var item in CaptureMonitors)
				{
					item.Close();
				}
				CaptureMonitors.Clear();
			}
		}

		public string Filter { get; set; }


		public void StartCapture()
		{
			lock (monitorLock)
			{
				continueMonitoring = true;
				foreach (var socket in CaptureMonitors)
				{
					// Create data buffer where socket will write captured data.
					var state = new SocketState(socket, bufferSize);
					// Start receiving the packets asynchronously.
					socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), state);
				}
			}
		}

		public void StopCapture()
		{
			lock (monitorLock)
			{
				continueMonitoring = false;
				var ev = OnCaptureStopped;
				if (ev != null)
				{
					var e = new CaptureStoppedEventStatus();
					ev(this, e);
				}
			}
		}

		public void Open(DeviceMode mode)
		{
			throw new NotImplementedException();
		}

		public void Open(DeviceMode mode, int read_timeout)
		{
			throw new NotImplementedException();
		}

		public void Capture()
		{
			throw new NotImplementedException();
		}

		public RawCapture GetNextPacket()
		{
			throw new NotImplementedException();
		}

		public int GetNextPacketPointers(ref IntPtr header, ref IntPtr data)
		{
			throw new NotImplementedException();
		}

		public void SendPacket(Packet p)
		{
			throw new NotImplementedException();
		}

		public void SendPacket(Packet p, int size)
		{
			throw new NotImplementedException();
		}

		public void SendPacket(byte[] p)
		{
			throw new NotImplementedException();
		}

		public void SendPacket(byte[] p, int size)
		{
			throw new NotImplementedException();
		}

		public ReadOnlyCollection<IPAddress> Addresses
		{
			get
			{
				return new ReadOnlyCollection<IPAddress>(IpAddresses);
			}
		}

		public string Name
		{
			get
			{
				return null;
			}
		}

		public string Description
		{
			get
			{
				return null;
			}
		}

		public string LastError
		{
			get
			{
				return null;
			}
		}

		public ICaptureStatistics Statistics
		{
			get
			{
				return null;
			}
		}

		public PhysicalAddress MacAddress
		{
			get
			{
				return null;
			}
		}

		public bool Started
		{
			get
			{
				return false;
			}
		}

		public TimeSpan StopCaptureTimeout
		{
			get
			{
				return new TimeSpan();
			}
			set
			{
			}
		}

		public LinkLayers LinkType
		{
			get
			{
				return LinkLayers.Null;
			}
		}
	}
}
