using System.Windows.Forms;
using JocysCom.ClassLibrary.Controls;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class MonitorClipboardUserControl : UserControl
	{
		public MonitorClipboardUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			ControlsHelper.AddDataBinding(MonitorEnabledCheckBox, s => s.Checked, SettingsManager.Options, d => d.ClipboardMonitorEnabled);
			ControlsHelper.AddDataBinding(CopyIntervalUpDown, s => s.Value, Program._ClipboardMonitor, d => d.CopyInterval);
			ControlsHelper.AddDataBinding(MessagesTextBox, s => s.Text, Program._ClipboardMonitor, d => d.MessagesReceived);
		}
	}
}
