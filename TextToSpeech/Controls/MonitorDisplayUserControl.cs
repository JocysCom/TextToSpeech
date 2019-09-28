using JocysCom.ClassLibrary.Drawing;
using JocysCom.ClassLibrary.Win32;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.ComponentModel;
using System.Diagnostics;
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

		byte[] GetPrefixRgbColorBytes()
		{
			var text = ColorPrefixTextBox.Text;
			byte[] prefixBytes;
			if (string.IsNullOrEmpty(text))
			{
				prefixBytes = System.Text.Encoding.ASCII.GetBytes("TextToSpeech");
				return prefixBytes;
			}
			var intColors = DisPcapDevice.ColorsFromRgbs(text);
			if (intColors.Length > 0)
			{
				prefixBytes = Basic.ColorsToBytes(intColors);
				return prefixBytes;
			}
			prefixBytes = System.Text.Encoding.ASCII.GetBytes(text);
			return prefixBytes;
		}

		int changeValue = 0;

		private void CreateImageButton_Click(object sender, EventArgs e)
		{

		}

		private void CaptureImageButton_Click(object sender, EventArgs e)
		{

		}

		private void ColorPrefixTextBox_TextChanged(object sender, EventArgs e)
		{

		}

	}
}
