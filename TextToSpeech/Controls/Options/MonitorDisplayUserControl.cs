using JocysCom.ClassLibrary.Collections;
using JocysCom.ClassLibrary.Configuration;
using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Drawing;
using JocysCom.TextToSpeech.Monitor.Capturing.Monitors;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class MonitorDisplayUserControl : UserControl
	{
		public MonitorDisplayUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
		}


		private void CreateImageButton_Click(object sender, EventArgs e)
		{
			ImagePictureBox.Image = Program._DisplayMonitor.CreateImage(MessageTextBox.Text, EnableBlankPixelsCheckBox.Checked);
		}

		private void CaptureImageButton_Click(object sender, EventArgs e)
		{
			//// Create a task and supply a user delegate by using a lambda expression. 
			//var taskA = new Task(() =>
			//{
			//	Invoke((Action)(() =>
			//	{
			//		StatusTextBox.Text = "Test Start...";
			//	}));
			//	var watch = new System.Diagnostics.Stopwatch();
			//	watch.Start();
			//	int z = 0;
			//	while (watch.ElapsedMilliseconds < 10000)
			//	{
			//		var image = Basic.CaptureImage(10, 10, 32, 4);
			//		var bytes = Basic.GetImageBytes(image);
			//		z++;
			//	}
			//	Invoke((Action)(() =>
			//	{
			//		StatusTextBox.Text = string.Format("Test End... {0} - {1}ms ", z, watch.ElapsedMilliseconds / z);
			//	}));

			//});
			//taskA.Start();
			//var a = 1;
			//if (a == 1)
			//	return;
			// Start the task.
			//taskA.Start();
			StatusTextBox.Text = "Capturing...";
			int change;
			string message;
			Program._DisplayMonitor.CaptureMessage(out change, out message);
			ResultsChangeTextBox.Text = change.ToString("X6");
			ResultsMessageTextBox.Text = message ?? "";
			// Select current screen.
			var list = (KeyValue<Screen, string>[])ScreensList.DataSource;
			ScreensList.SelectedItem = list.FirstOrDefault(x => x.Key == Program._DisplayMonitor.CurrentScreen);
		}


		private void ColorPrefixTextBox_TextChanged(object sender, EventArgs e)
		{
			var hexColors = ColorPrefixTextBox.Text;
			var intColors = DisplayMonitor.ColorsFromRgbs(hexColors);
			var values = intColors.Select(x => DisplayMonitor.GetValue(x));
			ColorPrefixValueTextBox.Text = string.Join(",", values);
		}

		private void ClearImageButton_Click(object sender, EventArgs e)
		{
			ImagePictureBox.Image = null;
		}

		private void ResetToDefaultButton_Click(object sender, EventArgs e)
		{
			SettingsManager.Options.DisplayMonitorResetSettings();
		}

		private void MonitorDisplayUserControl_Load(object sender, EventArgs e)
		{
			if (ControlsHelper.IsDesignMode(this))
				return;
			// To avoid validation problems, make sure to add DataBindings inside "Load" event and not inside Constructor.
			BoxXUpDown.DataBindings.Add(nameof(BoxXUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionX));
			BoxYUpDown.DataBindings.Add(nameof(BoxYUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionY));
			ColorPrefixTextBox.DataBindings.Add(nameof(ColorPrefixTextBox.Text), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPrefix));
			MonitorEnabledCheckBox.DataBindings.Add(nameof(MonitorEnabledCheckBox.Checked), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorEnabled));
			CopyIntervalUpDown.DataBindings.Add(nameof(CopyIntervalUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorInterval));
			MessagesTextBox.DataBindings.Add(nameof(MessagesTextBox.Text), Program._DisplayMonitor, nameof(Program._DisplayMonitor.MessagesReceived));
			HavePixelSpacesTextBox.DataBindings.Add(nameof(HavePixelSpacesTextBox.Text), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorHavePixelSpaces));
			UpdateScreenList();
		}



		void UpdateScreenList()
		{
			int i = 1;
			var list = Screen.AllScreens.Select(x =>
				new KeyValue<Screen, string>(x, string.Format("{0}. X={1},Y={2} [{3}x{4}]", i++, x.Bounds.X, x.Bounds.Y, x.Bounds.Width, x.Bounds.Height)))
				.ToArray();
			ScreensList.DataSource = list;
			ScreensList.DisplayMember = nameof(KeyValue.Value);
			ScreensList.ValueMember = nameof(KeyValue.Key);
			ScreensList.SelectedItem = list.FirstOrDefault(x => x.Key == Screen.PrimaryScreen);
		}

		private void CaptureScreenAndSave_Click(object sender, EventArgs e)
		{
			var screen = ((KeyValue<Screen, string>)ScreensList.SelectedItem).Key;
			byte[] screenBytes = null;
			Bitmap screenBitmap = null;
			Basic.CaptureImage(ref screenBitmap, screen);
			Basic.GetImageBytes(ref screenBytes, screenBitmap);
			var asm = new AssemblyInfo();
			var path = asm.GetAppDataPath(false, "Images\\Screen_{0:yyyyMMdd_HHmmss_fff}.png", DateTime.Now);
			var fi = new System.IO.FileInfo(path);
			if (!fi.Directory.Exists)
				fi.Directory.Create();
			screenBitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
			ControlsHelper.OpenPath(fi.Directory.FullName);
		}
	}
}
