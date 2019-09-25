using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Drawing;
using JocysCom.ClassLibrary.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class OptionsCapturingUserControl : UserControl, INotifyPropertyChanged
	{
		public OptionsCapturingUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.AddDataBinding(this, c => c._CapturingType, SettingsManager.Options, d => d.CapturingType);
			UpdateWinCapState();
			WinPcapRadioButton.CheckedChanged += WinPcapRadioButton_CheckedChanged;
			SocPcapRadioButton.CheckedChanged += WinPcapRadioButton_CheckedChanged;
			DisplayRadioButton.CheckedChanged += WinPcapRadioButton_CheckedChanged;
		}

		private void WinPcapRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(_CapturingType));
		}

		public Capturing.CapturingType _CapturingType
		{
			get
			{
				if (SocPcapRadioButton.Checked)
					return Capturing.CapturingType.SocPcap;
				if (WinPcapRadioButton.Checked)
					return Capturing.CapturingType.WinPcap;
				return Capturing.CapturingType.Display;
			}
			set
			{
				switch (value)
				{
					case Capturing.CapturingType.WinPcap:
						WinPcapRadioButton.Checked = true;
						break;
					case Capturing.CapturingType.SocPcap:
						SocPcapRadioButton.Checked = true;
						break;
					default:
						DisplayRadioButton.Checked = true;
						break;
				}
				OnPropertyChanged();
			}
		}

		public void UpdateWinCapState()
		{
			var version = Capturing.WinPcapHelper.GetWinPcapVersion();
			if (version != null)
			{
				WinPcapRadioButton.Text = string.Format("WinPcap {0}", version.ToString());
				WinPcapRadioButton.Enabled = true;
			}
			else
			{
				WinPcapRadioButton.Text = "WinPcap";
				WinPcapRadioButton.Enabled = false;
			}
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

		byte[] GetPrefixRgbColorBytes()
		{
			var rx = new Regex("#[0-9AF]{6}", RegexOptions.IgnoreCase);
			var text = ColorPrefixTextBox.Text;
			byte[] prefixBytes;
			var matches = rx.Matches(ColorPrefixTextBox.Text);
			if (string.IsNullOrEmpty(text))
			{
				prefixBytes = System.Text.Encoding.ASCII.GetBytes("TextToSpeech");
			}
			else if (matches.Count > 0)
			{
				var ms = new MemoryStream();
				var bw = new BinaryWriter(ms);
				foreach (Match match in matches)
				{
					var hex = match.Value;
					var v = int.Parse(hex.Substring(1, 6), System.Globalization.NumberStyles.HexNumber);
					var c = Color.FromArgb(v);
					// Native bitmap color byte order for 32bpp: [B,G,R,A...], 24bpp: [B,G,R...].
					bw.Write(c.B);
					bw.Write(c.G);
					bw.Write(c.R);
				}
				prefixBytes = ms.ToArray();
			}
			else
			{
				prefixBytes = System.Text.Encoding.ASCII.GetBytes(text);
			}
			return prefixBytes;
		}

		int changeValue = 0;

		

		private void CreateImageButton_Click(object sender, EventArgs e)
		{

			// Image: prefix[6] + change[1] + encoding[1] + [size[1] + message[X]]
			// 


			// if encoding = 0, then clipboard.
			var image = new Bitmap(ImagePictureBox.Width, ImagePictureBox.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			var g = Graphics.FromImage(image);
			// Create object to write.
			var prefixBytes = GetPrefixRgbColorBytes();
			changeValue += 0x20;
			//var encodingCode = (int)JocysCom.ClassLibrary.Text.EncodingType.UCS2LE * 0x20;
			var messageBytes = AddMessageTextCheckBox.Checked
				? System.Text.Encoding.Unicode.GetBytes(TestTextBox.Text)
				// Add RGB pixel.
				: new byte[] { (byte)(changeValue & 0xFF), 0x00, 0x00 };
			// Add pixels.
			var ms = new MemoryStream();
			var br = new System.IO.BinaryWriter(ms);
			br.Write(prefixBytes);
			br.Write(messageBytes.Length);
			br.Write(messageBytes);

			var allBytes = image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb
				? JocysCom.ClassLibrary.Drawing.Basic.BppFrom24To32Bit(ms.ToArray())
				: ms.ToArray();

			var imageBytes = Basic.GetImageBytes(image);

			var colors = Basic.BytesToColors(allBytes, true);
			var boxSize = (int)BoxSizeUpDown.Value;
			for (int c = 0; c < colors.Length; c++)
			{
				var x = c * boxSize;
				var pen = new SolidBrush(colors[c]);
				g.FillRectangle(pen, x, 0, boxSize, boxSize);
			}

			//Basic.SetImageBytes(image, imageBytes);
			g.Dispose();
			ImagePictureBox.Image = image;
		}

		int currentX;
		int currentY;

		private void CaptureImageButton_Click(object sender, EventArgs e)
		{


			// Create a task and supply a user delegate by using a lambda expression. 
			var taskA = new Task(() =>
			{
				Invoke((Action)(() =>
				{
					StatusTextBox.Text = "Test Start...";
				}));
				var watch = new System.Diagnostics.Stopwatch();
				watch.Start();
				int z = 0;
				while (watch.ElapsedMilliseconds < 10000)
				{
					var image = Basic.CaptureImage(10, 10, 32, 4);
					var bytes = Basic.GetImageBytes(image);
					z++;
				}
				Invoke((Action)(() =>
				{
					StatusTextBox.Text = string.Format("Test End... {0} - {1}ms ", z, watch.ElapsedMilliseconds / z);
				}));

			});
			taskA.Start();
			var a = 1;
			if (a == 1)
				return;
			// Start the task.
			//taskA.Start();


			StatusTextBox.Text = "Capturing...";
			var w = ImagePictureBox.Width;
			var h = ImagePictureBox.Height;
			var b = Basic.CaptureImage(currentX, currentY, w, h);
			var prefixBytes = GetPrefixRgbColorBytes();
			var prefix = Basic.GetImageBytes(b, prefixBytes.Length);
			if (!prefix.SequenceEqual(prefixBytes))
			{
				var screen = Screen.PrimaryScreen;
				var sw = screen.Bounds.Width;
				var sh = screen.Bounds.Height;
				StatusTextBox.Text = "Wrong Bytes. Searching...";
				b = Basic.CaptureImage();
				//var screenBytes = GetImageBytes(b);
				var screenBytes = Basic.GetImageBytes(b);
				var index = JocysCom.ClassLibrary.Helper.IndexOfPattern(screenBytes, prefixBytes) / 3;
				StatusTextBox.Text = string.Format("Pixel Index  {0}...", index);
				if (index > -1)
				{
					var x = index % sw;
					var y = (index - x) / sw;
					currentX = x;
					currentY = y;
					StatusTextBox.Text += string.Format(" [{0}:{1}]", x, y);
				}
			}
			else
			{
				StatusTextBox.Text += string.Format("Prefix found");
				var allBytes = Basic.GetImageBytes(b, w * h);
				var ms = new MemoryStream(allBytes);
				var br = new System.IO.BinaryReader(ms);
				br.Read(prefix, 0, prefix.Length);
				var messageSize = br.ReadInt32();
				var messageBytes = new byte[messageSize];
				br.Read(messageBytes, 0, messageSize);
				var message = System.Text.Encoding.Unicode.GetString(messageBytes);
				ResultsTextBox.Text = message;
			}
		}

		private void EnableCapturingCheckBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void CopyWowTextButton_Click(object sender, EventArgs e)
		{

		}




		#region WoW


		static Process GetProcess()
		{
			var item = new PlugIns.WowListItem();
			foreach (var name in item.Process)
			{
				var p = Process.GetProcessesByName(name).FirstOrDefault();
				if (p != null)
					return p;
			}
			return null;
		}


		public static bool ActivateWindow()
		{
			var process = GetProcess();
			if (process == null)
				return false;
			NativeMethods.AppActivate(process.Id);
			return true;
		}

		#endregion
	}
}
