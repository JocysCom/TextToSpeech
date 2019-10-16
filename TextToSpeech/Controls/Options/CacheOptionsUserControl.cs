using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class CacheOptionsUserControl : UserControl
	{
		public CacheOptionsUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.BindEnum<CacheFileFormat>(CacheAudioFormatComboBox, "{2}");
			CacheAudioChannelsComboBox.DataSource = (AudioChannel[])Enum.GetValues(typeof(AudioChannel));
			// Audio Sample Rate.
			CacheAudioSampleRateComboBox.DataSource = new int[] { 8000, 11025, 22050, 44100, 48000 };
			// Audio Bits Per Sample.
			CacheAudioBitsPerSampleComboBox.DataSource = new int[] { 8, 16, 24 };
			// 32 kbit/s – generally acceptable only for speech
			// 96 kbit/s – generally used for speech or low-quality streaming
			// 128 or 160 kbit/s – mid-range bitrate quality
			// 192 kbit/s – medium quality bitrate
			// 256 kbit/s – a commonly used high-quality bitrate
			// 320 kbit/s – highest level supported by the MP3 standard
			CacheAudioAverageBitsPerSecondComboBox.DataSource = new int[] { 32000, 64000, 96000, 128000, 192000, 256000, 320000 };
			// Block Alignment = Bytes per Sample x Number of Channels
			// For example, the block alignment value for 16-bit PCM format mono audio is 2 (2 bytes per sample x 1 channel). For 16-bit PCM format stereo audio, the block alignment value is 4.
			CacheAudioBlockAlignComboBox.DataSource = new int[] { 1, 2, 4 };
		}

		private void OpenCacheButton_Click(object sender, EventArgs e)
		{
			var dir = MainHelper.GetCreateCacheFolder();
			MainHelper.OpenUrl(dir.FullName);
		}

		string _CacheMessageFormat;

		private void OptionsCacheUserControl_Load(object sender, EventArgs e)
		{
			if (ControlsHelper.IsDesignMode(this))
				return;
			// To avoid validation problems, make sure to add DataBindings inside "Load" event and not inside Constructor.
			ControlsHelper.AddDataBinding(CacheDataWriteCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataWrite);
			ControlsHelper.AddDataBinding(CacheDataReadCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataRead);
			ControlsHelper.AddDataBinding(CacheDataGeneralizeCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataGeneralize);
			// Add cache audio format.
			ControlsHelper.AddDataBinding(CacheAudioConvertCheckBox, SettingsManager.Options, d => d.CacheAudioConvert);
			ControlsHelper.AddDataBinding(CacheAudioFormatComboBox, SettingsManager.Options, d => d.CacheAudioFormat);
			_CacheMessageFormat = CacheLabel.Text;
			var files = MainHelper.GetCreateCacheFolder().GetFiles("*.*", SearchOption.AllDirectories);
			var count = files.Count();
			var size = SizeSuffix(files.Sum(x => x.Length), 1);
			CacheLabel.Text = string.Format(_CacheMessageFormat, count, size);
		}

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		static string SizeSuffix(long value, int decimalPlaces = 0)
		{
			if (value < 0)
			{
				throw new ArgumentException("Bytes should not be negative", nameof(value));
			}
			var mag = (int)Math.Max(0, Math.Log(value, 1024));
			var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
			return string.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
		}

		void AddCacheBindings()
		{
			// Bind Cache format options.
			CacheAudioChannelsComboBox.DataBindings.Add(nameof(CacheAudioChannelsComboBox.SelectedItem), SettingsManager.Options, nameof(SettingsManager.Options.CacheAudioChannels));

			//ControlsHelper.AddDataBinding(CacheAudioChannelsComboBox, SettingsManager.Options, d => d.CacheAudioChannels);
			ControlsHelper.AddDataBinding(CacheAudioSampleRateComboBox, SettingsManager.Options, d => d.CacheAudioSampleRate);
			ControlsHelper.AddDataBinding(CacheAudioBitsPerSampleComboBox, SettingsManager.Options, d => d.CacheAudioBitsPerSample);
			ControlsHelper.AddDataBinding(CacheAudioAverageBitsPerSecondComboBox, SettingsManager.Options, d => d.CacheAudioAverageBitsPerSecond);
			ControlsHelper.AddDataBinding(CacheAudioBlockAlignComboBox, SettingsManager.Options, d => d.CacheAudioBlockAlign);
		}

		void ClearCacheBindings()
		{
			CacheAudioChannelsComboBox.DataBindings.Clear();
			CacheAudioSampleRateComboBox.DataBindings.Clear();
			CacheAudioBitsPerSampleComboBox.DataBindings.Clear();
			CacheAudioAverageBitsPerSecondComboBox.DataBindings.Clear();
			CacheAudioBlockAlignComboBox.DataBindings.Clear();
		}

		private void CacheAudioFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ClearCacheBindings();
			switch (SettingsManager.Options.CacheAudioFormat)
			{
				case CacheFileFormat.WAV:
					CacheAudioChannelsComboBox.Enabled = true;
					CacheAudioSampleRateComboBox.Enabled = true;
					CacheAudioBitsPerSampleComboBox.Enabled = true;
					CacheAudioAverageBitsPerSecondComboBox.Enabled = false;
					CacheAudioBlockAlignComboBox.Enabled = false;
					SettingsManager.Options.CacheAudioChannels = SettingsManager.Options.AudioChannels;
					SettingsManager.Options.CacheAudioSampleRate = SettingsManager.Options.AudioSampleRate;
					SettingsManager.Options.CacheAudioBitsPerSample = SettingsManager.Options.AudioBitsPerSample;
					SettingsManager.Options.CacheAudioAverageBitsPerSecond = 96000;
					SettingsManager.Options.CacheAudioBlockAlign = 2;
					break;
				case CacheFileFormat.MP3:
					CacheAudioChannelsComboBox.Enabled = false;
					CacheAudioSampleRateComboBox.Enabled = false;
					CacheAudioBitsPerSampleComboBox.Enabled = false;
					CacheAudioAverageBitsPerSecondComboBox.Enabled = true;
					CacheAudioBlockAlignComboBox.Enabled = false;
					SettingsManager.Options.CacheAudioChannels = AudioChannel.Mono;
					SettingsManager.Options.CacheAudioSampleRate = 22050;
					SettingsManager.Options.CacheAudioBitsPerSample = 16;
					SettingsManager.Options.CacheAudioAverageBitsPerSecond = 96000;
					SettingsManager.Options.CacheAudioBlockAlign = 2;
					break;
				case CacheFileFormat.ULaw:
				case CacheFileFormat.ALaw:
					CacheAudioChannelsComboBox.Enabled = false;
					CacheAudioSampleRateComboBox.Enabled = false;
					CacheAudioBitsPerSampleComboBox.Enabled = false;
					CacheAudioAverageBitsPerSecondComboBox.Enabled = false;
					CacheAudioBlockAlignComboBox.Enabled = false;
					SettingsManager.Options.CacheAudioChannels = AudioChannel.Mono;
					SettingsManager.Options.CacheAudioSampleRate = 8000;
					SettingsManager.Options.CacheAudioBitsPerSample = 8;
					SettingsManager.Options.CacheAudioAverageBitsPerSecond = 64000;
					SettingsManager.Options.CacheAudioBlockAlign = 1;
					break;
				default:
					break;
			}
			AddCacheBindings();
		}
	}
}