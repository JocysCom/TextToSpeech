using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Google
{
	public partial class GoogleOptionsUserControl : UserControl
	{
		public GoogleOptionsUserControl()
		{
			InitializeComponent();
			WebAppClientIdTextBox.Text = SettingsManager.Options.GoogleWebAppClientId;
			WebAppClientSecretTextBox.Text = SettingsManager.Options.GoogleWebAppClientSecret;
			WebAppClientIdTextBox.TextChanged += WebAppClientIdTextBox_TextChanged;
			WebAppClientSecretTextBox.TextChanged += WebAppClientSecretTextBox_TextChanged;
		}

		private void GoogleOptionsUserControl_Load(object sender, EventArgs e)
		{

		}

		private void WebAppClientIdTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.GoogleWebAppClientId = WebAppClientIdTextBox.Text;
		}

		private void WebAppClientSecretTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.GoogleWebAppClientSecret = WebAppClientSecretTextBox.Text;
		}

		private void TestButton_Click(object sender, EventArgs e)
		{
			var client = new Google.TextToSpeechClient();
			//client.ReceiveToken()
		}
	}
}
