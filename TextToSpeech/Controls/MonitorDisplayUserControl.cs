using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Capturing.Monitors;
using System;
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
			BoxXUpDown.DataBindings.Add(nameof(BoxXUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionX));
			BoxYUpDown.DataBindings.Add(nameof(BoxYUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionY));
			ColorPrefixTextBox.DataBindings.Add(nameof(ColorPrefixTextBox.Text), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPrefix));
			MonitorEnabledCheckBox.DataBindings.Add(nameof(MonitorEnabledCheckBox.Checked), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorEnabled));
			CopyIntervalUpDown.DataBindings.Add(nameof(CopyIntervalUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorInterval));
			MessagesTextBox.DataBindings.Add(nameof(MessagesTextBox.Text), Program._DisplayMonitor, nameof(Program._DisplayMonitor.MessagesReceived));
		}


		private void CreateImageButton_Click(object sender, EventArgs e)
		{
			ImagePictureBox.Image = Program._DisplayMonitor.CreateImage(MessageTextBox.Text);
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
	}
}
