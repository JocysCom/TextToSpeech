using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JocysCom.ClassLibrary.Controls;

namespace JocysCom.TextToSpeech.Monitor.Controls.Options
{
	public partial class AmazonPollyUserControl : UserControl
	{
		public AmazonPollyUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			AccessKeyTextBox.Text = SettingsManager.Options.AmazonAccessKey;
			SecretKeyTextBox.Text = SettingsManager.Options.AmazonSecretKey;
			AccessKeyTextBox.TextChanged += AccessKeyTextBox_TextChanged;
			SecretKeyTextBox.TextChanged += SecretKeyTextBox_TextChanged;
		}

		private void SecretKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.AmazonSecretKey = SecretKeyTextBox.Text;
		}

		private void AccessKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.AmazonAccessKey = AccessKeyTextBox.Text;
		}

		private void AwsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MainHelper.OpenUrl(AwsLinkLabel.Text);
		}

	}
}
