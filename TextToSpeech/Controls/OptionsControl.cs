using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JocysCom.ClassLibrary.IO;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{


		public OptionsControl()
		{
			InitializeComponent();
			LoggingFolderTextBox.Text = GetLogsPath(true);
			LoadSettings();
			SilenceBefore();
			SilenceAfter();
		}

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

		public Decimal silenceBefore
		{
			get { return AddSilcenceBeforeNumericUpDown.Value; }
			set { AddSilcenceBeforeNumericUpDown.Value = value; }
		}

		public Decimal silenceAfter
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
			var path = new DirectoryInfo(Application.CommonAppDataPath).Parent.FullName;
			path = Path.Combine(path, "Logs");
			if (create && !Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			return path;
		}

		private void OpenButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(LoggingFolderTextBox.Text);
		}

		public byte[] SearchPattern;
		public JocysCom.ClassLibrary.IO.LogFileWriter Writer;
		public object WriterLock = new object();

		void LoadSettings()
		{
			// Load settings into form.
			LoggingTextBox.Text = Properties.Settings.Default.LogText;
			SearchPattern = Encoding.ASCII.GetBytes(LoggingTextBox.Text);
			LoggingCheckBox.Checked = Properties.Settings.Default.LogEnable;
			CacheDataCheckBox.Checked = Properties.Settings.Default.CacheData;
			UpdateWinCapState();
			var allowWinCap = Properties.Settings.Default.UseWinCap & MainHelper.GetWinPcapVersion() != null;
			CaptureSocButton.Checked = !allowWinCap;
			CaptureWinButton.Checked = allowWinCap;
			// Update writer settings.
			SaveSettings();
			// Attach events.
			LoggingTextBox.TextChanged += LoggingTextBox_TextChanged;
			LoggingCheckBox.CheckedChanged += LoggingCheckBox_CheckedChanged;
			CacheDataCheckBox.CheckedChanged += CacheDataCheckBox_CheckedChanged;
			LoggingPlaySoundCheckBox.CheckedChanged += LoggingPlaySoundCheckBox_CheckedChanged;
			CaptureSocButton.CheckedChanged += CaptureSocButton_CheckedChanged;
			CaptureWinButton.CheckedChanged += CaptureWinButton_CheckedChanged;
			EnumeratePlaybackDevices();
			UpdatePlayBackDevice();
		}

		public void UpdateWinCapState()
		{
			var version = MainHelper.GetWinPcapVersion();
			if (version != null)
			{
				CaptureWinButton.Text = string.Format("WinPcap {0}", version.ToString());
				CaptureWinButton.Enabled = true;
			}
			else
			{
				CaptureWinButton.Text = "WinPcap";
				CaptureWinButton.Enabled = false;
			}
		}

		// Save value
		private void LoggingPlaySoundCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var value = LoggingPlaySoundCheckBox.Checked;
			Properties.Settings.Default.LogSound = value;
		}

		private void LoggingTextBox_TextChanged(object sender, EventArgs e)
		{
			var text = LoggingTextBox.Text;
			Properties.Settings.Default.LogText = text;
			SearchPattern = string.IsNullOrEmpty(text)
				? null
				: Encoding.ASCII.GetBytes(LoggingTextBox.Text);
		}

		void SaveSettings()
		{
			Properties.Settings.Default.LogEnable = LoggingCheckBox.Checked;
			LoggingTextBox.Enabled = !LoggingCheckBox.Checked;
			LoggingPlaySoundCheckBox.Enabled = !LoggingCheckBox.Checked;
			LoggingFolderTextBox.Enabled = !LoggingCheckBox.Checked;
			OpenButton.Enabled = !LoggingCheckBox.Checked;
			FilterTextLabel.Enabled = !LoggingCheckBox.Checked;
			LogFolderLabel.Enabled = !LoggingCheckBox.Checked;
			lock (WriterLock)
			{
				var en = Properties.Settings.Default.LogEnable;
				if (Writer == null && en && !IsDisposed && !Disposing)
				{
					Writer = new ClassLibrary.IO.LogFileWriter();
					Writer.LogFilePrefix = GetLogsPath(true) + "\\log_";
					Writer.LogFileAutoFlush = true;
				}
				else if (Writer != null && !en)
				{
					Writer.Dispose();
					Writer = null;
				}
			}
		}

		private void CacheDataCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.CacheData = CacheDataCheckBox.Checked;
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

		private void CaptureSocButton_CheckedChanged(object sender, EventArgs e)
		{
			if (CaptureSocButton.Checked)
			{
				Properties.Settings.Default.UseWinCap = false;
				Program.TopForm.StopNetworkMonitor();
				Program.TopForm.StartNetworkMonitor();
			}
		}

		private void CaptureWinButton_CheckedChanged(object sender, EventArgs e)
		{
			if (CaptureWinButton.Checked)
			{
				Properties.Settings.Default.UseWinCap = true;
				Program.TopForm.StopNetworkMonitor();
				Program.TopForm.StartNetworkMonitor();
			}
		}

		private void OpenCacheButton_Click(object sender, EventArgs e)
		{
			var dir = MainHelper.GetCreateCacheFolder();
			MainHelper.OpenUrl(dir.FullName);
		}

		string _CacheMessageFormat;

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		static string SizeSuffix(long value, int decimalPlaces = 0)
		{
			if (value < 0)
			{
				throw new ArgumentException("Bytes should not be negative", "value");
			}
			var mag = (int)Math.Max(0, Math.Log(value, 1024));
			var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
			return String.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
		}

		private void OptionsControl_Load(object sender, EventArgs e)
		{
			_CacheMessageFormat = CacheLabel.Text;
			var files = MainHelper.GetCreateCacheFolder().GetFiles();
			var count = files.Count();
			var size = SizeSuffix(files.Sum(x => x.Length), 1);
			CacheLabel.Text = string.Format(_CacheMessageFormat, count, size);
		}

		private void CortanaDetailsButton_Click(object sender, EventArgs e)
		{
			var frm = new CortanaForm();
			frm.StartPosition = FormStartPosition.CenterParent;
			frm.ShowDialog(Program.TopForm);
			frm.Dispose();
		}

		private void HowToButton_Click(object sender, EventArgs e)
		{
			var message = "";
			message += "1. Enable logging.\r\n";
			message += "2.Enter and send specified text message(for example: me66age) through game or program chat.\r\n";
			message += "3.Information about found packets with specified text(for example: me66age) will be logged to TXT file.\r\n";
			MessageBox.Show(message, "How To...");
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
			var player = new AudioPlayerApp.AudioPlayer2();
			var names = player.GetDeviceNames();
			player.Dispose();
			foreach (var name in names)
				PlaybackDeviceComboBox.Items.Add(name);
			// Restore audio settings.
			if (PlaybackDeviceComboBox.Items.Contains(Properties.Settings.Default.PlaybackDevice))
			{
				PlaybackDeviceComboBox.SelectedItem = Properties.Settings.Default.PlaybackDevice;
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
			Properties.Settings.Default.PlaybackDevice = (string)PlaybackDeviceComboBox.SelectedItem;
			UpdatePlayBackDevice();
		}

		void UpdatePlayBackDevice()
		{
		}

		#endregion

	}
}
