using JocysCom.ClassLibrary.Controls;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class MonitorServerUserControl : UserControl
	{
		public MonitorServerUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
		}

		private void MonitorUdpPortTestButton_Click(object sender, EventArgs e)
		{
			// Enable UDP monitor if not enabled yet.
			if (!SettingsManager.Options.UdpMonitorEnabled)
				SettingsManager.Options.UdpMonitorEnabled = true;
			// Create UDP client.
			var clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			var address = IPAddress.Parse("127.0.0.1");
			var remoteEP = new IPEndPoint(address, (int)UdpPortNumberNumericUpDown.Value);
			clientSock.Connect(remoteEP);
			// Get message to send.
			var text = SapiVoiceRadioButton.Checked
				? SapiMessageTextBox.Text : MonitorMessageTextBox.Text;
			var bytes = System.Text.Encoding.UTF8.GetBytes(text);
			// Send the message and dispose client.
			clientSock.Send(bytes);
			clientSock.Close();
		}

		private void SapiVoiceRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			SapiMessageTextBox.ReadOnly = !SapiVoiceRadioButton.Checked;
			MonitorMessageTextBox.ReadOnly = SapiVoiceRadioButton.Checked;
		}

		private void UdpPortNumberNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			UpdateExample();
		}

		void UpdateExample()
		{
			var cs =
			"// C# example how to send message to Text to Speech Monitor from another program.\r\n" +
			"var clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);\r\n" +
			"var address = IPAddress.Parse(\"127.0.0.1\");\r\n" +
			"var remoteEP = new IPEndPoint(address, {0});\r\n" +
			"clientSock.Connect(remoteEP);\r\n" +
			"var message = \"<message command=\\\"Play\\\"><part>Test message.</part></message>\";\r\n" +
			"var bytes = System.Text.Encoding.UTF8.GetBytes(message);\r\n" +
			"clientSock.Send(bytes);\r\n" +
			"clientSock.Close();";
			CodeExampleTextBox.Text = string.Format(cs, (int)UdpPortNumberNumericUpDown.Value);

		}

		private void MonitorServerUserControl_Load(object sender, EventArgs e)
		{
			// To avoid validation problems, make sure to add DataBindings inside "Load" event and not inside Constructor.
			ControlsHelper.AddDataBinding(UdpEnabledCheckBox, s => s.Checked, SettingsManager.Options, d => d.UdpMonitorEnabled);
			ControlsHelper.AddDataBinding(UdpPortNumberNumericUpDown, s => s.Value, Program._UdpMonitor, d => d.PortNumber);
			ControlsHelper.AddDataBinding(UdpPortMessagesTextBox, s => s.Text, Program._UdpMonitor, d => d.MessagesReceived);
			UpdateExample();
		}
	}
}
