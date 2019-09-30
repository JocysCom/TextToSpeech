using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JocysCom.TextToSpeech.Monitor.Capturing.Monitors
{
	public class MonitorBase : IMonitor, IDisposable, INotifyPropertyChanged
	{
		public bool IsRunning { get { return _IsRunning; } }
		internal bool _IsRunning;

		public long MessagesReceived { get { return _MessagesReceived; } set { _MessagesReceived = value; OnPropertyChanged(); } }
		long _MessagesReceived;

		public event EventHandler<EventArgs<string>> MessageReceived;
		public event EventHandler<MonitorEventArgs> StatusChanged;
		public virtual void Stop() { }
		public virtual void Start() { }

		internal object monitorLock = new object();

		// Used from derived classes to raise ProgressStarted.
		protected void OnMessageReceived(string text)
		{
			MessagesReceived++;
			var handler = MessageReceived;
			if (handler == null)
				return;
			ControlsHelper.Invoke(() => { handler(this, new ClassLibrary.EventArgs<string>(text)); });
		}

		protected void OnStatusChanged(string error, string filter = null, string packets = null, string state = null)
		{
			var handler = StatusChanged;
			if (handler == null)
				return;
			var e = new MonitorEventArgs()
			{
				Error = error,
				Filter = filter,
				Packets = packets,
				State = state,
			};
			ControlsHelper.Invoke(() => { handler(this, e); });
		}

		public string Error { get; set; }
		public string Filter { get; set; }
		public string State { get; set; }
		public string Packets { get; set; }

		#region IDisposable

		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected bool IsDisposing;

		public Exception LastException { get; set; }

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Don't dispose twice.
				if (IsDisposing)
					return;
				IsDisposing = true;
				MessageReceived = null;
				Stop();
			}
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;
			ControlsHelper.Invoke(() => { handler(this, new PropertyChangedEventArgs(propertyName)); });
		}

		#endregion


	}
}
