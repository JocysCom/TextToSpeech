using JocysCom.ClassLibrary.Controls;
using NAudio.Wave;
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
			CacheAudioFormatComboBox.DataSource = new WaveFormatEncoding[]
			{
				 WaveFormatEncoding.MuLaw,
				 WaveFormatEncoding.Pcm,
				 WaveFormatEncoding.MpegLayer3,
			};
			CacheAudioChannelsComboBox.DataSource = (AudioChannel[])Enum.GetValues(typeof(AudioChannel));
			// Audio Sample Rate.
			CacheAudioSampleRateComboBox.DataSource = new int[] { 8000, 11025, 22050, 44100, 48000 };
			// Audio Bits Per Sample.
			CacheAudioBitsPerSampleComboBox.DataSource = new int[] { 8, 16 };
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
			// To avoid validation problems, make sure to add DataBindings inside "Load" event and not inside Constructor.
			ControlsHelper.AddDataBinding(CacheDataWriteCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataWrite);
			ControlsHelper.AddDataBinding(CacheDataReadCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataRead);
			ControlsHelper.AddDataBinding(CacheDataGeneralizeCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataGeneralize);
			// Bind Cache format options.
			ControlsHelper.AddDataBinding(CacheAudioConvertCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheAudioConvert);
			ControlsHelper.AddDataBinding(CacheAudioFormatComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioFormat);
			ControlsHelper.AddDataBinding(CacheAudioChannelsComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioChannels);
			ControlsHelper.AddDataBinding(CacheAudioSampleRateComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioSampleRate);
			ControlsHelper.AddDataBinding(CacheAudioBitsPerSampleComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioBitsPerSample);
			ControlsHelper.AddDataBinding(CacheAudioAverageBitsPerSecondComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioAverageBitsPerSecond);
			ControlsHelper.AddDataBinding(CacheAudioBlockAlignComboBox, c => c.SelectedItem, SettingsManager.Options, d => d.CacheAudioBlockAlign);
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
			return String.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
		}

	}
}
