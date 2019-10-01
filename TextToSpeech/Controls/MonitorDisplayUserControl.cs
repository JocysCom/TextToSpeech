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
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion


		int changeValue = 0;

		


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


			//StatusTextBox.Text = "Capturing...";
			//var w = ImagePictureBox.Width;
			//var h = ImagePictureBox.Height;
			//var b = Basic.CaptureImage((int)BoxXUpDown.Value, (int)BoxYUpDown.Value, w, h);
			//var prefixBytes = GetPrefixRgbColorBytes();
			//var prefix = Basic.GetImageBytes(b, prefixBytes.Length);
			//if (!prefix.SequenceEqual(prefixBytes))
			//{
			//	var screen = Screen.PrimaryScreen;
			//	var sw = screen.Bounds.Width;
			//	var sh = screen.Bounds.Height;
			//	StatusTextBox.Text = "Wrong Bytes. Searching...";
			//	b = Basic.CaptureImage();
			//	//var screenBytes = GetImageBytes(b);
			//	var screenBytes = Basic.GetImageBytes(b);
			//	var index = JocysCom.ClassLibrary.Helper.IndexOfPattern(screenBytes, prefixBytes) / 3;
			//	StatusTextBox.Text = string.Format("Pixel Index  {0}...", index);
			//	if (index > -1)
			//	{
			//		var x = index % sw;
			//		var y = (index - x) / sw;
			//		BoxXUpDown.Value = x;
			//		BoxYUpDown.Value = y;
			//		StatusTextBox.Text += string.Format(" [{0}:{1}]", x, y);
			//	}
			//}
			//else
			//{
			//	StatusTextBox.Text += string.Format("Prefix found");
			//	var allBytes = Basic.GetImageBytes(b, w * h);
			//	var ms = new MemoryStream(allBytes);
			//	var br = new System.IO.BinaryReader(ms);
			//	br.Read(prefix, 0, prefix.Length);
			//	var messageSize = br.ReadInt32();
			//	var messageBytes = new byte[messageSize];
			//	br.Read(messageBytes, 0, messageSize);
			//	var message = System.Text.Encoding.Unicode.GetString(messageBytes);
			//	ResultsTextBox.Text = message;
			//}
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
