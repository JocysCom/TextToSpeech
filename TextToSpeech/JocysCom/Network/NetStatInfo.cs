using JocysCom.ClassLibrary.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace JocysCom.ClassLibrary.Network
{

	public class NetworkInfo
	{
		public bool IsNetworkAvailable = false;
		public bool IsMobileNicUp = false;
		public bool IsWirelessNicUp = false;
		public List<KeyValue> NicInfo = new List<KeyValue>();
		public string LocalIpAddress = "0.0.0.0";
		public string LocalGatewayIpAddress = "0.0.0.0";
		public string CurrentNicks = "";
	}

	public static class NetworkHelper
	{
		/// <summary>
		/// Original source.
		/// https://code.msdn.microsoft.com/C-Sample-to-list-all-the-4817b58f/sourcecode?fileId=147562&pathId=62315043
		/// </summary>

		public class NativeMethods
		{

			[DllImport("kernel32.dll")]
			internal static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

			[DllImport("psapi.dll", CharSet = CharSet.Unicode)]
			internal static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CloseHandle(IntPtr hObject);

			[DllImport("iphlpapi.dll", SetLastError = true)]
			internal static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tableLength, bool sort, int ipVersion, TCP_TABLE_CLASS tableClass, uint reserved = 0);

			[DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern uint GetExtendedUdpTable(IntPtr udpTable, ref int tableLength, bool sort, int ipVersion, UDP_TABLE_CLASS tableClass, uint reserved = 0);

		}

		public static string GetProcessName(int pid)
		{
			var processHandle = NativeMethods.OpenProcess(0x0400 | 0x0010, false, pid);
			if (processHandle == IntPtr.Zero)
			{
				return null;
			}
			const int lengthSb = 4000;
			var sb = new StringBuilder(lengthSb);
			string result = null;
			if (NativeMethods.GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, lengthSb) > 0)
			{
				result = Path.GetFileName(sb.ToString());
			}
			NativeMethods.CloseHandle(processHandle);
			return result;
		}

		public static string GetExtendedTable(bool sorted)
		{
			var tcpList = GetExtendedTcpTable(true);
			var udpList = GetExtendedUdpTable(true);
			var list = new List<NetStatInfo>();
			list.AddRange(tcpList);
			list.AddRange(udpList);
			var items = list.Select(x => x.ToLineString());
			var result = string.Join("\r\n", items);
			return NetStatInfo.ToHeaderLine() + result;
		}

		public static NetStatInfo[] GetExtendedTcpTable(bool sorted)
		{
			IntPtr table = IntPtr.Zero;
			int tableLength = 0;
			NetStatInfo[] rows = null;
			int AfInet = 2;
			if (NativeMethods.GetExtendedTcpTable(table, ref tableLength, sorted, AfInet, TCP_TABLE_CLASS.OwnerPidAll, 0) != 0)
			{
				try
				{
					table = Marshal.AllocHGlobal(tableLength);
					if (NativeMethods.GetExtendedTcpTable(table, ref tableLength, true, AfInet, TCP_TABLE_CLASS.OwnerPidAll, 0) == 0)
					{
						var mibTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(table, typeof(MIB_TCPTABLE_OWNER_PID));
						rows = new NetStatInfo[mibTable.NumEntries];
						IntPtr rowPtr = (IntPtr)((long)table + Marshal.SizeOf(mibTable.NumEntries));
						for (int i = 0; i < mibTable.NumEntries; ++i)
						{
							var mibRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
							var row = new NetStatInfo();
							row.Proto = ProtocolType.Tcp;
							row.State = mibRow.state;
							row.ProcessId = mibRow.owningPid;
							row.LocalAddress = new IPAddress(mibRow.localAddr);
							row.LocalPort = BitConverter.ToUInt16(new byte[2] { mibRow.localPort[1], mibRow.localPort[0] }, 0);
							row.RemoteAddress = new IPAddress(mibRow.remoteAddr);
							row.RemotePort = BitConverter.ToUInt16(new byte[2] { mibRow.remotePort[1], mibRow.remotePort[0] }, 0);
							if (row.ProcessId > 0)
							{
								row.ProcessName = GetProcessName(row.ProcessId);
							}
							rows[i] = row;
							rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(typeof(MIB_TCPROW_OWNER_PID)));
						}
					}
				}
				finally
				{
					if (table != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(table);
					}
				}
			}

			return rows;
		}


		public static NetStatInfo[] GetExtendedUdpTable(bool sorted)
		{
			IntPtr table = IntPtr.Zero;
			int tableLength = 0;
			NetStatInfo[] rows = null;
			int AfInet = 2;
			if (NativeMethods.GetExtendedUdpTable(table, ref tableLength, sorted, AfInet, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0) != 0)
			{
				try
				{
					table = Marshal.AllocHGlobal(tableLength);
					if (NativeMethods.GetExtendedUdpTable(table, ref tableLength, true, AfInet, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0) == 0)
					{
						var mibTable = (MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(table, typeof(MIB_UDPTABLE_OWNER_PID));
						rows = new NetStatInfo[mibTable.NumEntries];
						IntPtr rowPtr = (IntPtr)((long)table + Marshal.SizeOf(mibTable.NumEntries));
						for (int i = 0; i < mibTable.NumEntries; ++i)
						{
							var mibRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_UDPROW_OWNER_PID));
							var row = new NetStatInfo();
							row.Proto = ProtocolType.Udp;
							row.ProcessId = mibRow.owningPid;
							row.LocalAddress = new IPAddress(mibRow.localAddr);
							row.LocalPort = BitConverter.ToUInt16(new byte[2] { mibRow.localPort[1], mibRow.localPort[0] }, 0);
							row.RemoteAddress = IPAddress.Any;
							row.RemotePort = 0;
							if (row.ProcessId > 0)
							{
								row.ProcessName = GetProcessName(row.ProcessId);
							}
							rows[i] = row;
							rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(typeof(MIB_UDPROW_OWNER_PID)));
						}
					}
				}
				finally
				{
					if (table != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(table);
					}
				}
			}

			return rows;
		}

		#region TCP socket enumerations and structures

		/// <summary>
		/// <see cref="http://msdn2.microsoft.com/en-us/library/aa366386.aspx"/>
		/// </summary>
		public enum TCP_TABLE_CLASS
		{
			BasicListener,
			BasicConnections,
			BasicAll,
			OwnerPidListener,
			OwnerPidConnections,
			OwnerPidAll,
			OwnerModuleListener,
			OwnerModuleConnections,
			OwnerModuleAll,
		}

		/// <summary>
		/// The structure contains information that describes an IPv4 TCP connection with 
		/// IPv4 addresses, ports used by the TCP connection, and the specific process ID 
		/// (PID) associated with connection. 
		/// <see cref="http://msdn2.microsoft.com/en-us/library/aa366921.aspx"/>
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct MIB_TCPTABLE_OWNER_PID
		{
			public uint NumEntries;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public MIB_TCPROW_OWNER_PID[] table;
		}

		/// <summary>
		/// The structure contains information that describes an IPv4 TCP connection with 
		/// IPv4 addresses, ports used by the TCP connection, and the specific process ID 
		/// (PID) associated with connection. 
		/// <see cref="http://msdn2.microsoft.com/en-us/library/aa366913.aspx"/>
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct MIB_TCPROW_OWNER_PID
		{
			public TcpState state;
			public uint localAddr;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] localPort;
			public uint remoteAddr;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] remotePort;
			public int owningPid;
		}

		#endregion

		#region  UDP socket enumerations and structures

		/// <summary>
		/// Enum to define the set of values used to indicate the type of table returned by calls 
		/// made to the function GetExtendedUdpTable. 
		/// </summary>
		public enum UDP_TABLE_CLASS
		{
			UDP_TABLE_BASIC,
			UDP_TABLE_OWNER_PID,
			UDP_TABLE_OWNER_MODULE
		}

		/// <summary> 
		/// The structure contains an entry from the User Datagram Protocol (UDP) listener 
		/// table for IPv4 on the local computer. The entry also includes the process ID 
		/// (PID) that issued the call to the bind function for the UDP endpoint. 
		/// </summary> 
		[StructLayout(LayoutKind.Sequential)]
		public struct MIB_UDPROW_OWNER_PID
		{
			public uint localAddr;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] localPort;
			public int owningPid;
		}

		/// <summary> 
		/// The structure contains the User Datagram Protocol (UDP) listener table for IPv4 
		/// on the local computer. The table also includes the process ID (PID) that issued 
		/// the call to the bind function for each UDP endpoint. 
		/// </summary> 
		[StructLayout(LayoutKind.Sequential)]
		public struct MIB_UDPTABLE_OWNER_PID
		{
			public uint NumEntries;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct,
				SizeConst = 1)]
			public MIB_UDPROW_OWNER_PID[] table;
		}

		#endregion

	}

	public class NetStatInfo
	{
		public ProtocolType Proto { get; set; }
		public IPAddress LocalAddress { get; set; }
		public int LocalPort { get; set; }
		public IPAddress RemoteAddress { get; set; }
		public int RemotePort { get; set; }
		public TcpState? State { get; set; }
		public int ProcessId;
		public string ProcessName;
		public string ToLineString()
		{
			return string.Format("{0} {1,5} {2,-11} {3,-21} {4,-21} {5}",
				Proto.ToString().ToUpper(), ProcessId, State, new IPEndPoint(LocalAddress, LocalPort), new IPEndPoint(RemoteAddress, RemotePort), ProcessName);

		}
		public static string ToHeaderLine()
		{
			return
				"PRO PID   State       Local Endpoint        Remote Endpoint      \r\n" +
				"--- ----- ----------- --------------------- ---------------------\r\n";
		}

		#region Check Network

		public static List<IPAddress> GetAvailableIps()
		{
			var list = new List<IPAddress>();
			var nics = NetworkInterface.GetAllNetworkInterfaces();
			for (int i = 0; i < nics.Length; i++)
			{
				var ni = nics[i];
				var properties = ni.GetIPProperties();
				if (ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
				{
					// Loop trough IP address properties.
					for (var a = 0; a < properties.UnicastAddresses.Count; a++)
					{
						var address = properties.UnicastAddresses[a].Address;
						list.Add(address);
					}
				}
			}
			return list;
		}
		public static bool IsExcludedIp(string ip, string ipList = null)
		{
			var ips = ipList ?? "";
			string[] list = ips.Split(';', ',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
			foreach (string item in list)
			{
				var rxString = item.Replace(".", "\\.").Replace("*", ".*");
				var rx = new Regex("^" + rxString + "$");
				var match = rx.Match(ip);
				if (match.Success) return true;
			}
			return false;
		}

		/// <summary>
		/// This method is time consuming and would freeze MDT application if you run it on main thread.
		/// </summary>
		public static NetworkInfo CheckNetwork(string excludeIps = null)
		{
			var info = new NetworkInfo();
			var nicsList = new List<String>();
			var nics = NetworkInterface.GetAllNetworkInterfaces();
			var ips = new List<KeyValuePair<string, int>>();
			for (int i = 0; i < nics.Length; i++)
			{
				var ni = nics[i];
				var badTypes = new NetworkInterfaceType[] { NetworkInterfaceType.Tunnel, NetworkInterfaceType.Loopback };
				if (badTypes.Contains(ni.NetworkInterfaceType)) continue;
				var nicsSb = new StringBuilder();
				nicsSb.AppendFormat("    Name = {0}, Status = {1}", ni.Name, ni.OperationalStatus);
				var sb = new StringBuilder();
				var properties = ni.GetIPProperties();
				FillAdapter(sb, ni);
				info.NicInfo.Add(new KeyValue(ni.Description, sb.ToString()));
				if (ni.OperationalStatus == OperationalStatus.Up)
				{
					var desc = ni.Description;
					// If state is still off then...
					if (!info.IsMobileNicUp)
					{
						info.IsMobileNicUp = desc.Contains("HSPA Network");
					}
					// If state is still off then...
					if (!info.IsWirelessNicUp)
					{
						info.IsWirelessNicUp = desc.Contains("802.11") || desc.Contains("802.11");
					}
					// Loop trough IP address properties.
					for (var a = 0; a < properties.UnicastAddresses.Count; a++)
					{
						var address = properties.UnicastAddresses[a].Address;
						// Make sure its IP4 version.
						if (address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(address))
						{
							nicsSb.AppendFormat(", Address = {0}", address);
							if (excludeIps == null || !IsExcludedIp(address.ToString(), excludeIps))
							{
								// Normal IP4 address was found.
								info.IsNetworkAvailable = true;
								// More configuration = higher priority of IP address.
								var priority =
									properties.GatewayAddresses.Count +
									properties.DnsAddresses.Count +
									properties.DhcpServerAddresses.Count +
									properties.WinsServersAddresses.Count;
								ips.Add(new KeyValuePair<string, int>(address.ToString(), priority));
							}
						}
					}
				}
				nicsList.Add(nicsSb.ToString());
			}
			info.CurrentNicks = string.Join("\r\n", nicsList);
			var ipAddress = ips.OrderByDescending(x => x.Value).Select(x => x.Key).FirstOrDefault();
			if (ipAddress != null) info.LocalIpAddress = ipAddress;
			return info;
		}

		static void FillAdapter(StringBuilder sb, NetworkInterface adapter)
		{
			var properties = adapter.GetIPProperties();
			var ph = adapter.GetPhysicalAddress().GetAddressBytes().Select(x => x.ToString("X2")).ToArray();
			sb.AppendFormat("  Interface Type. . . . . . . . . . . . . . : {0}\r\n", adapter.NetworkInterfaceType);
			sb.AppendFormat("  Operational Status. . . . . . . . . . . . : {0}\r\n", adapter.OperationalStatus);
			sb.AppendFormat("  Physical Address. . . . . . . . . . . . . : {0}\r\n", string.Join("-", ph));
			FillIPAddresses(sb, properties, AddressFamily.InterNetwork);
			// The following information is not useful for loop-back adapters.
			if (adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
			{
				sb.AppendFormat("  DNS Suffix. . . . . . . . . . . . . . . . : {0}", properties.DnsSuffix);
			}
		}

		static void FillIPAddresses(StringBuilder sb, IPInterfaceProperties addresses, AddressFamily family)
		{
			var list = addresses.UnicastAddresses.Where(x => family == AddressFamily.Unspecified || x.Address.AddressFamily == family).ToArray();
			for (int i = 0; i <= list.Count() - 1; i++)
			{
				if (family == AddressFamily.InterNetwork)
				{
					sb.AppendFormat("  IPv4 Address. . . . . . . . . . . . . . . : {0}\r\n", list[i].Address);
					sb.AppendFormat("  Subnet Mask . . . . . . . . . . . . . . . : {0}\r\n", list[i].IPv4Mask);
				}
				else if (family == AddressFamily.InterNetworkV6)
				{
					sb.AppendFormat("  IPv6 Address. . . . . . . . . . . . . . . : {0}\r\n", list[i].Address);
				}
			}
		}

		#endregion


	}

}
