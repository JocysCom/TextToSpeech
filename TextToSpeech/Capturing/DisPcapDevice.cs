using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

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

		/// <summary>
		/// Convert 0-511 integer to [A]RGB integer. Only RGB values affected.
		/// </summary>
		public static int GetColor(int v)
		{
			// Example:
			// var cols = new List<string>();
			// for (int i = 0; i < 512; i++)
			// 	cols.Add((Capturing.DisPcapDevice.GetColor(i) & // 0xFFFFFF).ToString("X6"));
			// MessageBox.Show(string.Join(" ", cols));
			//
			// DEC   OCT   ARGB HEX
			// ---   ---   --------
			//   0 =   0 = FF000000
			//   1 =   1 = FF000020
			//   2 =   2 = FF000040
			//   3 =   3 = FF000060
			//   4 =   4 = FF000080
			//   5 =   5 = FF0000A0
			//   6 =   6 = FF0000C0
			//   7 =   7 = FF0000E0
			//   8 =  10 = FF002000
			//   9 =  11 = FF002020
			//  10 =  12 = FF002040
			// ...   ...   ........
			// 511 = 777 = FFE0E0E0
			var num = Convert.ToString(v, 8);
			var rgb = num.Select(x => int.Parse(x.ToString()) * 0x20).ToArray();
			Array.Reverse(rgb);
			var b = rgb.Length > 0 ? rgb[0] : 0;
			var g = rgb.Length > 1 ? rgb[1] : 0;
			var r = rgb.Length > 2 ? rgb[2] : 0;
			var c = (0xFF << 24) | (r << 16) | (g << 8) | b;
			return c;
		}

		/// <summary>
		/// Convert [A]RGB integer to 0-511 integer. Only RGB values affected.
		/// </summary>
		public static int GetValue(int color)
		{
			// Example:
			// var vals = new List<int>();
			// for (int i = 0; i < cols.Count; i++)
			// 	vals.Add(Capturing.DisPcapDevice.GetValue(Convert.ToInt32(cols[i], 16)));
			// MessageBox.Show(string.Join(" ", vals));
			//
			//  ARGB HEX   OCT   DEC 
			//  --------   ---   --- 
			//  FF000000 =   0 =   0
			//  FF000020 =   1 =   1
			//  FF000040 =   2 =   2
			//  FF000060 =   3 =   3
			//  FF000080 =   4 =   4
			//  FF0000A0 =   5 =   5
			//  FF0000C0 =   6 =   6
			//  FF0000E0 =   7 =   7
			//  FF002000 =  10 =   8
			//  FF002020 =  11 =   9
			//  FF002040 =  12 =  10
			//  ........   ...   ... 
			//  FFE0E0E0 = 777 = 511
			var r = (color >> 16) & 0xFF;
			var g = (color >> 8) & 0xFF;
			var b = color & 0xFF; ;
			// Round.
			var r8 = Math.Round((decimal)r / (decimal)0x20, 0);
			var g8 = Math.Round((decimal)g / (decimal)0x20, 0);
			var b8 = Math.Round((decimal)b / (decimal)0x20, 0);
			var oct = string.Format("{0}{1}{2}", r8, g8, b8).TrimStart('0');
			if (oct == "")
				oct = "0";
			var v = Convert.ToInt32(oct, 8);
			return v;
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
