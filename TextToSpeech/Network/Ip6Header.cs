using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace JocysCom.TextToSpeech.Monitor.Network
{

	public class Ip6Header : IIpHeader
	{
		/// <summary>
		/// Version of the protocol. For IPv6, the version is 6.
		/// </summary>
		public byte Version { get { return _Version; } }
		byte _Version;

		/// <summary>
		/// Header Length. For IPv6, the length is always 40.
		/// </summary>
		public byte HeaderLength { get { return _HeaderLength; } }
		byte _HeaderLength;

		/// <summary>
		/// Intended for originating nodes and forwarding routers
		/// to identify and distinguish between different classes or
		/// priorities of IPv6 packets.
		/// </summary>
		public byte TrafficClass { get { return _TrafficClass; } }
		byte _TrafficClass;

		/// <summary>
		/// Total packer Length (header + payload).
		/// </summary>
		public ushort TotalLength { get { return _TotalLength; } }
		ushort _TotalLength;

		/// <summary>
		/// Defines how traffic is handled and identified.
		/// </summary>
		public uint FlowLabel { get { return _FlowLabel; } }
		uint _FlowLabel;

		/// <summary>
		/// Identifies the header type immediately following the IPv6 header.
		/// </summary>
		public ProtocolType Protocol { get { return _Protocol; } }
		ProtocolType _Protocol;

		/// <summary>
		/// Number of network segments, also known as links or subnets,
		/// on which the packet is allowed to travel before being discarded by a router. 
		/// </summary>
		public byte HopLimit { get { return _HopLimit; } }
		public byte _HopLimit;

		/// <summary>
		/// IPv6 address of the original source of the IPv6 packet.
		/// </summary>
		public IPAddress SourceAddress { get { return _SourceAddress; } }
		IPAddress _SourceAddress;

		/// <summary>
		/// IPv6 address of the intermediate or final destination of the IPv6 packet.
		/// </summary>
		public IPAddress DestinationAddress { get { return _DestinationAddress; } }
		IPAddress _DestinationAddress;

		/// <summary>
		/// Payload/Data.
		/// </summary>
		public byte[] Data { get { return _Data; } }
		byte[] _Data;

		public static bool TryParse(byte[] buffer, int index, int count, out Ip6Header header)
		{
			header = null;
			byte minimumSize = 40;
			if (buffer == null || buffer.Length < minimumSize)
			{
				return false;
			}
			var h = new Ip6Header();
			// Create MemoryStream out of the received bytes.
			MemoryStream memoryStream = new MemoryStream(buffer, index, count);
			//Create a BinaryReader out of the MemoryStream.
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			//
			//  RFC 2460 - IP6 Header Format
			//
			//  0               1               2               3            
			//  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |Version| Traffic Class |           Flow Label                  |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |         Payload Length        |  Next Header  |   Hop Limit   |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |                                                               |
			//  +                                                               +
			//  |                                                               |
			//  +                         Source Address                        +
			//  |                                                               |
			//  +                                                               +
			//  |                                                               |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |                                                               |
			//  +                                                               +
			//  |                                                               |
			//  +                      Destination Address                      +
			//  |                                                               |
			//  +                                                               +
			//  |                                                               |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//
			var b0 = binaryReader.ReadByte();
			var b1 = binaryReader.ReadByte();
			var b2 = binaryReader.ReadByte();
			var b3 = binaryReader.ReadByte();
			// First byte of the IP header contain version and part of Traffic Class
			h._Version = (byte)(b0 >> 4);
			if (h.Version != 6)
			{
				return false;
			}
			// IP6 Header length is 40 bytes.
			h._HeaderLength = minimumSize;
			// Traffic Class
			h._TrafficClass = (byte)(((b0 & 0xF) << 4) | (b1 >> 4));
			// Flow value for the packet.
			uint flow = (byte)(((b1 & 0xF) << 16) | (b2 << 8) | b3);
			h._FlowLabel = (uint)IPAddress.NetworkToHostOrder(flow);
			// Payload (data length).
			short payload = binaryReader.ReadInt16();
			ushort dataLength = (ushort)IPAddress.NetworkToHostOrder(payload);
			h._TotalLength = (ushort)(dataLength + h._HeaderLength);
			// Next byte represents the protocol encapsulated in the datagram.
			h._Protocol = (ProtocolType)binaryReader.ReadByte();
			h._HopLimit = binaryReader.ReadByte();
			// Next 16 bytes have source IP address.
			h._SourceAddress = new IPAddress(binaryReader.ReadBytes(16));
			// Next 16 bytes have destination IP address.
			h._DestinationAddress = new IPAddress(binaryReader.ReadBytes(16));
			if (dataLength <= (buffer.Length - h._HeaderLength))
			{
				// Create new array to store data.
				var data = new byte[dataLength];
				// Copy the data carried by the datagram.
				Array.Copy(buffer, h._HeaderLength, data, 0, dataLength);
				h._Data = data;
			}
			else
			{
				h._Data = new byte[0];
			}
			header = h;
			return true;
		}

		/// <summary>
		/// Returns the information contained in the header as a tree node
		/// </summary>
		public TreeNode ToTreeNode()
		{
			TreeNode node = new TreeNode();
			node.Text = "IP";
			node.Nodes.Add("Version: IPv" + _Version);
			node.Nodes.Add("Header Length: " + _HeaderLength);
			node.Nodes.Add("Data Length: " + _Data.Length);
			node.Nodes.Add("Total Length: " + _TotalLength);
			node.Nodes.Add(string.Format("TrafficClass: 0x{0:X2} ({0})", _TrafficClass));
			node.Nodes.Add("Flow Label: " + _FlowLabel);
			node.Nodes.Add("Next Header: " + _Protocol);
			node.Nodes.Add("Hop Limit:" + HopLimit);
			node.Nodes.Add("Source: " + _SourceAddress.ToString());
			node.Nodes.Add("Destination: " + _DestinationAddress.ToString());
			return node;
		}

	}
}
