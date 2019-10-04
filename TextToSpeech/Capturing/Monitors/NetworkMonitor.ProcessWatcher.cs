using JocysCom.ClassLibrary;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public partial class NetworkMonitor : MonitorBase
	{

		ManagementEventWatcher startWatch;
		ManagementEventWatcher stopWatch;

		public event EventHandler<EventArgs<string>> ProcessChanged;

		/// <summary>Monitors starting and shutdown of the processes.</summary>
		void InitWatcher()
		{
			startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
			startWatch.EventArrived += StartWatch_EventArrived;
			startWatch.Start();
			stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
			stopWatch.EventArrived += StopWatch_EventArrived;
			stopWatch.Start();
			CheckProcessStatus();
		}

		/// <summary>New Process started.</summary>
		void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
		{
			var name = (string)e.NewEvent.Properties["ProcessName"].Value;
			var item = Program.MonitorItem;
			if (item.Process.Contains(name.ToLower()))
				CheckProcessStatus();
		}

		/// <summary>Process closed.</summary>
		void StopWatch_EventArrived(object sender, EventArrivedEventArgs e)
		{
			var name = (string)e.NewEvent.Properties["ProcessName"].Value;
			var item = Program.MonitorItem;
			if (item.Process.Contains(name.ToLower()))
				CheckProcessStatus();
		}

		string GetExecutableName()
		{
			var mi = Program.MonitorItem;
			if (mi == null)
				return null;
			// Get names of executables to look for.
			var exeNames = mi.Process.Select(x => x.ToLower()).ToArray();
			// Query to get executable paths of all processes.
			var wmiQueryString = "SELECT ExecutablePath FROM Win32_Process WHERE ExecutablePath <> Null";
			//var paths = new List<string>();
			using (var searcher = new ManagementObjectSearcher(wmiQueryString))
			{
				using (var results = searcher.Get())
				{
					var mos = results.Cast<ManagementObject>().ToList();
					foreach (var mo in mos)
					{
						if (mo == null)
							continue;
						var path = (string)mo["ExecutablePath"];
						var name = Path.GetFileName(path).ToLower();
						//paths.Add(name);
						// If process found then return executable name.
						if (exeNames.Contains(name))
							return name;
					}
				}
			}
			return null;
		}

		void CheckProcessStatus()
		{
			var name = GetExecutableName();
			OnEvent(ProcessChanged, name);
			SetFilter(Program.MonitorItem);
		}

		public void DisposeWatcher()
		{
			if (startWatch != null)
			{
				startWatch.Stop();
				startWatch.Dispose();
				startWatch = null;
			}
			if (stopWatch != null)
			{
				stopWatch.Stop();
				stopWatch.Dispose();
				stopWatch = null;
			}
		}

	}
}
