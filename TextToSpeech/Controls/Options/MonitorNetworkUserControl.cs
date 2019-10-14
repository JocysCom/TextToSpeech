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
		}

		private void MonitorNetworkUserControl_Load(object sender, EventArgs e)
		{
			// To avoid validation problems, make sure to add DataBindings inside "Load" event and not inside Constructor.
			ControlsHelper.AddDataBinding(this, c => c._CapturingType, SettingsManager.Options, d => d.NetworkMonitorCapturingType);
			ControlsHelper.AddDataBinding(LogFolderTextBox, s => s.Text, SettingsManager.Options, d => d.NetworkMonitorLogFolder);
			ControlsHelper.AddDataBinding(LogEnabledCheckBox, s => s.Checked, SettingsManager.Options, d => d.LogEnable);
			ControlsHelper.AddDataBinding(LogFilterTextTextBox, s => s.Text, SettingsManager.Options, d => d.LogText);
			ControlsHelper.AddDataBinding(LogPlaySoundCheckBox, s => s.Text, SettingsManager.Options, d => d.LogSound);
			if (string.IsNullOrEmpty(SettingsManager.Options.NetworkMonitorLogFolder))
				SettingsManager.Options.NetworkMonitorLogFolder = Capturing.Monitors.NetworkMonitor.GetLogsPath(true);
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

		#region Log 

		private void OpenButton_Click(object sender, EventArgs e)
		{

		}

		private void HowToButton_Click(object sender, EventArgs e)
		{

		}

		#endregion

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
