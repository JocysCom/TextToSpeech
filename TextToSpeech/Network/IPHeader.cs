using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace JocysCom.TextToSpeech.Monitor.Network
{

    public class IpHeader
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

        public IpHeader(byte[] buffer, int index, int count)
        {
            // Create MemoryStream out of the received bytes.
            MemoryStream memoryStream = new MemoryStream(buffer, index, count);
            //Vreate a BinaryReader out of the MemoryStream.
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            // First byte of the IP header contain version and header length.
            byte versionAndHeaderLength = binaryReader.ReadByte();
            // Upper 4 bits have the version.
            _Version = (byte)(versionAndHeaderLength >> 4);
            // Lower 4 bits have the header length. Multiply by four to get the exact header length.
            _HeaderLength = (byte)((versionAndHeaderLength & 0xF) * 4);
            // Next byte contain the differentiated services.
            _DifferentiatedServices = binaryReader.ReadByte();
            // Next 2 bytes have total length (header + message) of the datagram.
            _TotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have identification.
            _Identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have flags and fragmentation offset.
            ushort flagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Upper 3 bits have the flags.
            _Flags = (IpHeaderFlags)(flagsAndOffset >> 13);
            // LOwer 13 bits have the fragmentation offset.
            _FragmentationOffset = (ushort)(flagsAndOffset & 0x1FFF);
            // Next byte have the TTL value.
            _TTL = binaryReader.ReadByte();
            // Next byte represnts the protocol encapsulated in the datagram.
            _Protocol = (ProtocolType)binaryReader.ReadByte();
            // Next 2 bytes have checksum of the header.
            _Checksum = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 4 bytes have source IP address.
            _SourceAddress = new IPAddress(binaryReader.ReadBytes(4));
            // Next 4 bytes have destination IP address.
            _DestinationAddress = new IPAddress(binaryReader.ReadBytes(4));
            // Calculate data length (total length - header length).
            int dataLength = (int)(TotalLength - _HeaderLength);
            // Create new arrray to store data.
            _Data = new byte[dataLength];
            // Copy the data carried by the datagram.
            Array.Copy(buffer, _HeaderLength, _Data, 0, dataLength);
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
            node.Nodes.Add(string.Format("Differntiated Services: 0x{0:X2} ({0})", _DifferentiatedServices));
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
