using JocysCom.ClassLibrary.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public byte[] ColorPrefixBytes;

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
			var newBytes = new byte[allBytes.Length * 2];
			for (int p = 0; p < allBytes.Length; p += 3)
			{
				newBytes[p * 2 + 0] = allBytes[p + 0];
				newBytes[p * 2 + 1] = allBytes[p + 1];
				newBytes[p * 2 + 2] = allBytes[p + 2];
			}
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
			var imageBytes = Basic.GetImageBytes(image);
			Basic.SetImageBytes(image, newBytes);
			g.Dispose();
			return image;
		}

		#endregion

	}
}
