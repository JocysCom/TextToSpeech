using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls.Options
{
	public partial class GeneralOptionsUserControl : UserControl
	{
		public GeneralOptionsUserControl()
		{
			InitializeComponent();
		}

		private void GeneralOptionsUserControl_Load(object sender, EventArgs e)
		{
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.AddDataBinding(AddSilenceBeforeNumericUpDown, SettingsManager.Options, x => x.AddSilenceBeforeMessage);
			ControlsHelper.AddDataBinding(AddSilenceAfterNumericUpDown, SettingsManager.Options, x => x.AddSilenceAfterMessage);
			ControlsHelper.AddDataBinding(SplitMessageIntoSentencesCheckBox, SettingsManager.Options, x => x.SplitMessageIntoSentences);
			SettingsManager.Options.PropertyChanged += Options_PropertyChanged;
			EnumeratePlaybackDevices();
			SilenceBefore();
			SilenceAfter();
		}

		private void Options_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(SettingsManager.Options.AddSilenceBeforeMessage))
				SilenceBefore();
			if (e.PropertyName == nameof(SettingsManager.Options.AddSilenceAfterMessage))
				SilenceAfter();
		}

		private void SilenceBefore()
		{
			// Show or hide silence after message tag.
			var value = SettingsManager.Options.AddSilenceBeforeMessage;
			SilenceBeforeTagLabel.Text = value > 0
				? string.Format("<silence msec=\"{0}\" />", value)
				: "";
		}

		private void SilenceAfter()
		{
			// Show or hide silence after message tag.
			var value = SettingsManager.Options.AddSilenceAfterMessage;
			SilenceAfterTagLabel.Text = value > 0
				? string.Format("<silence msec=\"{0}\" />", value)
				: "";
		}

		#region Playback Devices

		private void RefreshPlaybackDevices_Click(object sender, EventArgs e)
		{
			EnumeratePlaybackDevices();
		}

		void EnumeratePlaybackDevices()
		{
			// Setup our sound listener
			PlaybackDeviceComboBox.Items.Clear();
			var names = AudioPlayer.GetDeviceNames();
			foreach (var name in names)
				PlaybackDeviceComboBox.Items.Add(name);
			// Restore audio settings.
			if (PlaybackDeviceComboBox.Items.Contains(SettingsManager.Options.PlaybackDevice))
				PlaybackDeviceComboBox.SelectedItem = SettingsManager.Options.PlaybackDevice;
			else
				PlaybackDeviceComboBox.SelectedIndex = 0;
		}

		#endregion

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				SettingsManager.Options.PropertyChanged -= Options_PropertyChanged;
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}


	}
}
