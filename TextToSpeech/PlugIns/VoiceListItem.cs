using PacketDotNet;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class VoiceListItem
	{
		public VoiceListItem()
		{
			FilterDirection = TrafficDirection.Out;
			FilterProtocol = System.Net.Sockets.ProtocolType.Tcp;
			// Encoding used when converting bytes to text.
			DataEncoding = Encoding.UTF8;
		}

		protected Encoding DataEncoding;
		protected string StartTag = "<message";
		protected string EndTag1 = "</message>";
		protected string EndTag2 = " />";

		protected string ExtractMessage(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
				return null;
			var startTagBytes = DataEncoding.GetBytes(StartTag);
			// Find start tag.
			var start = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, startTagBytes);
			if (start == -1)
				return null;
			// Find end tag.
			var endTag1Bytes = DataEncoding.GetBytes(EndTag1);
			var end = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, endTag1Bytes, start);
			// If ending was found then...
			if (end > -1)
				return DataEncoding.GetString(bytes, start, end + endTag1Bytes.Length - start);
			// Try to find alternative ending.
			var endTag2Bytes = DataEncoding.GetBytes(EndTag2);
			end = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, endTag2Bytes, start);
			// If ending was found then...
			if (end > -1)
				return DataEncoding.GetString(bytes, start, end + endTag2Bytes.Length - start);
			return null;
		}

		protected string ExtractMessage(string text)
		{
			if (string.IsNullOrEmpty(text))
				return null;
			// Find start tag.
			var start = text.IndexOf(StartTag, System.StringComparison.Ordinal);
			if (start == -1)
				return null;
			// Find end tag.
			var end = text.IndexOf(EndTag1, start, System.StringComparison.Ordinal);
			// If ending was found then...
			if (end > -1)
				return text.Substring(start, end + EndTag1.Length - start);
			// Try to find alternative ending.
			end = text.IndexOf(EndTag2, start, System.StringComparison.Ordinal);
			// If ending was found then...
			if (end > -1)
				return text.Substring(start, end + EndTag2.Length - start);
			return null;
		}

		/// <summary>
		/// Load message from bytes captured from from network.
		/// </summary>
		/// <returns>Null if message not found.</returns>
		public virtual bool Load(IpPacket ipHeader, TcpPacket tcpHeader)
		{
			if (tcpHeader == null)
				return false;
			_IpHeader = ipHeader;
			_TcpHeader = tcpHeader;
			var s = ExtractMessage(tcpHeader.PayloadData);
			_Load(s);
			return s != null;
		}

		/// <summary>
		/// Load message from text.
		/// </summary>
		/// <returns>Null if message not found.</returns>
		public virtual bool Load(string text)
		{
			var s = ExtractMessage(text);
			_Load(s);
			return s != null;
		}

		private void _Load(string text)
		{
			_IsVoiceItem = true;
			_VoiceXml = text;
		}

		internal IpPacket _IpHeader;
		internal TcpPacket _TcpHeader;

		#region GridView columns.

		public int TotalLength => _IpHeader?.TotalLength ?? 0;
		public IPAddress SourceAddress => _IpHeader?.SourceAddress;
		public ushort SourcePort => _TcpHeader?.SourcePort ?? 0;
		public IPAddress DestinationAddress => _IpHeader?.DestinationAddress;
		public ushort DestinationPort => _TcpHeader?.DestinationPort ?? 0;
		public int DataLength => _TcpHeader?.PayloadData?.Length ?? 0;
		public uint SequenceNumber => _TcpHeader?.SequenceNumber ?? 0; 
		public int VoiceXmlLength => _VoiceXml?.Length ?? 0; 

		#endregion

		public bool IsVoiceItem => _IsVoiceItem;
		internal bool _IsVoiceItem;

		public string VoiceXml => _VoiceXml;
		internal string _VoiceXml;

		public string Name { get; set; }

		public string[] Process { get; set; }

		#region Listen Filter

		public int FilterDestinationPort { get; set; }
		public int FilterSourcePort { get; set; }
		public TrafficDirection FilterDirection { get; set; }
		public ProtocolType FilterProtocol { get; set; }
		public string FilterProcessName { get; set; }

		#endregion
	

	}

}
