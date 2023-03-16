using JocysCom.ClassLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls.Options
{
	public partial class ElevenLabsVoicesUserControl : UserControl
	{
		public ElevenLabsVoicesUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			SecretKeyTextBox.Text = SettingsManager.Options.ElevenLabsApiKey;
			SecretKeyTextBox.TextChanged += SecretKeyTextBox_TextChanged;
			// If key is valid then...
			var isValid =
				!string.IsNullOrEmpty(SettingsManager.Options.ElevenLabsApiKey);
			if (isValid)
				RefreshVoicesButton_Click(null, null);
		}

		private void VoicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SpeakButton.Enabled = VoicesComboBox.SelectedIndex > -1;
		}

		private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshVoicesButton_Click(null, null);
		}

		private void SecretKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.Options.ElevenLabsApiKey = SecretKeyTextBox.Text;
		}

		private void AwsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var link = ((LinkLabel)sender).Text;
			MainHelper.OpenUrl(link);
		}

		private void RefreshVoicesButton_Click(object sender, EventArgs e)
		{
			var selectedName = (VoicesComboBox.SelectedItem as InstalledVoiceEx)?.ToString();
			var voices = GetVoices();
			VoicesComboBox.DataSource = voices;
			var selected = voices.FirstOrDefault(x => x.ToString() == selectedName);
			if (selected == null)
				selected = voices.FirstOrDefault(x => x.Name == "Joanna" && x.ToString().Contains("neural") && x.CultureName.Contains("en-US"));
			if (selected == null)
				selected = voices.FirstOrDefault(x => x.Name != "Ivy" && x.ToString().Contains("neural") && x.CultureName.Contains("en-US"));
			if (selected == null)
				selected = voices.FirstOrDefault(x => x.Name != "Ivy" && x.CultureName.Contains("en-US"));
			if (selected != null)
				VoicesComboBox.SelectedItem = selected;
		}

		List<InstalledVoiceEx> GetVoices()
		{
			StatusTextBox.Text = "";
			var client = new Voices.ElevenLabsClient(
				SettingsManager.Options.ElevenLabsApiKey
			);
			var voices = client.GetVoices();
			var result = client.LastException == null ? "Success" : client.LastException.Message;
			StatusTextBox.Text = string.Format("{0:HH:mm:ss}: {1}", DateTime.Now, result);
			var installedVoices = voices
				.Select(x => client.Convert(x))
				.OrderBy(x => x.Name)
				.ToList();
			return installedVoices;
		}

		private void Global_Exception(object sender, ClassLibrary.EventArgs<Exception> e)
		{
			throw new NotImplementedException();
		}

		private void SpeakButton_Click(object sender, EventArgs e)
		{
			StatusTextBox.Text = "";
			var promptBuilder = new System.Speech.Synthesis.PromptBuilder();
			promptBuilder.AppendText(MessageTextBox.Text);
			var client = new Voices.ElevenLabsClient(
				SettingsManager.Options.ElevenLabsApiKey
			);
			//var voice = (Voice)((InstalledVoiceEx)VoicesComboBox.SelectedItem).Voice;
			//var buffer = client.SynthesizeSpeech(voice.Id, promptBuilder.ToXml(), null);
			//var result = client.LastException == null ? "Success" : client.LastException.Message;
			//StatusTextBox.Text = string.Format("{0:HH:mm:ss}: {1}", DateTime.Now, result);
			//var item = Global.DecodeToPlayItem(buffer);
			//Global.playlist.Add(item);
		}

	}
}
