using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using PacketDotNet;
using SharpPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public partial class NetworkMonitor : MonitorBase
	{
		public NetworkMonitor()
		{
			Writer = new ClassLibrary.IO.LogFileWriter();
			Writer.LogFileAutoFlush = true;
			var folder = SettingsManager.Options.NetworkMonitorLogFolder;
			if (string.IsNullOrEmpty(folder))
				folder = GetLogsPath(true);
			Writer.LogFileName = folder;
		}

		List<IPAddress> IpAddresses = new List<IPAddress>();
		// The socket which monitors all incoming packets.
		private List<ICaptureDevice> CaptureDevices = new List<ICaptureDevice>();

		long Ip4PacketsCount = 0;
		long Ip6PacketsCount = 0;

		public JocysCom.ClassLibrary.IO.LogFileWriter Writer;
		public object WriterLock = new object();

		object SequenceNumbersLock = new object();
		List<uint> SequenceNumbers = new List<uint>();

		public static string GetLogsPath(bool create)
		{
			var path = Path.Combine(MainHelper.AppDataPath, "Logs");
			if (create && !Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}

		public CapturingType CapturingType
		{
			get { return _CapturingType; }
			set
			{
				var isRunning = IsRunning;
				if (isRunning)
					Stop();
				_CapturingType = value;
				if (isRunning)
					Start();
				OnPropertyChanged();
			}
		}
		CapturingType _CapturingType;

		public override void Start()
		{
			lock (monitorLock)
			{
				InitWatcher();
				if (IsRunning)
					return;
				if (IsDisposing)
					return;
				// If monitor is running already then...
				if (CaptureDevices.Count > 0)
				{
					OnStatusChanged(null, null, null, "Error: Monitoring already. Stop first!");
					return;
				}
				try
				{
					IpAddresses.Clear();
					// Retrieve all capture devices
					if (SettingsManager.Options.NetworkMonitorCapturingType == CapturingType.WinPcap)
					{
						var devices = CaptureDeviceList.Instance.ToArray();
						var wcaps = devices.OfType<WinPcapDevice>();
						foreach (var device in wcaps)
						{
							device.OnPacketArrival += Wc_OnPacketArrival;
							device.Open(DeviceMode.Normal);
							device.Filter = "ip";
							foreach (var address in device.Addresses)
							{
								if (address.Addr != null && address.Addr.ipAddress != null)
								{
									IpAddresses.Add(address.Addr.ipAddress);
								}
							}
							CaptureDevices.Add(device);
						}
					}
					else if (SettingsManager.Options.NetworkMonitorCapturingType == CapturingType.SocPcap)
					{
						var device = new SocPcapDevice();
						device.OnPacketArrival += Wc_OnPacketArrival;
						device.Open();
						device.Filter = "ip";
						foreach (var address in device.Addresses)
							IpAddresses.Add(address);
						CaptureDevices.Add(device);
					}
					// Set default packet filter.
					var mi = Program.MonitorItem;
					SetFilter(mi);
					var ip4c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetwork);
					var ip6c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetworkV6);
					OnStatusChanged(null, null, null, string.Format("Addresses: {0} IPv4, {1} IPv6", ip4c, ip6c));
					// Retrieve all capture devices
					foreach (var device in CaptureDevices)
					{
						// Start the capturing process
						device.StartCapture();
					}
				}
				catch (Exception ex)
				{
					LastException = ex;
				}
			}
		}

		public override void Stop()
		{
			lock (monitorLock)
			{
				// If monitor is not running then...
				if (CaptureDevices.Count == 0)
				{
					OnStatusChanged(null, null, null, "Error: Not monitoring. Start monitor first!");
					return;
				}
				foreach (var device in CaptureDevices)
				{
					device.StopCapture();
					device.Close();
				}
				CaptureDevices.Clear();
				DisposeWatcher();
			}
		}

		private void Wc_OnPacketArrival(object sender, CaptureEventArgs e)
		{
			if (IsDisposing)
				return;
			if (e.Packet == null || e.Packet.Data == null || e.Packet.Data.Length == 0)
				return;
			if (e.Packet.LinkLayerType != LinkLayers.Ethernet)
				return;
			Packet packet = null;
			try
			{
				packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
			}
			catch (Exception)
			{
				return;
			}
			var ep = packet as EthernetPacket;
			if (ep == null)
				return;
			if (ep.Type != EthernetPacketType.IpV4 && ep.Type != EthernetPacketType.IpV6) return;
			var ip = ep.PayloadPacket as IpPacket;
			if (ip == null)
				return;
			var tp = ip.PayloadPacket as TcpPacket;
			if (tp == null)
				return;
			if (tp.PayloadData.Length == 0)
				return;
			IPAddress srcIp = ip.SourceAddress;
			IPAddress dstIp = ip.DestinationAddress;
			int srcPort = tp.SourcePort;
			int dstPort = tp.DestinationPort;
			if (ep.Type == EthernetPacketType.IpV6)
				System.Threading.Interlocked.Increment(ref Ip6PacketsCount);
			else
				System.Threading.Interlocked.Increment(ref Ip4PacketsCount);
			OnStatusChanged(null, null, string.Format("Packets: {0} IPv4, {1} IPv6", Ip4PacketsCount, Ip6PacketsCount));
			var sequenceNumber = tp.SequenceNumber;
			// ---------------------------------------------------------------------------
			var sourceIsLocal = IpAddresses.Contains(ip.SourceAddress);
			var destinationIsLocal = IpAddresses.Contains(ip.DestinationAddress);
			var direction = TrafficDirection.Local;
			// Determine packet direction.
			if (sourceIsLocal && !destinationIsLocal)
				direction = TrafficDirection.Out;
			else if (!sourceIsLocal && destinationIsLocal)
				direction = TrafficDirection.In;
			// IPHeader.Data stores the data being carried by the IP datagram.
			if (SettingsManager.Options.LogEnable)
			{
				var index = -1;
				var text = SettingsManager.Options.LogText;
				if (!string.IsNullOrEmpty(text))
				{
					var pattern = System.Text.Encoding.ASCII.GetBytes(text);
					index = ClassLibrary.Text.Helper.IndexOf(tp.PayloadData, pattern, 0);
				}
				if (index > -1)
				{
					// Play "Radio2" sound if "LogEnabled" and "LogSound" check-boxes are checked.
					if (SettingsManager.Options.LogSound)
						Audio.Global.PlayLogSound();
					// ---------------------------------------------
					var writer = Writer;
					if (writer != null)
					{
						writer.WriteLine("{0:HH:mm:ss.fff}: {1} {2}: {3}:{4} -> {5}:{6} Data[{7}]",
							DateTime.Now,
							ep.Type.ToString().ToUpper(),
							destinationIsLocal ? "In" : "Out",
							ip.SourceAddress,
							tp.SourcePort,
							ip.DestinationAddress,
							tp.DestinationPort,
							tp.PayloadData.Length
						);
						var block = JocysCom.ClassLibrary.Text.Helper.BytesToStringBlock(
							ep.PayloadData, false, true, true);
						block = JocysCom.ClassLibrary.Text.Helper.IdentText(block, 4, " ");
						writer.WriteLine(block);
						writer.WriteLine("");
					}
				}
			}
			var mi = Program.MonitorItem;

			// ------------------------------------------------------------
			// If direction specified, but wrong type then return.
			if (mi.FilterDirection != TrafficDirection.None && direction != mi.FilterDirection)
				return;
			// If port is specified but wrong number then return.
			if (mi.FilterDestinationPort > 0 && tp.DestinationPort != mi.FilterDestinationPort)
				return;
			// If process name specified.
			if (!string.IsNullOrEmpty(mi.FilterProcessName))
			{
				//var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				//var tcpListenters = ipGlobalProperties.GetActiveTcpListeners();
				//var udpListenters = ipGlobalProperties.GetActiveUdpListeners();
				//var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
				//var myEnum = tcpConnInfoArray.GetEnumerator();
				//while (myEnum.MoveNext())
				//{
				//	var tcpInfo = (TcpConnectionInformation)myEnum.Current;
				//	Console.WriteLine("Port {0} {1} {2} ", tcpInfo.LocalEndPoint, tcpInfo.RemoteEndPoint, tcpInfo.State);
				//	//usedPort.Add(TCPInfo.LocalEndPoint.Port);
				//}
			}
			var pluginType = mi.GetType();
			var voiceItem = (VoiceListItem)Activator.CreateInstance(pluginType);
			voiceItem.Load(ip, tp);
			// If data do not contain XML message then return.
			if (!voiceItem.IsVoiceItem)
			{
				return;
			}
			var allowToAdd = true;
			// If message contains sequence number...
			if (sequenceNumber > 0)
			{
				lock (SequenceNumbersLock)
				{
					// Cleanup sequence list by removing oldest numbers..
					while (SequenceNumbers.Count > 10) SequenceNumbers.RemoveAt(0);
					// If number is not unique then...
					if (SequenceNumbers.Contains(sequenceNumber))
					{
						// Don't allow to add the message.
						allowToAdd = false;
					}
					else
					{
						// Store sequence number for the future checks.
						SequenceNumbers.Add(sequenceNumber);
					}
				}
			}
			if (allowToAdd)
			{
				// If default capture filter.
				if (!IsDetailFilter)
				{
					// Restrict filter to improve speed.
					SetFilter(voiceItem, true);
				}
				// Add wow item to the list. Use Invoke to make it Thread safe.
				Audio.Global.addVoiceListItem(voiceItem);
			}
		}

		#region Capture Filters

		public List<string> LastFilters = new List<string>();
		bool IsDetailFilter;

		public void SetFilter(VoiceListItem item, bool detailFilter = false)
		{
			LastFilters = GetFilters(item);
			IsDetailFilter = detailFilter;
			var filter = string.Join(" and ", LastFilters);
			var deviceType = "Socket";
			foreach (var device in CaptureDevices)
			{
				if (device is WinPcapDevice)
				{
					deviceType = "WinPcap";
				}
				device.Filter = filter;
			}
			OnStatusChanged(null, string.Format("{0} Filters: {1}", deviceType, LastFilters.Count));
		}

		List<string> GetFilters(VoiceListItem item)
		{
			var filters = new List<string>();
			string f;
			var defaultItem = Program.MonitorItem;
			if (defaultItem == null)
			{
				filters.Add("ip");
				return filters;
			}
			// Protocol type.
			if (defaultItem.FilterProtocol != ProtocolType.Unspecified)
			{
				f = string.Format("{0}", defaultItem.FilterProtocol).ToLower();
				filters.Add(f);
			}
			// UDP/TCP Port.
			if (defaultItem.SourcePort > 0)
			{
				f = string.Format("src port {0}", defaultItem.SourcePort);
				filters.Add(f);
			}
			if (defaultItem.DestinationPort > 0)
			{
				f = string.Format("dst port {0}", defaultItem.DestinationPort);
				filters.Add(f);
			}
			// If source address is set and local then...
			if (item.SourceAddress != null && IpAddresses.Contains(item.SourceAddress))
			{
				f = string.Format("src host {0}", item.SourceAddress);
				filters.Add(f);
			}
			// If destination address is set and local then...
			if (item.DestinationAddress != null && IpAddresses.Contains(item.DestinationAddress))
			{
				f = string.Format("dst host {0}", item.DestinationAddress);
				filters.Add(f);
			}
			return filters;
		}

		#endregion

		static void OnEvent<T>(EventHandler<EventArgs<T>> handler, T data)
		{
			if (handler != null)
				ControlsHelper.Invoke(handler, null, new EventArgs<T>(data));
		}

		public override void Dispose()
		{
			base.Dispose();
			lock (WriterLock)
			{
				foreach (var device in CaptureDevices)
					device.Close();
				if (Writer != null)
				{
					Writer.Dispose();
					Writer = null;
				}
			}
		}

	}
}
