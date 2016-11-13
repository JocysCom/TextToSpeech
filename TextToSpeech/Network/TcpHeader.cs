using System.Net;
using System;
using System.IO;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Network
{
    public class TcpHeader: ITcpUdpHeader
    {
        ushort _SourcePort;
        public ushort SourcePort { get { return _SourcePort; } }

        ushort _DestinationPort;
        public ushort DestinationPort { get { return _DestinationPort; } }

        uint _SequenceNumber;
        public uint SequenceNumber { get { return _SequenceNumber; } }

        uint _AcknowledgmentNumber;
        public uint AcknowledgementNumber { get { return _AcknowledgmentNumber; } }

        byte _HeaderLength;
        public byte HeaderLength { get { return _HeaderLength; } }

        TcpHeaderFlags _Flags;
        public TcpHeaderFlags Flags { get { return _Flags; } }
        
        ushort _WindowSize;
        public ushort WindowSize { get { return _WindowSize; } }

        ushort _Checksum;
        public ushort Checksum { get { return _Checksum; } }

        ushort _UrgentPointer;
        public uint UrgentPointer { get { return _UrgentPointer; } }
        
        byte[] _Data;
        public byte[] Data { get { return _Data; } }
        
        public TcpHeader(byte[] buffer, int index, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            // First 2 bytes have the source port.
            _SourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the destination port.
            _DestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 4 bytes have the sequence number.
            _SequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
            // Next 4 bytes have the acknowledgment number.
            _AcknowledgmentNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
            // Next 2 bytes have the flags and the data offset.
            ushort dataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Upper 4 bits have the header length.
            _HeaderLength = (byte)(dataOffsetAndFlags >> 12);
            // Middle 6 bits are reserved.
            // Lower 6 bits have flags.
            _Flags = (TcpHeaderFlags)(dataOffsetAndFlags & 0x3F);
            // Next 2 bytes have the window size.
            _WindowSize = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the checksum.
            _Checksum = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            // Next 2 bytes have the urgent pointer.
            _UrgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
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
            node.Text = "TCP";
            node.Nodes.Add("Source Port: " + _SourcePort);
            node.Nodes.Add("Destination Port: " + _DestinationPort);
            node.Nodes.Add("Sequence Number: " + _SequenceNumber);
            node.Nodes.Add("Header Length: " + _HeaderLength);
            node.Nodes.Add("Data Length: " + _Data.Length); 
            node.Nodes.Add("Flags: " + _Flags);
            if (_Flags.HasFlag(TcpHeaderFlags.ACK)) node.Nodes.Add("Acknowledgment Number: " + _AcknowledgmentNumber);
            if (_Flags.HasFlag(TcpHeaderFlags.URG)) node.Nodes.Add("Urgent Pointer: " + _UrgentPointer);
            node.Nodes.Add("Window Size: " + _WindowSize);
            node.Nodes.Add(string.Format("Checksum: 0x{0:X2} ({0})", _Checksum));
            return node;
        }

    }
}