using JocysCom.TextToSpeech.Monitor.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.PlugIns
{
	public class WowListItem : VoiceListItem
	{
		public WowListItem()
		{
			_PortNumber = 3724;
			_Name = "WoW";
		}

		public override void Load(IpHeader ipHeader, TcpHeader tcpHeader)
		{
			_IpHeader = ipHeader;
			_TcpHeader = tcpHeader;
			// Convert bytes to ASCII text. ASCII encoding is used in order to find property position of <message></message> tags inside byte array.
			var text = System.Text.Encoding.ASCII.GetString(ipHeader.Data);
			loadFromText(text, ipHeader.Data);
		}

		void loadFromText(string text, byte[] data = null)
		{
			if (text.Contains("<message"))
			{
				var endTag = "</message>";
				// Find start of the voice tag.
				var start = text.IndexOf("<message");
				// Find end of the voice tag.
				var end = text.IndexOf(endTag, start);
				if (end == -1)
				{
					endTag = " />";
					end = text.IndexOf(endTag, start);
				}
				if (end > start)
				{
					_IsVoiceItem = true;
					// If original bytes are not available then...
					if (data == null)
					{
						// Get voice text from original text block.
						_VoiceXml = text.Substring(start, end - start + endTag.Length);
					}
					else
					{
						// Get voice text from original bytes as UTF8 text.
						_VoiceXml = System.Text.Encoding.UTF8.GetString(data, start, end - start + endTag.Length);
					}
				}
			}
		}

	}
}
