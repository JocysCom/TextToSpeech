using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Drawing;

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

		byte[] prefixBytes = System.Text.Encoding.ASCII.GetBytes("TextToSpeech");

		private void CreateImageButton_Click(object sender, EventArgs e)
		{
			var image = new Bitmap(ImagePictureBox.Width, ImagePictureBox.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			var g = Graphics.FromImage(image);
			g.Dispose();
			var ms = new MemoryStream();
			var br = new System.IO.BinaryWriter(ms);
			br.Write(prefixBytes);
			var messageBytes = System.Text.Encoding.Unicode.GetBytes(TestTextBox.Text);
			br.Write(messageBytes.Length);
			br.Write(messageBytes);
			var allBytes = image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb
				? JocysCom.ClassLibrary.Drawing.Effects.RgbToAlphaBytes(ms.ToArray())
				: ms.ToArray();
			Basic.SetImageBytes(image, allBytes);
			var bytes = Basic.GetImageBytes(image);
			ImagePictureBox.Image = image;
		}

		int currentX;
		int currentY;

		private void CaptureImageButton_Click(object sender, EventArgs e)
		{
			StatusTextBox.Text = "Capturing...";
			var w = ImagePictureBox.Width;
			var h = ImagePictureBox.Height;
			var b = Basic.CaptureImage(currentX, currentY, w, h);
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

	
	}
}
