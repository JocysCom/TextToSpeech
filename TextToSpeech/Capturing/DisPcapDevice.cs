using SharpPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class DisPcapDevice //: ICaptureDevice
	{
		Stopwatch sw = new Stopwatch();
		public static Bitmap CaptureImage(int x, int y, int w, int h)
		{
			var b = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			using (var g = Graphics.FromImage(b))
			{
				g.CopyFromScreen(x, y, 0, 0, new Size(100, 100), CopyPixelOperation.SourceCopy);
				g.DrawLine(Pens.Black, new Point(0, 27), new Point(99, 27));
				g.DrawLine(Pens.Black, new Point(0, 73), new Point(99, 73));
				g.DrawLine(Pens.Black, new Point(52, 0), new Point(52, 99));
				g.DrawLine(Pens.Black, new Point(14, 0), new Point(14, 99));
				g.DrawLine(Pens.Black, new Point(85, 0), new Point(85, 99));
			}
			return b;
		}

		//private void button1_Click(object sender, EventArgs e)
		//{
		//	Bitmap bmp = null;
		//	sw.Restart();
		//	for (int i = 0; i < 1000; i++)
		//	{
		//		bmp = DisPcapDevice.CaptureImage(390, 420);
		//	}
		//	sw.Stop();
		//	Console.WriteLine(sw.ElapsedMilliseconds);
		//}

	}
}
