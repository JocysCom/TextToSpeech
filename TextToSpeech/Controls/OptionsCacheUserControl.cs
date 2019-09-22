using System;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using JocysCom.ClassLibrary.Controls;
using System.Reflection;
using System.Linq.Expressions;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class OptionsCacheUserControl : UserControl
	{
		public OptionsCacheUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.AddDataBinding(CacheDataWriteCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataWrite);
			ControlsHelper.AddDataBinding(CacheDataReadCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataRead);
			ControlsHelper.AddDataBinding(CacheDataGeneralizeCheckBox, c => c.Checked, SettingsManager.Options, d => d.CacheDataGeneralize);
		}

		private void OpenCacheButton_Click(object sender, EventArgs e)
		{
			var dir = MainHelper.GetCreateCacheFolder();
			MainHelper.OpenUrl(dir.FullName);
		}

		string _CacheMessageFormat;

		private void OptionsCacheUserControl_Load(object sender, EventArgs e)
		{
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
				throw new ArgumentException("Bytes should not be negative", "value");
			}
			var mag = (int)Math.Max(0, Math.Log(value, 1024));
			var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
			return String.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
		}

	}
}
