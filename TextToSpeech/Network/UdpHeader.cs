using System.Net;
using System;
using System.IO;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class UdpHeader: IPortsHeader
    {
        ushort _SourcePort;
        public ushort SourcePort { get { return _SourcePort; } }

        ushort _DestinationPort;
        public ushort DestinationPort { get { return _DestinationPort; } }

        ushort _Checksum;
        public ushort Checksum { get { return _Checksum; } }

        byte _HeaderLength;
        public byte HeaderLength { get { return _HeaderLength; } }

        byte[] _Data;
        public byte[] Data { get { return _Data; } }

        public UdpHeader(byte[] buffer, int index, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            // UDP header will be 8 bytes.
            _HeaderLength = 8;
            // First 2 bytes have the source port.
            _SourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the destination port.
            _DestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the length of the UDP packet. 
            var length = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the checksum. 
            _Checksum = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Calculate data length (total length of the TCP packet - header length).
            int dataLength = (int)(count - _HeaderLength);
            // Create new array to store data.
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
            node.Text = "UDP";
            node.Nodes.Add("Source Port: " + _SourcePort);
            node.Nodes.Add("Destination Port: " + _DestinationPort);
            node.Nodes.Add("Header Length: " + _HeaderLength);
            node.Nodes.Add("Data Length: " + _Data.Length);
            node.Nodes.Add(string.Format("Checksum: 0x{0:X2} ({0})", _Checksum));
            return node;
        }
    }
}
