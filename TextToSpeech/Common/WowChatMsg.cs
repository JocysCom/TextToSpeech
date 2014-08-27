using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace JocysCom.WoW.TextToSpeech
{
    public class WowChatMsg
    {

        byte Byte1;
        byte Sequence;
        byte Byte3;
        uint Unknown1;
        byte[] _Head1;
        ushort MessageLength;
        ushort Unknown2;
        string Message;

        public WowChatMsg(byte[] buffer, int index, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            var dataString = JocysCom.ClassLibrary.Text.Helper.BytesToStringBlock(buffer, true, true, true);

            Byte1 = binaryReader.ReadByte();
            Sequence = binaryReader.ReadByte();
            Byte3 = binaryReader.ReadByte();
            Unknown1 = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
            _Head1 = binaryReader.ReadBytes(20);
            MessageLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            Unknown2 = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            Message = System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(MessageLength));
        }

    }
}
