using JocysCom.ClassLibrary.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class MonitorNetworkUserControl : UserControl, INotifyPropertyChanged
	{
		public MonitorNetworkUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.AddDataBinding(this, c => c._CapturingType, SettingsManager.Options, d => d.NetworkMonitorCapturingType);
			UpdateWinCapState();
			WinPcapRadioButton.CheckedChanged += WinPcapRadioButton_CheckedChanged;
			SocPcapRadioButton.CheckedChanged += WinPcapRadioButton_CheckedChanged;
		}

		private void WinPcapRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(_CapturingType));
		}

		public Capturing.CapturingType _CapturingType
		{
			get
			{
				return SocPcapRadioButton.Checked
					? Capturing.CapturingType.SocPcap
					: Capturing.CapturingType.WinPcap;
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

	}
}
