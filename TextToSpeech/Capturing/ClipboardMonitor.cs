using JocysCom.ClassLibrary.Processes;
using JocysCom.ClassLibrary.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class ClipboardMonitor: MonitorBase
	{

		public ClipboardMonitor()
		{
		}

		private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			var success = ActivateWindow();
			if (!success)
				return;
			var process = GetProcess();
			KeyboardHelper.SendKey("^(A)", process.ProcessName);
			KeyboardHelper.SendKey("^(C)", process.ProcessName);
		}

		#region Process

		static Process GetProcess()
		{
			var item = new PlugIns.WowListItem();
			foreach (var name in item.Process)
			{
				var p = Process.GetProcessesByName(name).FirstOrDefault();
				if (p != null)
					return p;
			}
			return null;
		}


		public static bool ActivateWindow()
		{
			var process = GetProcess();
			if (process == null)
				return false;
			NativeMethods.AppActivate(process.Id);
			return true;
		}

		#endregion

		System.Timers.Timer _Timer;
		object serverLock = new object();

		public int CopyInterval
		{
			get { return _Interval; }
			set
			{
				var isRunning = IsRunning;
				_Timer.Stop();
				_Interval = value;
				if (isRunning)
					Start();
				OnPropertyChanged();
			}
		}

		int _Interval = 200;

		public bool IsRunning { get { return _IsRunning; } }
		bool _IsRunning;

		public override void Start()
		{
			lock (serverLock)
			{
				if (IsDisposing)
					return;
				if (_Timer != null)
				{
					// Server is already running;
					return;
				}
				_Timer = new System.Timers.Timer();
				_Timer.Interval = CopyInterval;
				_Timer.AutoReset = true;
				_Timer.Elapsed += _Timer_Elapsed;
				_Timer.Start();
				_IsRunning = true;
			}
		}

		public override void Stop()
		{
			lock (serverLock)
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

	}
}
