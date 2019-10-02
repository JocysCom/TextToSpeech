using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Drawing;
using JocysCom.TextToSpeech.Monitor.Capturing;
using JocysCom.TextToSpeech.Monitor.Capturing.Monitors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class MonitorDisplayUserControl : UserControl
	{
		public MonitorDisplayUserControl()
		{
			InitializeComponent();
			BoxXUpDown.DataBindings.Add(nameof(BoxXUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionX));
			BoxYUpDown.DataBindings.Add(nameof(BoxYUpDown.Value), SettingsManager.Options, nameof(SettingsManager.Options.DisplayMonitorPositionY));
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

		}


		private void ColorPrefixTextBox_TextChanged(object sender, EventArgs e)
		{
			var hexColors = ColorPrefixTextBox.Text;
			var intColors = DisplayMonitor.ColorsFromRgbs(hexColors);
			var values = intColors.Select(x => DisplayMonitor.GetValue(x));
			ColorPrefixValueTextBox.Text = string.Join(",", values);
		}

	}
}
