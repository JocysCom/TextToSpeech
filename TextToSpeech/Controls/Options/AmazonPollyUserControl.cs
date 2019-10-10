using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
			var regions = RegionEndpoint.EnumerableAllRegions
				.OrderBy(x => x.ToString())
				.ToArray();
			RegionComboBox.DataSource = regions;
			var region = regions.FirstOrDefault(x => x.SystemName == SettingsManager.Options.AmazonRegionSystemName);
			if (region == null)
				region = regions.FirstOrDefault(x => x.ToString().Contains("EU West") && x.ToString().Contains("London"));
			if (region == null)
				region = regions.FirstOrDefault(x => x.ToString().Contains("EU West"));
			if (region == null)
				region = regions.FirstOrDefault();
			if (region != null)
				RegionComboBox.SelectedItem = region;
			RegionComboBox.SelectedIndexChanged += RegionComboBox_SelectedIndexChanged;
			AmazonEnabledCheckBox.DataBindings.Add(nameof(AmazonEnabledCheckBox.Checked), SettingsManager.Options, nameof(SettingsManager.Options.AmazonEnabled));
		}

		private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var region = (RegionEndpoint)RegionComboBox.SelectedItem;
			if (region != null)
				SettingsManager.Options.AmazonRegionSystemName = region.SystemName;
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

		private void RefreshVoicesButton_Click(object sender, EventArgs e)
		{
			var voices = GetAmazonVoices();
			VoicesComboBox.DataSource = voices;
		}

		List<InstalledVoiceEx> GetAmazonVoices()
		{
			var client = new Voices.AmazonPolly(
				SettingsManager.Options.AmazonAccessKey,
				SettingsManager.Options.AmazonSecretKey,
				SettingsManager.Options.AmazonRegionSystemName
			);
			var voices = client.DescribeVoices();
			var installedVoices = voices.Select(x => new InstalledVoiceEx(x));
			return installedVoices.OrderBy(x=>x.Name).ToList();
		}

		private void SpeakButton_Click(object sender, EventArgs e)
		{
			var text = MessageTextBox.Text;
			var client = new Voices.AmazonPolly(
				SettingsManager.Options.AmazonAccessKey,
				SettingsManager.Options.AmazonSecretKey,
				SettingsManager.Options.AmazonRegionSystemName
			);
			var voice = (Voice)((InstalledVoiceEx)VoicesComboBox.SelectedItem).Voice;
			var buffer = client.SynthesizeSpeech(voice.Id, text);
			var item = ConvertToPlatItem(buffer);
			Global.playlist.Add(item);
		}

		PlayItem ConvertToPlatItem(byte[] mp3)
		{
			var item = new PlayItem();
			using (Stream stream = new MemoryStream(mp3))
			{
				// Load existing XML and WAV data into PlayItem.
				var ms = new MemoryStream();
				var ad = new SharpDX.MediaFoundation.AudioDecoder(stream);
				var samples = ad.GetSamples();
				var enumerator = samples.GetEnumerator();
				while (enumerator.MoveNext())
				{
					var sample = enumerator.Current.ToArray();
					ms.Write(sample, 0, sample.Length);
				}
				// Read WAV head.
				item.WavHead = ad.WaveFormat;
				// Read WAV data.
				item.WavData = ms.ToArray();
				item.Duration = (int)ad.Duration.TotalMilliseconds;
			}
			item.Status = JobStatusType.Synthesized;
			return item;
		}


	}
}
