using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Capturing.Monitors;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace JocysCom.TextToSpeech.Monitor
{
	static partial class Program
	{
		public static UdpMonitor _UdpMonitor;
		public static ClipboardMonitor _ClipboardMonitor;
		public static NetworkMonitor _NetworkMonitor;

		public static List<VoiceListItem> PlugIns = new List<VoiceListItem>() { new WowListItem() };

		public static VoiceListItem MonitorItem
		{
			get
			{
				var item = PlugIns.FirstOrDefault(x => x.Name == SettingsManager.Options.ProgramComboBoxText);
				if (item != null)
					return item;
				return PlugIns.FirstOrDefault();
			}
		}

		public static void InitMonitors()
		{
			// Initialize UDP Monitor.
			_UdpMonitor = new UdpMonitor();
			_UdpMonitor.PortNumber = SettingsManager.Options.UdpMonitorPort;
			_UdpMonitor.MessageReceived += _Monitor_MessageReceived;
			if (SettingsManager.Options.UdpMonitorEnabled)
				_UdpMonitor.Start();
			// Initialize Clipboard Monitor.
			_ClipboardMonitor = new ClipboardMonitor();
			_ClipboardMonitor.CopyInterval = SettingsManager.Options.ClipboardMonitorInterval;
			_ClipboardMonitor.MessageReceived += _Monitor_MessageReceived;
			if (SettingsManager.Options.ClipboardMonitorEnabled)
				_ClipboardMonitor.Start();
			// Initialize Network Monitor.
			_NetworkMonitor = new NetworkMonitor();
			_NetworkMonitor.MessageReceived += _Monitor_MessageReceived;
			if (SettingsManager.Options.NetworkMonitorEnabled)
				_NetworkMonitor.Start();
			// Start monitoring property changes.
			SettingsManager.Options.PropertyChanged += Options_PropertyChanged;

		}

		public static void DisposeMonitors()
		{
			_UdpMonitor.Dispose();
			_ClipboardMonitor.Dispose();
			_NetworkMonitor.Dispose();
		}

		private static void _Monitor_MessageReceived(object sender, ClassLibrary.EventArgs<string> e)
		{
			// Invoke on UI thread.
			ControlsHelper.BeginInvoke(() =>
			{
				Audio.Global.AddMessageToPlay(e.Data);
			});
		}

		static void Options_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var enabledCanged = e.PropertyName == nameof(SettingsManager.Options.MonitorsEnabled);
			var en = SettingsManager.Options.MonitorsEnabled;

			// UDP Monitor Properties.
			if (e.PropertyName == nameof(SettingsManager.Options.UdpMonitorPort))
				_UdpMonitor.PortNumber = SettingsManager.Options.UdpMonitorPort;
			if (e.PropertyName == nameof(SettingsManager.Options.UdpMonitorEnabled) || enabledCanged)
			{
				if (SettingsManager.Options.UdpMonitorEnabled && en)
					_UdpMonitor.Start();
				else
					_UdpMonitor.Stop();
			}
			// Clipboard Monitor Properties.
			if (e.PropertyName == nameof(SettingsManager.Options.ClipboardMonitorInterval))
				_ClipboardMonitor.CopyInterval = SettingsManager.Options.ClipboardMonitorInterval;
			if (e.PropertyName == nameof(SettingsManager.Options.ClipboardMonitorEnabled) || enabledCanged)
			{
				if (SettingsManager.Options.ClipboardMonitorEnabled && en)
					_ClipboardMonitor.Start();
				else
					_ClipboardMonitor.Stop();
			}
			// Network Monitor Properties.
			if (e.PropertyName == nameof(SettingsManager.Options.NetworkMonitorCapturingType))
				_NetworkMonitor.CapturingType = SettingsManager.Options.NetworkMonitorCapturingType;
			if (e.PropertyName == nameof(SettingsManager.Options.NetworkMonitorEnabled) || enabledCanged)
			{
				if (SettingsManager.Options.NetworkMonitorEnabled && en)
					_NetworkMonitor.Start();
				else
					_NetworkMonitor.Stop();
			}
		}

	}
}
