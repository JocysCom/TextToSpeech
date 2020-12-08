using PacketDotNet;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class WowListItem : VoiceListItem
	{
		public WowListItem()
		{
			Name = "WoW";
			Process = new string[] { "wow.exe", "wow-64.exe" };
			FilterDestinationPort = 3724;
			FilterDirection = TrafficDirection.Out;
			FilterProtocol = System.Net.Sockets.ProtocolType.Tcp;
			// Encoding used when converting bytes to text.
			DataEncoding = Encoding.UTF8;
		}

		private Encoding DataEncoding;
		public const string startTag = "<message";
		public const string endTag1 = "</message>";
		public const string endTag2 = " />";

		private string ExtractMessage(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
				return null;
			var startTagBytes = DataEncoding.GetBytes(startTag);
			// Find start tag.
			var start = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, startTagBytes);
			if (start == -1)
				return null;
			// Find end tag.
			var endTag1Bytes = DataEncoding.GetBytes(endTag1);
			var end = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, endTag1Bytes, start);
			// If ending was found then...
			if (end > -1)
				return DataEncoding.GetString(bytes, start, end + endTag1Bytes.Length - start);
			// Try to find alternative ending.
			var endTag2Bytes = DataEncoding.GetBytes(endTag2);
			end = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, endTag2Bytes, start);
			// If ending was found then...
			if (end > -1)
				return DataEncoding.GetString(bytes, start, end + endTag2Bytes.Length - start);
			return null;
		}

		private static string ExtractMessage(string text)
		{
			if (string.IsNullOrEmpty(text))
				return null;
			// Find start tag.
			var start = text.IndexOf(startTag, System.StringComparison.Ordinal);
			if (start == -1)
				return null;
			// Find end tag.
			var end = text.IndexOf(endTag1, start, System.StringComparison.Ordinal);
			// If ending was found then...
			if (end > -1)
				return text.Substring(start, end + endTag1.Length - start);
			// Try to find alternative ending.
			end = text.IndexOf(endTag2, start, System.StringComparison.Ordinal);
			// If ending was found then...
			if (end > -1)
				return text.Substring(start, end + endTag2.Length - start);
			return null;
		}

		/// <summary>
		/// Load message from bytes captured from from network.
		/// </summary>
		/// <returns>Null if message not found.</returns>
		public override bool Load(IpPacket ipHeader, TcpPacket tcpHeader)
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
		public override bool Load(string text)
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


	}
}
