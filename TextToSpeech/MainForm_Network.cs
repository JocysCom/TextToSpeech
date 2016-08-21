using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Network;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm
	{

		// The socket which monitors all incoming packets.
		private List<Socket> monitoringSockets = new List<Socket>();
		// A flag to check if packets are to be monitored or not.
		private bool continueMonitoring = false;
		List<IPAddress> IpAddresses = new List<IPAddress>();
		object monitorLock = new object();

		private void StartNetworkMonitor()
		{
			lock (monitorLock)
			{
				// If monitor is running already then...
				if (monitoringSockets.Count > 0)
				{
					MonitoringStateStatusLabel.Text = "Error: Monitoring already. Stop first!";
					return;
				}
				IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
				IpAddresses.Clear();
				if (HosyEntry.AddressList.Length > 0)
				{
					foreach (IPAddress ip in HosyEntry.AddressList)
					{
						if (ip.AddressFamily == AddressFamily.InterNetwork)
						{
							// If IP address is not in the list then...
							if (!IpAddresses.Contains(ip))
							{
								// Add IP Address.
								IpAddresses.Add(ip);
							}
						}
					}
				}
				if (IpAddresses.Count == 0)
				{
					MonitoringStateStatusLabel.Text = "Error: No Interface to monitor the packets!";
					return;
				}
				else
				{
					MonitoringStateStatusLabel.Text = string.Format("Monitoring: {0}", string.Join(", ", IpAddresses));
				}
				try
				{
					continueMonitoring = true;
					foreach (var ip in IpAddresses)
					{
						// For sniffing the socket to monitor the packets has to be a raw socket, with the
						// address family being of type internetwork, and protocol being IP
						var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
						//Bind the socket to the selected IP address.
						// Note: it looks like monitorPort value is ignored and all ports will be monitored.
						socket.Bind(new IPEndPoint(ip, MonitorItem.FilterDestinationPort));
						//Set the socket  options: Applies only to TCP packets, Set the include the header, option to true.
						socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
						// Input data required by the operation. 
						byte[] optionInValue = new byte[4] { 1, 0, 0, 0 };
						// Output data returned by the operation. 
						byte[] optionOutValue = new byte[4];
						// Socket.IOControl is analogous to the WSAIoctl method of Winsock 2: Equivalent to SIO_RCVALL constant, of Winsock 2
						socket.IOControl(IOControlCode.ReceiveAll, optionInValue, optionOutValue);
						var bufferSize = 0xFFFF;
						// The default socket buffer size in Windows sockets is 8192 bytes.
						// Increase the receive buffer to 65535 bytes or some UDP data packets will be lost.
						socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, bufferSize);
						// Create data buffer where socket will write captured data.
						var state = new SocketState(socket, bufferSize);
						// Start receiving the packets asynchronously.
						socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), state);
						monitoringSockets.Add(socket);
					}
				}
				catch (Exception ex)
				{
					LastException = ex;
				}
			}
		}

		void StopNetworkMonitor()
		{
			lock (monitorLock)
			{
				// If monitor is not running then...
				if (monitoringSockets.Count == 0)
				{
					MonitoringStateStatusLabel.Text = "Error: Not monitoring. Start monitor first!";
					return;
				}
				foreach (var socket in monitoringSockets)
				{
					socket.Close();
				}
				monitoringSockets.Clear();
			}
		}

		object PacketsStateStatusLabelLock = new object();
		long PacketsCount = 0;

		private void BeginReceive_Callback(IAsyncResult ar)
		{
			try
			{
				if (Disposing || IsDisposed || !IsHandleCreated)
				{
					return;
				}
				BeginInvoke((Action)(() =>
				{
					lock (PacketsStateStatusLabelLock)
					{
						PacketsCount++;
						PacketsStateStatusLabel.Text = string.Format("Packets: {0}", PacketsCount);
					}
				}));
				var state = (SocketState)ar.AsyncState;
				SocketError errorCode;
				int bytesReceived = state.Socket.EndReceive(ar, out errorCode);
				if (errorCode == SocketError.Success)
				{
					// Analyze the bytes received...
					ParseData(state.Buffer, bytesReceived);
				}
				if (continueMonitoring)
				{
					//Another call to BeginReceive so that we continue to receive the incoming packets.
					state.Socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, new AsyncCallback(BeginReceive_Callback), state);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				LastException = ex;
			}
		}

		BindingList<PlugIns.VoiceListItem> WowMessageList = new BindingList<PlugIns.VoiceListItem>();

		object SequenceNumbersLock = new object();
		List<uint> SequenceNumbers = new List<uint>();




		private void ParseData(byte[] byteData, int nReceived)
		{
			// All protocol packets are encapsulated in the IP datagram.
			// Parse IP header and see what protocol data is being carried by it.
			IpHeader ipHeader = new IpHeader(byteData, 0, nReceived);
			// ------------------------------------------------------------
			var sourceIsLocal = IpAddresses.Contains(ipHeader.SourceAddress);
			var destinationIsLocal = IpAddresses.Contains(ipHeader.DestinationAddress);
			var direction = TrafficDirection.Local;
			if (sourceIsLocal && !destinationIsLocal)
			{
				direction = TrafficDirection.Out;
			}
			else if (!sourceIsLocal && destinationIsLocal)
			{
				direction = TrafficDirection.In;
			}
			ITcpUdpHeader header = null;
			uint? sequenceNumber = null;
			if (ipHeader.Protocol == ProtocolType.Tcp)
			{
				var tcpHeader = new TcpHeader(ipHeader.Data, 0, ipHeader.Data.Length);
				sequenceNumber = tcpHeader.SequenceNumber;
				header = tcpHeader;
			}
			else if (ipHeader.Protocol == ProtocolType.Udp)
			{
				header = new UdpHeader(ipHeader.Data, 0, ipHeader.Data.Length);
			}
			// If IP datagram do not contain TCP or UDP data then return.
			if (header == null)
			{
				return;
			}
			// IPHeader.Data stores the data being carried by the IP datagram.
			if (Properties.Settings.Default.LogEnable)
			{
				var index = 0;
				if (OptionsPanel.SearchPattern != null && OptionsPanel.SearchPattern.Length > 0)
				{
					index = JocysCom.ClassLibrary.Text.Helper.IndexOf(byteData, OptionsPanel.SearchPattern, 0);
				}
				if (index > -1)
				{
                    // Play "Radio2" sound if "LogEnabled" and "LogSound" check-boxes are checked.
                    if (Properties.Settings.Default.LogEnable && Properties.Settings.Default.LogSound)
                    {
                            var stream = GetIntroSound("Radio2");
                            if (stream != null)
                            {
                                var player = new System.Media.SoundPlayer();
                                player.Stream = stream;
                                player.Play();
                            }
                    }
                    // ---------------------------------------------
                    var writer = OptionsPanel.Writer;
					if (writer != null)
					{
						writer.WriteLine("{0:HH:mm:ss.fff}: {1} {2}: {3}:{4} -> {5}:{6} Data[{7}]",
							DateTime.Now,
							ipHeader.Protocol.ToString().ToUpper(),
							destinationIsLocal ? "In" : "Out",
							ipHeader.SourceAddress,
							header.SourcePort,
							ipHeader.DestinationAddress,
							header.DestinationPort,
							header.Data.Length
						);
						var block = JocysCom.ClassLibrary.Text.Helper.BytesToStringBlock(
							header.Data, false, true, true);
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
			if (MonitorItem.FilterDestinationPort > 0 && header.DestinationPort != MonitorItem.FilterDestinationPort)
			{
				return;
			}
			// If protocol is specified but wrong type then return.
			if (MonitorItem.FilterProtocol.HasValue && ipHeader.Protocol != MonitorItem.FilterProtocol.Value)
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
			voiceItem.Load(ipHeader, header);
			// If data do not contain XML message then return.
			if (!voiceItem.IsVoiceItem)
			{
				return;
			}
			var allowToAdd = true;
			// If message contains sequence number...
			if (sequenceNumber.HasValue)
			{
				lock (SequenceNumbersLock)
				{
					// Cleanup sequence list by removing oldest numbers..
					while (SequenceNumbers.Count > 10) SequenceNumbers.RemoveAt(0);
					// If number is not unique then...
					if (SequenceNumbers.Contains(sequenceNumber.Value))
					{
						// Don't allow to add the message.
						allowToAdd = false;
					}
					else
					{
						// Store sequence number for the future checks.
						SequenceNumbers.Add(sequenceNumber.Value);
					}
				}
			}
			if (allowToAdd)
			{
				// Add wow item to the list. Use Invoke to make it Thread safe.
				this.Invoke((Action<PlugIns.VoiceListItem>)addVoiceListItem, new object[] { voiceItem });
			}
		}

		bool ScrollMessagesGrid = false;
		object ScrollMessagesGridLock = new object();

		private void addVoiceListItem(PlugIns.VoiceListItem wowItem)
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
				foreach (var socket in monitoringSockets)
				{
					socket.Close();
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

		void ProcessVoiceTextMessage(string text)
		{
			// If <message.
			if (!text.Contains("<message")) return;
			var v = Serializer.DeserializeFromXmlString<message>(text);
			// Override voice values.
			var name = v.name;
			var overrideVoice = SettingsFile.Current.Overrides.FirstOrDefault(x => x.name == name);
			// if override was not found then...
			if (overrideVoice != null) v.OverrideFrom(overrideVoice);

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
			_PitchComment = _Pitch >= 0 ? -4 : 4;

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
				case "add":
					if (v.parts != null) buffer += string.Join("", v.parts);
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
							if (Sound.enabled == false) break;
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
					if (v.parts != null) buffer += string.Join("", v.parts);
					var decodedText = System.Web.HttpUtility.HtmlDecode(buffer);
					buffer = "";
					IncomingTextTextBox.Text = decodedText;
					//TextXmlTabControl.SelectedTab = TextTabPage;
					// mark text (or audio file) with v.goup value.

					// Add silence before message.
					int silenceIntBefore = Decimal.ToInt32(OptionsPanel.silenceBefore);
					string silenceStringBefore = OptionsPanel.silenceBefore.ToString();
					if (silenceIntBefore > 0) AddTextToPlaylist("<silence msec=\"" + silenceStringBefore + "\" />", true, v.group);

					AddTextToPlaylist(decodedText, true, v.group);

					// Add silence after message.
					int silenceIntAfter = Decimal.ToInt32(OptionsPanel.silenceAfter);
					string silenceStringAfter = OptionsPanel.silenceAfter.ToString();
					if (silenceIntAfter > 0) AddTextToPlaylist("<silence msec=\"" + silenceStringAfter + "\" />", true, v.group);


					break;
				case "stop":
					text = "";
					StopPlayer(v.group);
					IncomingRateTextBox.Text = "";
					IncomingPitchTextBox.Text = "";
					break;
				case "save":
					if (!string.IsNullOrEmpty(v.name)) VoiceOverridesPanel.UpsertRecord(v);
					break;
				default:
					break;
			}
		}

	}
}
