using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Google
{
	public partial class GoogleTTSUserControl : UserControl
	{
		public GoogleTTSUserControl()
		{
			InitializeComponent();
			if (IsDesignMode)
				return;
			WebAppClientIdTextBox.Text = SettingsManager.Options.GoogleWebAppClientId;
			WebAppClientSecretTextBox.Text = SettingsManager.Options.GoogleWebAppClientSecret;
			WebAppClientIdTextBox.TextChanged += WebAppClientIdTextBox_TextChanged;
			WebAppClientSecretTextBox.TextChanged += WebAppClientSecretTextBox_TextChanged;
			ApiKeyTextBox.TextChanged += ApiKeyTextBox_TextChanged;
		}

		public bool IsDesignMode
		{
			get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
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

		private void ApiKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.GoogleTtsApiKey = ApiKeyTextBox.Text;
		}

		private void TestButton_Click(object sender, EventArgs e)
		{
			var client = new Google.TextToSpeechClient();
			client.ApiKey = SettingsManager.Options.GoogleTtsApiKey;
			var list = client.List();
			//var key = client.ReceiveToken(
			//	"",
			//	SettingsManager.Options.GoogleWebAppClientId,
			//	SettingsManager.Options.GoogleWebAppClientSecret
			//);
		}

		private void GetKeyButton_Click(object sender, EventArgs e)
		{
			var client = new Google.TextToSpeechClient();
			var list = client.ReceiveToken2("",
				SettingsManager.Options.GoogleWebAppClientId,
				SettingsManager.Options.GoogleWebAppClientSecret,
				""
			);
		}

		private void GetCredentialsButton_Click(object sender, EventArgs e)
		{
			var client = new Google.TextToSpeechClient();
			var list = client.ReceiveClientCredentials(
				SettingsManager.Options.GoogleWebAppClientId,
				SettingsManager.Options.GoogleWebAppClientSecret
			);
		}
	}
}
