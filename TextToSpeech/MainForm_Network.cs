using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.Network;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using PacketDotNet;
using SharpPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm
	{

		// The socket which monitors all incoming packets.
		private List<ICaptureDevice> CaptureDevices = new List<ICaptureDevice>();
		// A flag to check if packets are to be monitored or not.
		private bool continueMonitoring = false;
		List<IPAddress> IpAddresses = new List<IPAddress>();
		object monitorLock = new object();

		public void StartNetworkMonitor()
		{
			lock (monitorLock)
			{
				if (!MonitorPortCheckBox.Checked)
				{
					return;
				}
				if (Disposing || IsDisposed || !IsHandleCreated)
				{
					return;
				}
				// If monitor is running already then...
				if (CaptureDevices.Count > 0)
				{
					StateStatusLabel.Text = "Error: Monitoring already. Stop first!";
					return;
				}
				try
				{
					IpAddresses.Clear();
					continueMonitoring = true;
					// Retrieve all capture devices
					if (SettingsManager.Options.UseWinCap)
					{
						var devices = CaptureDeviceList.Instance.ToArray();
						var wcaps = devices.OfType<WinPcapDevice>();
						foreach (var device in wcaps)
						{
							device.OnPacketArrival += Wc_OnPacketArrival;
							device.Open(DeviceMode.Normal);
							device.Filter = "ip";
							foreach (var address in device.Addresses)
							{
								if (address.Addr != null && address.Addr.ipAddress != null)
								{
									IpAddresses.Add(address.Addr.ipAddress);
								}
							}
							CaptureDevices.Add(device);
						}
					}
					else
					{
						var device = new SocPcapDevice();
						device.OnPacketArrival += Wc_OnPacketArrival;
						device.Open();
						device.Filter = "ip";
						foreach (var address in device.Addresses)
						{
							IpAddresses.Add(address);
						}
						CaptureDevices.Add(device);
					}
					// Set default packet filter.
					SetFilter(MonitorItem);
					var ip4c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetwork);
					var ip6c = IpAddresses.Count(x => x.AddressFamily == AddressFamily.InterNetworkV6);
					StateStatusLabel.Text = string.Format("Addresses: {0} IPv4, {1} IPv6", ip4c, ip6c);
					// Retrieve all capture devices
					foreach (var device in CaptureDevices)
					{
						// Start the capturing process
						device.StartCapture();
					}
				}
				catch (Exception ex)
				{
					LastException = ex;
				}
			}
		}

		#region Capture Filters

		List<string> LastFilters = new List<string>();
		bool IsDetailFilter;

		public void SetFilter(VoiceListItem item, bool detailFilter = false)
		{
			LastFilters = GetFilters(item);
			IsDetailFilter = detailFilter;
			var filter = string.Join(" and ", LastFilters);
			var deviceType = "Socket";
			foreach (var device in CaptureDevices)
			{
				if (device is WinPcapDevice)
				{
					deviceType = "WinPcap";
				}
				device.Filter = filter;
			}
			BeginInvoke((MethodInvoker)delegate ()
			{
				FilterStatusLabel.Text = string.Format("{0} Filters: {1}", deviceType, LastFilters.Count);
			});
		}

		List<string> GetFilters(VoiceListItem item)
		{
			var filters = new List<string>();
			string f;
			var defaultItem = MonitorItem;
			if (defaultItem == null)
			{
				filters.Add("ip");
				return filters;
			}
			// Protocol type.
			if (defaultItem.FilterProtocol != ProtocolType.Unspecified)
			{
				f = string.Format("{0}", defaultItem.FilterProtocol).ToLower();
				filters.Add(f);
			}
			// UDP/TCP Port.
			if (defaultItem.SourcePort > 0)
			{
				f = string.Format("src port {0}", defaultItem.SourcePort);
				filters.Add(f);
			}
			if (defaultItem.DestinationPort > 0)
			{
				f = string.Format("dst port {0}", defaultItem.DestinationPort);
				filters.Add(f);
			}
			// If source address is set and local then...
			if (item.SourceAddress != null && IpAddresses.Contains(item.SourceAddress))
			{
				f = string.Format("src host {0}", item.SourceAddress);
				filters.Add(f);
			}
			// If destination address is set and local then...
			if (item.DestinationAddress != null && IpAddresses.Contains(item.DestinationAddress))
			{
				f = string.Format("dst host {0}", item.DestinationAddress);
				filters.Add(f);
			}
			return filters;
		}

		#endregion

		private void Wc_OnPacketArrival(object sender, CaptureEventArgs e)
		{
			if (Disposing || IsDisposed || !IsHandleCreated)
			{
				return;
			}
			if (e.Packet == null || e.Packet.Data == null || e.Packet.Data.Length == 0)
			{
				return;
			}
			if (e.Packet.LinkLayerType != LinkLayers.Ethernet)
			{
				return;
			}
			Packet packet = null;
			try
			{
				packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
			}
			catch (Exception)
			{
				return;
			}
			var ep = packet as EthernetPacket;
			if (ep == null) return;
			if (ep.Type != EthernetPacketType.IpV4 && ep.Type != EthernetPacketType.IpV6) return;
			var ip = ep.PayloadPacket as IpPacket;
			if (ip == null) return;
			var tp = ip.PayloadPacket as TcpPacket;
			if (tp == null) return;
			if (tp.PayloadData.Length == 0) return;
			IPAddress srcIp = ip.SourceAddress;
			IPAddress dstIp = ip.DestinationAddress;
			int srcPort = tp.SourcePort;
			int dstPort = tp.DestinationPort;
			BeginInvoke((Action)(() =>
			{
				lock (PacketsStateStatusLabelLock)
				{
					if (ep.Type == EthernetPacketType.IpV6)
					{
						Ip6PacketsCount++;
					}
					else
					{
						Ip4PacketsCount++;
					}

					PacketsStatusLabel.Text = string.Format("Packets: {0} IPv4, {1} IPv6", Ip4PacketsCount, Ip6PacketsCount);
				}
			}));
			uint sequenceNumber = tp.SequenceNumber;
			// ---------------------------------------------------------------------------
			var sourceIsLocal = IpAddresses.Contains(ip.SourceAddress);
			var destinationIsLocal = IpAddresses.Contains(ip.DestinationAddress);
			var direction = TrafficDirection.Local;
			if (sourceIsLocal && !destinationIsLocal)
			{
				direction = TrafficDirection.Out;
			}
			else if (!sourceIsLocal && destinationIsLocal)
			{
				direction = TrafficDirection.In;
			}
			// IPHeader.Data stores the data being carried by the IP datagram.
			if (SettingsManager.Options.LogEnable)
			{
				var index = -1;
				if (OptionsPanel.SearchPattern != null && OptionsPanel.SearchPattern.Length > 0)
				{
					index = ClassLibrary.Text.Helper.IndexOf(tp.PayloadData, OptionsPanel.SearchPattern, 0);
				}
				if (index > -1)
				{
					// Play "Radio2" sound if "LogEnabled" and "LogSound" check-boxes are checked.
					if (SettingsManager.Options.LogSound)
					{
						var stream = GetIntroSound("Radio2");
						if (stream != null)
						{
							var player = new AudioPlayer(Handle);
							player.ChangeAudioDevice(SettingsManager.Options.PlaybackDevice);
							player.Load(stream);
							player.Play();
						}
					}
					// ---------------------------------------------
					var writer = OptionsPanel.Writer;
					if (writer != null)
					{
						writer.WriteLine("{0:HH:mm:ss.fff}: {1} {2}: {3}:{4} -> {5}:{6} Data[{7}]",
							DateTime.Now,
							ep.Type.ToString().ToUpper(),
							destinationIsLocal ? "In" : "Out",
							ip.SourceAddress,
							tp.SourcePort,
							ip.DestinationAddress,
							tp.DestinationPort,
							tp.PayloadData.Length
						);
						var block = JocysCom.ClassLibrary.Text.Helper.BytesToStringBlock(
							ep.PayloadData, false, true, true);
						block = JocysCom.ClassLibrary.Text.Helper.IdentText(4, block, ' ');
						writer.WriteLine(block);
						writer.WriteLine("");
					}
				}
			}
			// ------------------------------------------------------------
			// If direction specified, but wrong type then return.
			if (MonitorItem.FilterDirection != TrafficDirection.None && direction != MonitorItem.FilterDirection)
			{
				return;
			}
			// If port is specified but wrong number then return.
			if (MonitorItem.FilterDestinationPort > 0 && tp.DestinationPort != MonitorItem.FilterDestinationPort)
			{
				return;
			}
			// If process name specified.
			if (!string.IsNullOrEmpty(MonitorItem.FilterProcessName))
			{
				//var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				//var tcpListenters = ipGlobalProperties.GetActiveTcpListeners();
				//var udpListenters = ipGlobalProperties.GetActiveUdpListeners();
				//var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
				//var myEnum = tcpConnInfoArray.GetEnumerator();
				//while (myEnum.MoveNext())
				//{
				//	var tcpInfo = (TcpConnectionInformation)myEnum.Current;
				//	Console.WriteLine("Port {0} {1} {2} ", tcpInfo.LocalEndPoint, tcpInfo.RemoteEndPoint, tcpInfo.State);
				//	//usedPort.Add(TCPInfo.LocalEndPoint.Port);
				//}
			}
			var pluginType = MonitorItem.GetType();
			var voiceItem = (VoiceListItem)Activator.CreateInstance(pluginType);
			voiceItem.Load(ip, tp);
			// If data do not contain XML message then return.
			if (!voiceItem.IsVoiceItem)
			{
				return;
			}
			var allowToAdd = true;
			// If message contains sequence number...
			if (sequenceNumber > 0)
			{
				lock (SequenceNumbersLock)
				{
					// Cleanup sequence list by removing oldest numbers..
					while (SequenceNumbers.Count > 10) SequenceNumbers.RemoveAt(0);
					// If number is not unique then...
					if (SequenceNumbers.Contains(sequenceNumber))
					{
						// Don't allow to add the message.
						allowToAdd = false;
					}
					else
					{
						// Store sequence number for the future checks.
						SequenceNumbers.Add(sequenceNumber);
					}
				}
			}
			if (allowToAdd)
			{
				// If default capture filter.
				if (!IsDetailFilter)
				{
					// Restrict filter to improve speed.
					SetFilter(voiceItem, true);
				}
				// Add wow item to the list. Use Invoke to make it Thread safe.
				this.Invoke((Action<VoiceListItem>)addVoiceListItem, new object[] { voiceItem });
			}
		}

		public void StopNetworkMonitor()
		{
			lock (monitorLock)
			{
				// If monitor is not running then...
				if (CaptureDevices.Count == 0)
				{
					StateStatusLabel.Text = "Error: Not monitoring. Start monitor first!";
					return;
				}
				foreach (var device in CaptureDevices)
				{
					device.StopCapture();
					device.Close();
				}
				CaptureDevices.Clear();
			}
		}

		object PacketsStateStatusLabelLock = new object();
		long Ip4PacketsCount = 0;
		long Ip6PacketsCount = 0;

		BindingList<VoiceListItem> WowMessageList = new BindingList<VoiceListItem>();

		object SequenceNumbersLock = new object();
		List<uint> SequenceNumbers = new List<uint>();

		bool ScrollMessagesGrid = false;
		object ScrollMessagesGridLock = new object();

		private void addVoiceListItem(VoiceListItem wowItem)
		{
			lock (ScrollMessagesGridLock)
			{
				ScrollMessagesGrid = ScrollGrid();
				// Leave maximum 100 items in the list.
				while (WowMessageList.Count > 100)
				{
					WowMessageList.RemoveAt(0);
				}
				// Add new item at the bottom.
				WowMessageList.Add(wowItem);
			}
			ProcessVoiceTextMessage(wowItem.VoiceXml);

		}

		bool ScrollGrid()
		{
			bool scroll;
			lock (ScrollMessagesGridLock)
			{
				var grid = MessagesDataGridView;
				int firstDisplayed = grid.FirstDisplayedScrollingRowIndex;
				int displayed = grid.DisplayedRowCount(true);
				int lastVisible = (firstDisplayed + displayed) - 1;
				int lastIndex = grid.RowCount - 1;
				int newIndex = firstDisplayed + 1;
				scroll = lastVisible == lastIndex;
			}
			return scroll;
		}

		void ScrollDownGrid(DataGridView grid)
		{
			lock (ScrollMessagesGridLock)
			{
				if (ScrollMessagesGrid) { grid.FirstDisplayedScrollingRowIndex = grid.RowCount - 1; }
			}
		}

		private void MessagesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			ScrollDownGrid(MessagesDataGridView);
		}

		private void MessagesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			ScrollDownGrid(MessagesDataGridView);
		}

		private void SnifferForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (continueMonitoring)
			{
				foreach (var device in CaptureDevices)
				{
					device.Close();
				}
			}
		}

		string _Gender;
		string language;
		VoiceGender gender;
		int _Pitch;
		int _PitchComment;
		int _Rate;
		int _Volume;
		string buffer = "";

		void resetBuffer()
		{
			buffer = "";
		}

		string _PlayerName;
		string _PlayerNameChanged;
		string _PlayerClass;

		void ProcessVoiceTextMessage(string text)
		{
			// If <message.
			if (!text.Contains("<message")) return;
			var v = Serializer.DeserializeFromXmlString<message>(text);
			// Override voice values.
			var name = v.name;
			var overrideVoice = SettingsFile.Current.Defaults.FirstOrDefault(x => x.name == name);
			// if override was not found then...
			if (overrideVoice != null) v.UpdateMissingValuesFrom(overrideVoice);

			//Set gender. "Male"(1), "Female"(2), "Neutral"(3).
			_Gender = string.IsNullOrEmpty(v.gender) || v.gender != "Male" && v.gender != "Female" && v.gender != "Neutral" ? GenderComboBox.Text : v.gender;
			var success = Enum.TryParse(_Gender, out gender);
			IncomingGenderTextBox.Text = string.IsNullOrEmpty(v.gender) ? "" : "gender=\"" + _Gender + "\"";

			// Set language. ----------------------------------------------------------------------------------------------------
			language = v.language;
			IncomingLanguageTextBox.Text = string.IsNullOrEmpty(language) ? "" : "language=\"" + language + "\"";

			// Set voice. ----------------------------------------------------------------------------------------------------
			SelectVoice(v.name, language, gender);
			IncomingNameTextBox.Text = string.IsNullOrEmpty(v.name) ? "" : "name=\"" + v.name + "\"";

			// Set pitch.
			var pitchIsValid = int.TryParse(v.pitch, out _Pitch);
			if (!pitchIsValid)
			{
				if (int.Parse(PitchMinComboBox.Text) > int.Parse(PitchMaxComboBox.Text))
				{
					_Pitch = MainHelper.GetNumber(int.Parse(PitchMaxComboBox.Text), int.Parse(PitchMinComboBox.Text), "pitch", v.name);
				}
				else
				{
					_Pitch = MainHelper.GetNumber(int.Parse(PitchMinComboBox.Text), int.Parse(PitchMaxComboBox.Text), "pitch", v.name);
				}
			}
			if (_Pitch < -10) _Pitch = -10;
			if (_Pitch > 10) _Pitch = 10;
			IncomingPitchTextBox.Text = pitchIsValid ? "pitch=\"" + _Pitch + "\"" : "";
			PitchTextBox.Text = pitchIsValid ? PitchTextBox.Text = "" : _Pitch.ToString();

			// Set PitchComment.
			_PitchComment = _Pitch >= 0 ? -1 : 1;

			// Set rate.
			var rateIsValid = int.TryParse(v.rate, out _Rate);
			if (!rateIsValid)
			{
				if (int.Parse(RateMinComboBox.Text) > int.Parse(RateMaxComboBox.Text))
				{
					_Rate = MainHelper.GetNumber(int.Parse(RateMaxComboBox.Text), int.Parse(RateMinComboBox.Text), "rate", v.name);
				}
				else
				{
					_Rate = MainHelper.GetNumber(int.Parse(RateMinComboBox.Text), int.Parse(RateMaxComboBox.Text), "rate", v.name);
				}
			}
			if (_Rate < -10) _Rate = -10;
			if (_Rate > 10) _Rate = 10;
			IncomingRateTextBox.Text = rateIsValid ? "rate=\"" + _Rate + "\"" : "";
			RateTextBox.Text = rateIsValid ? RateTextBox.Text = "" : _Rate.ToString();

			// Set effect.
			var effectValue = string.IsNullOrEmpty(v.effect) ? "Default" : v.effect;
			SelectEffectsPreset(effectValue);
			IncomingEffectTextBox.Text = !string.IsNullOrEmpty(v.effect) ? "effect=\"" + v.effect + "\"" : "";

			// Set volume.
			var volumeIsValid = int.TryParse(v.volume, out _Volume);
			if (_Volume < 0) _Volume = 0;
			if (_Volume > 100) _Volume = 100;
			if (!volumeIsValid) _Volume = VolumeTrackBar.Value;
			IncomingVolumeTextBox.Text = volumeIsValid ? "volume=\"" + _Volume + "\"" : "";

			// Set group.
			var groupValue = string.IsNullOrEmpty(v.group) ? "" : v.group;
			IncomingGroupTextBox.Text = !string.IsNullOrEmpty(v.group) ? "group=\"" + v.group + "\"" : "";

			// Set command.
			IncomingCommandTextBox.Text = (string.IsNullOrEmpty(v.command)) ? "command value was not submitted!" : "command=\"" + v.command + "\"";
			if (string.IsNullOrEmpty(v.command)) return;

			// commands.
			switch (v.command.ToLower())
			{
				case "copy":
					break;
				case "player":
					if (v.name != null)
					{
						var playerNames = v.name.Split(',').Select(x=>x.Trim()).ToArray();
						_PlayerName = playerNames.FirstOrDefault();
						_PlayerNameChanged = playerNames.Skip(1).FirstOrDefault();
						_PlayerClass = playerNames.Skip(2).FirstOrDefault();
					}
					break;
				case "add":
					if (v.parts != null)
						buffer += string.Join("", v.parts);
					break;
				case "sound":
					// Get WAV (name), selected as default.
					string introSound = (string)DefaultIntroSoundComboBox.SelectedItem.ToString().ToLower();
					// If group is not submitted.
					if (string.IsNullOrEmpty(v.group))
					{
						var stream = GetIntroSound(introSound);
						if (stream != null)
						{
							AddIntroSoundToPlayList(introSound, v.group, stream);
						}
					}
					// If group is submitted.
					else
					{
						// Check if group is listed in [Intro Sounds] tab.
						var Sound = SettingsFile.Current.Sounds.FirstOrDefault(x => x.group.ToLower() == v.group.ToLower());
						// If group is not listed.
						if (Sound == null)
						{
							// add group to list.
							SoundsPanel.SoundsAddNewRecord(false, v.group);
							SoundsPanel.SelectRow(v.group);
							// play default (embedded) WAV.
							var stream = GetIntroSound(introSound);
							if (stream != null)
							{
								AddIntroSoundToPlayList(introSound, v.group, stream);
							}
						}
						// If group is listed.
						else
						{
							SoundsPanel.SelectRow(Sound.group);
							if (Sound.enabled == false)
								break;
							// Get WAV name/path.
							string wavToPlay = string.IsNullOrEmpty(Sound.file) ? introSound : Sound.file;
							wavToPlay = MainHelper.ConvertFromSpecialFoldersPattern(wavToPlay);
							//DefaultIntroSoundComboBox_SelectedIndexChanged(null, null, wavToPlay);
							// byte[] wavData;
							var stream = GetIntroSound(wavToPlay);
							if (stream != null)
							{
								AddIntroSoundToPlayList(wavToPlay, v.group, stream);
							}
							else if (System.IO.File.Exists(wavToPlay))
							{
								stream = new System.IO.FileStream(wavToPlay, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
								AddIntroSoundToPlayList(wavToPlay, v.group, stream);
							}
						}
					}
					break;
				case "play":
					if (v.parts != null)
						buffer += string.Join("", v.parts);
					var decodedText = System.Web.HttpUtility.HtmlDecode(buffer);
					buffer = "";
					IncomingTextTextBox.Text = decodedText;
					// Add silence before message.
					int silenceIntBefore = Decimal.ToInt32(OptionsPanel.silenceBefore);
					if (silenceIntBefore > 0)
					{
						AddTextToPlaylist(ProgramComboBox.Text, "<silence msec=\"" + silenceIntBefore.ToString() + "\" />", true, v.group);
					}
					// Add actual message to the playlist
					AddTextToPlaylist(ProgramComboBox.Text, decodedText, true, v.group,
						// Supply NCP properties.
						v.name, v.gender, v.effect,
						// Supply Player properties.
						_PlayerName, _PlayerNameChanged, _PlayerClass
					);
					// Add silence after message.
					int silenceIntAfter = Decimal.ToInt32(OptionsPanel.silenceAfter);
					if (silenceIntAfter > 0)
					{
						AddTextToPlaylist(ProgramComboBox.Text, "<silence msec=\"" + silenceIntAfter.ToString() + "\" />", true, v.group);
					}
					break;
				case "stop":
					text = "";
					StopPlayer(v.group);
					IncomingRateTextBox.Text = "";
					IncomingPitchTextBox.Text = "";
					break;
				case "save":
					if (!string.IsNullOrEmpty(v.name))
						VoiceDefaultsPanel.UpsertRecord(v);
					break;
				default:
					break;
			}
		}

	}
}
