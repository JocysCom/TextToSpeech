using JocysCom.ClassLibrary.Drawing;
using System;
using System.Drawing;
using System.IO;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public partial class DisplayMonitor : MonitorBase
	{
		private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{


			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (IsRunning)
					_Timer.Start();
			}
		}

		System.Timers.Timer _Timer;

		public int ScanInterval
		{
			get { return _Interval; }
			set
			{
				var isRunning = IsRunning;
				if (isRunning)
					_Timer.Stop();
				_Interval = value;
				if (isRunning)
					Start();
				OnPropertyChanged();
			}
		}

		int _Interval = 200;

		public override void Start()
		{
			lock (monitorLock)
			{
				if (IsDisposing)
					return;
				if (_Timer != null)
				{
					// Server is already running;
					return;
				}
				_Timer = new System.Timers.Timer();
				_Timer.Interval = ScanInterval;
				_Timer.AutoReset = false;
				_Timer.Elapsed += _Timer_Elapsed;
				_Timer.Start();
				_IsRunning = true;
			}
		}

		public override void Stop()
		{
			lock (monitorLock)
			{
				// If server is running then...
				if (_Timer != null)
				{
					_Timer.Dispose();
					_Timer = null;
				}
				_IsRunning = false;
			}
		}

		#region Create Image

		public int _CurrentChangeValue;

		byte[] ColorPrefixBytes;

		public void SetColorPrefix(byte[] rgbPrefixBytes)
		{
			ColorPrefixBytes = InsertBlankPixels(rgbPrefixBytes);
		}

		public static byte[] InsertBlankPixels(byte[] bytes)
		{
			// Insert blank pixels in the middle.
			var data = new byte[bytes.Length * 2];
			for (int p = 0; p < bytes.Length; p += 3)
			{
				data[p * 2 + 0] = bytes[p + 0];
				data[p * 2 + 1] = bytes[p + 1];
				data[p * 2 + 2] = bytes[p + 2];
			}
			return data;
		}

		/// <summary>
		/// Image: prefix[6] + change[1] + size[1] + message_bytes
		/// </summary>
		public Bitmap CreateImage(string message)
		{
			var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
			// Create object to write.
			_CurrentChangeValue++;
			// Reset count if too big.
			if (_CurrentChangeValue > 0xFFFFFF)
				_CurrentChangeValue = 0;
			var ms = new MemoryStream();
			var br = new System.IO.BinaryWriter(ms);
			// Write Prefix bytes.
			br.Write(ColorPrefixBytes);
			// Write change value.
			var changePixelBytes = Basic.ColorsToBytes(new[] { _CurrentChangeValue });
			br.Write(changePixelBytes);
			// Write message size value.		
			var sizePixelBytes = Basic.ColorsToBytes(new[] { messageBytes.Length });
			br.Write(sizePixelBytes);
			// Write message bytes.
			br.Write(messageBytes);
			// Write missing bytes for complete color.
			var mod = messageBytes.Length % 3;
			for (int i = 0; i < mod; i++)
				br.Write((byte)0);
			var allBytes = ms.ToArray();
			br.Dispose();
			// Insert blank pixels in the middle.
			var newBytes = InsertBlankPixels(allBytes);
			var pixelCount = newBytes.Length / 3;
			// Set image format.
			var format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
			// If format contains Alpha color then add alpha color.
			if (format == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
				newBytes = JocysCom.ClassLibrary.Drawing.Basic.BppFrom24To32Bit(newBytes);
			// Image width will be double since every second pixel is blank.
			var image = new Bitmap(pixelCount, 1, format);
			var g = Graphics.FromImage(image);
			// Image bytes size must match new bytes size.
			// var imageBytes = Basic.GetImageBytes(image);
			Basic.SetImageBytes(image, newBytes);
			g.Dispose();
			return image;
		}

		public string CaptureText()
		{
			var x = SettingsManager.Options.DisplayMonitorPositionX;
			var y = SettingsManager.Options.DisplayMonitorPositionY;
			var screen = System.Windows.Forms.Screen.PrimaryScreen;
			var sw = screen.Bounds.Width;
			var sh = screen.Bounds.Height;
			var prefix = ColorPrefixBytes;
			// Take screenshot of the line.
			var image = Basic.CaptureImage(x, y, w: -x, 1);
			var bytes = Basic.GetImageBytes(image);
			var index = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, prefix);
			if (index > -1)
			{
				//	StatusTextBox.Text = "Wrong Bytes. Searching...";
				image = Basic.CaptureImage();
				bytes = Basic.GetImageBytes(image);
				index = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, prefix);
				//	StatusTextBox.Text = string.Format("Pixel Index  {0}...", index);
				if (index > -1)
				{
					// Get new coordinates of image.
					var newX = index % sw;
					var newY = (index - x) / sw;
					SettingsManager.Options.DisplayMonitorPositionX = newX;
					SettingsManager.Options.DisplayMonitorPositionY = newY;
					//StatusTextBox.Text += string.Format(" [{0}:{1}]", x, y);
				}
			}
			if (index > -1)
			{
				//	StatusTextBox.Text += string.Format("Prefix found");
				//var allBytes = Basic.GetImageBytes(image, w * h);
				//	var ms = new MemoryStream(allBytes);
				//	var br = new System.IO.BinaryReader(ms);
				//	br.Read(prefix, 0, prefix.Length);
				//	var messageSize = br.ReadInt32();
				//	var messageBytes = new byte[messageSize];
				//	br.Read(messageBytes, 0, messageSize);
				//	var message = System.Text.Encoding.Unicode.GetString(messageBytes);
				//	ResultsTextBox.Text = message;
			}
			return null;
		}

		public int IndexOfPattern(byte[] bytes)
		{
			var index = JocysCom.ClassLibrary.Text.Helper.IndexOf(bytes, ColorPrefixBytes);
			return index;
		}

		#endregion

	}
}
