using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JocysCom.TextToSpeech.Monitor.Audio;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{

		public OptionsControl()
		{
			InitializeComponent();
			if (IsDesignMode)
				return;
			// Make Google Cloud invisible, because it is not finished yet.
			OptionsTabControl.TabPages.Remove(GoogleCloudTabPage);
			AddSilcenceBeforeNumericUpDown.Value = SettingsManager.Options.AddSilcenceBeforeMessage;
			AddSilenceAfterNumericUpDown.Value = SettingsManager.Options.DelayBeforeValue;
			LoggingFolderTextBox.Text = GetLogsPath(true);
			LoadSettings();
			SilenceBefore();
			SilenceAfter();
		}

		public bool IsDesignMode
		{
			get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
		}

		void LoadSettings()
		{
			// Load settings into form.
			LoggingTextBox.Text = SettingsManager.Options.LogText;
			SearchPattern = Encoding.ASCII.GetBytes(LoggingTextBox.Text);
			LoggingCheckBox.Checked = SettingsManager.Options.LogEnable;
			// Update writer settings.
			SaveSettings();
			// Attach events.
			LoggingTextBox.TextChanged += LoggingTextBox_TextChanged;
			LoggingCheckBox.CheckedChanged += LoggingCheckBox_CheckedChanged;
			LoggingPlaySoundCheckBox.CheckedChanged += LoggingPlaySoundCheckBox_CheckedChanged;
			EnumeratePlaybackDevices();
			UpdatePlayBackDevice();
		}

		void SaveSettings()
		{
			SettingsManager.Options.LogEnable = LoggingCheckBox.Checked;
			LoggingTextBox.Enabled = !LoggingCheckBox.Checked;
			LoggingPlaySoundCheckBox.Enabled = !LoggingCheckBox.Checked;
			LoggingFolderTextBox.Enabled = !LoggingCheckBox.Checked;
			OpenButton.Enabled = !LoggingCheckBox.Checked;
			FilterTextLabel.Enabled = !LoggingCheckBox.Checked;
			LogFolderLabel.Enabled = !LoggingCheckBox.Checked;
			lock (WriterLock)
			{
				var en = SettingsManager.Options.LogEnable;
				if (Writer == null && en && !IsDisposed && !Disposing)
				{
					Writer = new ClassLibrary.IO.LogFileWriter();
					Writer.LogFileName = GetLogsPath(true) + "\\log_";
					Writer.LogFileAutoFlush = true;
				}
				else if (Writer != null && !en)
				{
					Writer.Dispose();
					Writer = null;
				}
			}
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

		public string GetLogsPath(bool create)
		{
			var path = Path.Combine(MainHelper.AppDataPath, "Logs");
			if (create && !Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}

		private void OpenButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(LoggingFolderTextBox.Text);
		}

		public byte[] SearchPattern;
		public JocysCom.ClassLibrary.IO.LogFileWriter Writer;
		public object WriterLock = new object();

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
			var value = LoggingPlaySoundCheckBox.Checked;
			SettingsManager.Options.LogSound = value;
		}

		private void LoggingTextBox_TextChanged(object sender, EventArgs e)
		{
			var text = LoggingTextBox.Text;
			SettingsManager.Options.LogText = text;
			SearchPattern = string.IsNullOrEmpty(text)
				? null
				: Encoding.ASCII.GetBytes(LoggingTextBox.Text);
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
			lock (WriterLock)
			{
				if (Writer != null)
				{
					Writer.Dispose();
					Writer = null;
				}
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
