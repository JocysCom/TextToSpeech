using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	public class MonitorBase : IMonitor, IDisposable, INotifyPropertyChanged
	{
		public long MessagesReceived { get { return _MessagesReceived; } set { _MessagesReceived = value; OnPropertyChanged(); } }
		long _MessagesReceived;

		public event EventHandler<EventArgs<string>> MessageReceived;
		public virtual void Stop() { }
		public virtual void Start() { }

		// Used from derived classes to raise ProgressStarted.
		protected void OnMessageReceived(string text)
		{
			MessagesReceived++;
			var ev = MessageReceived;
			if (ev != null)
				ev(this, new ClassLibrary.EventArgs<string>(text));
		}

		#region IDisposable

		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected bool IsDisposing;

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
			if (handler != null)
				ControlsHelper.Invoke(() =>
				{
					handler(this, new PropertyChangedEventArgs(propertyName));
				});
		}

		#endregion


	}
}
