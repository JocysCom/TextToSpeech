using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.ClassLibrary.Controls;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{

		public OptionsControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			// Make Google Cloud invisible, because it is not finished yet.
			OptionsTabControl.TabPages.Remove(GoogleCloudTabPage);
			AddSilcenceBeforeNumericUpDown.Value = SettingsManager.Options.AddSilenceBeforeMessage;
			AddSilenceAfterNumericUpDown.Value = SettingsManager.Options.AddSilenceAfterMessage;
			LoadSettings();
			SilenceBefore();
			SilenceAfter();
			ControlsHelper.AddDataBinding(LogFolderTextBox, s => s.Text, SettingsManager.Options, d => d.NetworkMonitorLogFolder);
			ControlsHelper.AddDataBinding(LogEnabledCheckBox, s => s.Checked, SettingsManager.Options, d => d.LogEnable);
			ControlsHelper.AddDataBinding(LogFilterTextTextBox, s => s.Text, SettingsManager.Options, d => d.LogText);
			if (string.IsNullOrEmpty(SettingsManager.Options.NetworkMonitorLogFolder))
				SettingsManager.Options.NetworkMonitorLogFolder = Capturing.Monitors.NetworkMonitor.GetLogsPath(true);
		}

		void LoadSettings()
		{
			// Load settings into form.
			LogFilterTextTextBox.Text = SettingsManager.Options.LogText;
			LogEnabledCheckBox.Checked = SettingsManager.Options.LogEnable;
			// Update writer settings.
			SaveSettings();
			// Attach events.
			LogFilterTextTextBox.TextChanged += LoggingTextBox_TextChanged;
			LogEnabledCheckBox.CheckedChanged += LoggingCheckBox_CheckedChanged;
			LogPlaySoundCheckBox.CheckedChanged += LoggingPlaySoundCheckBox_CheckedChanged;
			EnumeratePlaybackDevices();
			UpdatePlayBackDevice();
		}

		void SaveSettings()
		{
			SettingsManager.Options.LogEnable = LogEnabledCheckBox.Checked;
			LogFilterTextTextBox.Enabled = !LogEnabledCheckBox.Checked;
			LogPlaySoundCheckBox.Enabled = !LogEnabledCheckBox.Checked;
			LogFolderTextBox.Enabled = !LogEnabledCheckBox.Checked;
			OpenButton.Enabled = !LogEnabledCheckBox.Checked;
			FilterTextLabel.Enabled = !LogEnabledCheckBox.Checked;
			LogFolderLabel.Enabled = !LogEnabledCheckBox.Checked;
		}

		#region Tab: General

		private void SilenceBefore()
		{
			// Show or hide silence before message tag.
			int silenceIntBeforeTag = Decimal.ToInt32(AddSilcenceBeforeNumericUpDown.Value);
			string silenceStringBeforeTag = AddSilcenceBeforeNumericUpDown.Value.ToString();
			if (silenceIntBeforeTag > 0)
			{
				SilenceBeforeTagLabel.Text = "<silence msec=\"" + silenceStringBeforeTag + "\" />";
			}
			else
			{
				SilenceBeforeTagLabel.Text = "";
			}
		}

		private void SilenceAfter()
		{
			// Show or hide silence after message tag.
			int silenceIntAfterTag = Decimal.ToInt32(AddSilenceAfterNumericUpDown.Value);
			string silenceStringAfterTag = AddSilenceAfterNumericUpDown.Value.ToString();
			if (silenceIntAfterTag > 0)
			{
				SilenceAfterTagLabel.Text = "<silence msec=\"" + silenceStringAfterTag + "\" />";
			}
			else
			{
				SilenceAfterTagLabel.Text = "";
			}
		}

		public decimal silenceBefore
		{
			get { return AddSilcenceBeforeNumericUpDown.Value; }
			set { AddSilcenceBeforeNumericUpDown.Value = value; }
		}

		public decimal silenceAfter
		{
			get { return AddSilenceAfterNumericUpDown.Value; }
			set { AddSilenceAfterNumericUpDown.Value = value; }
		}

		private void AddSilcenceBeforeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			SilenceBefore();
		}

		private void AddSilenceAfterNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			SilenceAfter();
		}

		private void OpenButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(LogFolderTextBox.Text);
		}

		private void HowToButton_Click(object sender, EventArgs e)
		{
			var message = "";
			message += "1. Enable logging.\r\n";
			message += "2.Enter and send specified text message(for example: me66age) through game or program chat.\r\n";
			message += "3.Information about found packets with specified text(for example: me66age) will be logged to TXT file.\r\n";
			MessageBox.Show(message, "How To...");
		}

		#endregion

		// Save value
		private void LoggingPlaySoundCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var value = LogPlaySoundCheckBox.Checked;
			SettingsManager.Options.LogSound = value;
		}

		private void LoggingTextBox_TextChanged(object sender, EventArgs e)
		{
			var text = LogFilterTextTextBox.Text;
			SettingsManager.Options.LogText = text;
		}

		private void LoggingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			SaveSettings();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Playback Devices

		private void RefreshPlaybackDevices_Click(object sender, EventArgs e)
		{
			EnumeratePlaybackDevices();
			UpdatePlayBackDevice();
		}

		bool suspendEvents;

		void EnumeratePlaybackDevices()
		{
			suspendEvents = true;
			// Setup our sound listener
			PlaybackDeviceComboBox.Items.Clear();
			var names = AudioPlayer.GetDeviceNames();
			foreach (var name in names)
				PlaybackDeviceComboBox.Items.Add(name);
			// Restore audio settings.
			if (PlaybackDeviceComboBox.Items.Contains(SettingsManager.Options.PlaybackDevice))
			{
				PlaybackDeviceComboBox.SelectedItem = SettingsManager.Options.PlaybackDevice;
			}
			else
			{
				PlaybackDeviceComboBox.SelectedIndex = 0;
			}
			suspendEvents = false;
		}

		private void PlaybackDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (suspendEvents) return;
			SettingsManager.Options.PlaybackDevice = (string)PlaybackDeviceComboBox.SelectedItem;
			UpdatePlayBackDevice();
		}

		void UpdatePlayBackDevice()
		{
		}

		#endregion

	}
}
