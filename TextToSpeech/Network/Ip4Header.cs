using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace JocysCom.TextToSpeech.Monitor.Network
{

	public class Ip4Header : IIpHeader
	{

		byte _Version;
		public byte Version { get { return _Version; } }

		byte _HeaderLength;
		public byte HeaderLength { get { return _HeaderLength; } }

		byte _DifferentiatedServices;
		public byte DifferentiatedServices { get { return _DifferentiatedServices; } }

		ushort _TotalLength;
		public ushort TotalLength { get { return _TotalLength; } }

		ushort _Identification;
		public ushort Identification { get { return _Identification; } }

		IpHeaderFlags _Flags;
		public IpHeaderFlags Flags { get { return _Flags; } }

		ushort _FragmentationOffset;
		public ushort FragmentationOffset { get { return _FragmentationOffset; } }

		byte _TTL;
		public byte TTL { get { return _TTL; } }

		ProtocolType _Protocol;
		public ProtocolType Protocol { get { return _Protocol; } }

		public ushort _Checksum;
		public ushort Checksum { get { return _Checksum; } }

		IPAddress _SourceAddress;
		public IPAddress SourceAddress { get { return _SourceAddress; } }

		IPAddress _DestinationAddress;
		public IPAddress DestinationAddress { get { return _DestinationAddress; } }

		byte[] _Data;
		public byte[] Data { get { return _Data; } }

		public static bool TryParse(byte[] buffer, int index, int count, out Ip4Header header)
		{
			header = null;
			byte minimumSize = 24;
			if (buffer == null || buffer.Length < minimumSize)
			{
				return false;
			}
			var h = new Ip4Header();
			// Create MemoryStream out of the received bytes.
			MemoryStream memoryStream = new MemoryStream(buffer, index, count);
			//Create a BinaryReader out of the MemoryStream.
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			//
			//  RFC 791 - IP4 Header Format
			//
			//  0               1               2               3            
			//  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |Version|  IHL  |Type of Service|          Total Length         |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |         Identification        |Flags|      Fragment Offset    |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |  Time to Live |    Protocol   |         Header Checksum       |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |                       Source Address                          |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |                    Destination Address                        |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//  |                    Options                    |    Padding    |
			//  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
			//
			// First byte of the IP header contain version and header length.
			byte versionAndHeaderLength = binaryReader.ReadByte();
			// Upper 4 bits have the version.
			h._Version = (byte)(versionAndHeaderLength >> 4);
			if (h._Version != 4)
			{
				return false;
			}
			// Lower 4 bits have the header length. Multiply by four to get the exact header length.
			h._HeaderLength = (byte)((versionAndHeaderLength & 0xF) * 4);
			// Next byte contain the differentiated services.
			h._DifferentiatedServices = binaryReader.ReadByte();
			// Next 2 bytes have total length (header + message) of the datagram.
			h._TotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
			// Next 2 bytes have identification.
			h._Identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
			// Next 2 bytes have flags and fragmentation offset.
			ushort flagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
			// Upper 3 bits have the flags.
			h._Flags = (IpHeaderFlags)(flagsAndOffset >> 13);
			// LOwer 13 bits have the fragmentation offset.
			h._FragmentationOffset = (ushort)(flagsAndOffset & 0x1FFF);
			// Next byte have the TTL value.
			h._TTL = binaryReader.ReadByte();
			// Next byte represents the protocol encapsulated in the datagram.
			h._Protocol = (ProtocolType)binaryReader.ReadByte();
			// Next 2 bytes have checksum of the header.
			h._Checksum = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
			// Next 4 bytes have source IP address.
			h._SourceAddress = new IPAddress(binaryReader.ReadBytes(4));
			// Next 4 bytes have destination IP address.
			h._DestinationAddress = new IPAddress(binaryReader.ReadBytes(4));
			// Calculate data length (total length - header length).
			ushort dataLength = (ushort)(h._TotalLength - h._HeaderLength);
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
			node.Nodes.Add(string.Format("Differentiated Services: 0x{0:X2} ({0})", _DifferentiatedServices));
			node.Nodes.Add("Identification: " + _Identification);
			node.Nodes.Add("Flags: " + _Flags);
			node.Nodes.Add("Fragmentation Offset: " + _FragmentationOffset);
			node.Nodes.Add("Time to Live: " + _TTL);
			node.Nodes.Add("Protocol: " + _Protocol.ToString());
			node.Nodes.Add(string.Format("Checksum: 0x{0:X2} ({0})", _Checksum));
			node.Nodes.Add("Source: " + _SourceAddress.ToString());
			node.Nodes.Add("Destination: " + _DestinationAddress.ToString());
			return node;
		}

	}
}
