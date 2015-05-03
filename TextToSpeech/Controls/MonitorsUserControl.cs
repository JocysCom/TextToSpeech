using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
    public partial class MonitorsUserControl : UserControl
    {
        public MonitorsUserControl()
        {
            InitializeComponent();
            UpdateExample();
        }

        private void MonitorUdpPortCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UdpEnabledCheckBox.Checked)
            {
                StartUdpServer();
            }
            else
            {
                StopUdpServer();
            }
        }

        private void MonitorUdpPortTestButton_Click(object sender, EventArgs e)
        {
            if (!UdpEnabledCheckBox.Checked)
            {
                UdpEnabledCheckBox.Checked = true;
            }
            var clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var address = IPAddress.Parse("127.0.0.1");
            var remoteEP = new IPEndPoint(address, (int)UdpPortNumberNumericUpDown.Value);
            clientSock.Connect(remoteEP);
            var text = SapiVoiceRadioButton.Checked
                ? SapiMessageTextBox.Text : MonitorMessageTextBox.Text;
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            clientSock.Send(bytes);
            clientSock.Close();
        }


        #region UDP Monitor

        Socket serverSocket;
        object serverSocketLock = new object();
        int udpMessagesReceived = 0;

        void StartUdpServer()
        {
            lock (serverSocketLock)
            {
                if (IsDisposing) return;
                if (serverSocket != null)
                {
                    // Server is already running;
                    return;
                }
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                serverSocket.ExclusiveAddressUse = false;
                serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                var address = IPAddress.Parse("127.0.0.1");
                var localEP = new IPEndPoint(address, (int)UdpPortNumberNumericUpDown.Value);
                serverSocket.Bind(localEP);
                StartReceive();
            }
        }

        void StopUdpServer()
        {
            lock (serverSocketLock)
            {
                // If server is running then...
                if (serverSocket != null)
                {
                    serverSocket.Close();
                    serverSocket = null;
                }
            }
        }

        void StartReceive()
        {
            byte[] buffer = new Byte[8096];
            var sockArgs = new SocketAsyncEventArgs();
            sockArgs.AcceptSocket = serverSocket;
            // Receive only from 127.0.0.1.
            var address = IPAddress.Parse("127.0.0.1");
            var remoteEP = new IPEndPoint(address, 0);
            sockArgs.RemoteEndPoint = remoteEP;
            sockArgs.SetBuffer(buffer, 0, buffer.Length);
            sockArgs.Completed += sockArgs_Completed;
            var willRaiseEvent = serverSocket.ReceiveFromAsync(sockArgs);
            if (!willRaiseEvent) CompleteReceive(sockArgs);
        }

        void sockArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            CompleteReceive(e);
        }

        void CompleteReceive(SocketAsyncEventArgs args)
        {
            string text = null;
            if (args.SocketError == SocketError.Success)
            {
                text = System.Text.Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);
            }
            BeginInvoke((MethodInvoker)delegate()
            {
                udpMessagesReceived++;
                UdpPortMessagesTextBox.Text = udpMessagesReceived.ToString();
                if (!string.IsNullOrEmpty(text))
                {
                    Program.TopForm.AddMessageToPlay(text);
                }
            });
            lock (serverSocketLock)
            {
                if (IsDisposing) return;
                // If socket stopped then return.
                if (serverSocket == null) return;
                StartReceive();
            }
        }

        #endregion

        bool IsDisposing;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                IsDisposing = true;
                components.Dispose();
                StopUdpServer();
            }
            base.Dispose(disposing);
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

    }
}
