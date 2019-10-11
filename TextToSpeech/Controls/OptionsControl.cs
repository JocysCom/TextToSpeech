using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{

		public OptionsControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			var isElevated = JocysCom.ClassLibrary.Security.PermissionHelper.IsElevated;
			// Hide clipboard for later.
			OptionsTabControl.TabPages.Remove(MonitorClipBoardTabPage);
			// Make Google Cloud invisible, because it is not finished yet.
			OptionsTabControl.TabPages.Remove(GoogleTTSTabPage);
			AddSilcenceBeforeNumericUpDown.Value = SettingsManager.Options.AddSilenceBeforeMessage;
			AddSilenceAfterNumericUpDown.Value = SettingsManager.Options.AddSilenceAfterMessage;
			EnumeratePlaybackDevices();
			UpdatePlayBackDevice();
			SilenceBefore();
			SilenceAfter();
			ControlsHelper.ApplyImageStyle(OptionsTabControl);
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

		#endregion

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
