using JocysCom.ClassLibrary.Processes;
using JocysCom.ClassLibrary.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class ClipboardMonitor : MonitorBase
	{

		public ClipboardMonitor()
		{
		}

		private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				var success = ActivateWindow();
				if (!success)
					return;
				var process = GetProcess();
				//KeyboardHelper.SendKey("^(A)", process.ProcessName);
				//KeyboardHelper.SendKey("^(C)", process.ProcessName);
				PressKeys();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally {
				if (IsRunning)
					_Timer.Start();
			}
		}

		#region Process

		static Process GetProcess()
		{
			var item = new PlugIns.WowListItem();
			item.Name = "Notepad";
			item.Process = new string[] { "notepad" };
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
				if (isRunning)
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
				_Timer.AutoReset = false;
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

		[DllImport("user32.dll", SetLastError = true)]
		static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
		public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
		public const int VK_LCONTROL = 0xA2; //Left Control key code
		public const int A = 0x41; //A key code
		public const int C = 0x43; //C key code

		public static void PressKeys()
		{
			//// Hold Control down and press A
			//keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
			//keybd_event(A, 0, KEYEVENTF_EXTENDEDKEY, 0);
			//keybd_event(A, 0, KEYEVENTF_KEYUP, 0);
			//keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);

			// Hold Control down and press C
			keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
			keybd_event(C, 0, KEYEVENTF_EXTENDEDKEY, 0);
			keybd_event(C, 0, KEYEVENTF_KEYUP, 0);
			keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
		}

	}
}
