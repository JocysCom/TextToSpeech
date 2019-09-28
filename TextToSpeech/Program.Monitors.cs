using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System.ComponentModel;

namespace JocysCom.TextToSpeech.Monitor
{
	static partial class Program
	{
		public static UdpMonitor _UdpMonitor;
		public static ClipboardMonitor _ClipboardMonitor;

		public static void InitMonitors()
		{
			// Init UDP Monitor.
			_UdpMonitor = new UdpMonitor();
			_UdpMonitor.PortNumber = SettingsManager.Options.UdpMonitorPort;
			_UdpMonitor.MessageReceived += _Monitor_MessageReceived;
			if (SettingsManager.Options.UdpMonitorEnabled)
				_UdpMonitor.Start();
			// Init Clipboard Monitor.
			_ClipboardMonitor = new ClipboardMonitor();
			_ClipboardMonitor.CopyInterval = SettingsManager.Options.ClipboardMonitorInterval;
			_ClipboardMonitor.MessageReceived += _Monitor_MessageReceived;
			if (SettingsManager.Options.ClipboardMonitorEnabled)
				_ClipboardMonitor.Start();
			// Start monitorig property changes.
			SettingsManager.Options.PropertyChanged += Options_PropertyChanged;

		}

		public static void DisposeMonitors()
		{
			_UdpMonitor.Dispose();
			_ClipboardMonitor.Dispose();
		}

		private static void _Monitor_MessageReceived(object sender, ClassLibrary.EventArgs<string> e)
		{
			// Invoke ok UI thread.
			ControlsHelper.BeginInvoke(() =>
			{
				TopForm.AddMessageToPlay(e.Data);
			});
		}

		static void Options_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			// UDP Monitor Properties.
			if (e.PropertyName == nameof(SettingsManager.Options.UdpMonitorPort))
				_UdpMonitor.PortNumber = SettingsManager.Options.UdpMonitorPort;
			if (e.PropertyName == nameof(SettingsManager.Options.UdpMonitorEnabled))
			{
				if (SettingsManager.Options.UdpMonitorEnabled)
					_UdpMonitor.Start();
				else
					_UdpMonitor.Stop();
			}
			// Clipboard Monitor Properties.
			if (e.PropertyName == nameof(SettingsManager.Options.ClipboardMonitorInterval))
				_ClipboardMonitor.CopyInterval = SettingsManager.Options.ClipboardMonitorInterval;
			if (e.PropertyName == nameof(SettingsManager.Options.ClipboardMonitorEnabled))
			{
				if (SettingsManager.Options.ClipboardMonitorEnabled)
					_ClipboardMonitor.Start();
				else
					_ClipboardMonitor.Stop();
			}
			//if (e.PropertyName == nameof(SettingsManager.Options.CapturingType))
			//{
			//	Program.TopForm.StopNetworkMonitor();
			//	Program.TopForm.StartNetworkMonitor();
			//}
		}

	}
}
