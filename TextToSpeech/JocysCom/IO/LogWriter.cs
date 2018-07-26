using System;
using System.IO;
using System.Linq;

namespace JocysCom.ClassLibrary.IO
{

	// Create a log file writer, so you can see the flow easily.
	// It can be printed. Makes it easier to figure out complex program flow.
	// The log StreamWriter uses a buffer. So it will only work right if you close
	// the server console properly at the end of the test.
	public class LogWriter : IDisposable
	{

		object streamWriterLock = new object();
		object lockerCurrent = new object();
		string saveFile;
		string logFileNamePattern;
		StreamWriter tw;
		bool _LogAutoFlush;
		public bool IsEnabled;
		public bool LogAutoFlush
		{
			get { return _LogAutoFlush; }
			set
			{
				lock (streamWriterLock)
				{
					if (tw != null && tw.AutoFlush != LogAutoFlush) tw.AutoFlush = value;
					_LogAutoFlush = value;
				}
			}
		}

		public LogWriter(string pattern = "{0:yyyyMMdd_HHmmss}.txt", bool isEnabled = true)
		{
			logFileNamePattern = pattern;
			isEnabled = true;
		}

		public void WriteLine(string format, params object[] args)
		{
			Write(format + "\r\n", args);
		}

		///// <summary>
		///// Wipe old files.
		///// </summary>
		///// <param name="maxLogFiles">Number of files to keep.</param>
		//int WipeOldLogFiles(int maxLogFiles, long maxLogBytes)
		//{
		//	var fi = new FileInfo(saveFile);
		//	var di = fi.Directory;
		//	var prefix = saveFile.Split('\\').Last();
		//	// Get file list ordered by newest on the top.
		//	var files = di.GetFiles(prefix + "*" + fi.Extension).OrderByDescending(x => x.CreationTime).ToArray();
		//	var deleted = 0;
		//	long totalSize = 0;
		//	for (int i = 0; i < files.Length; i++)
		//	{
		//		totalSize += files[i].Length;
		//		// If maximum reached then...
		//		if (i + 1 >= maxLogFiles || totalSize >= maxLogBytes)
		//		{
		//			files[i].Delete();
		//			deleted++;
		//		}
		//	}
		//	return deleted;
		//}

		public void Write(string format, params object[] args)
		{
			var message = args.Length > 0 ? string.Format(format, args) : format;
			lock (streamWriterLock)
			{
				if (saveFile == null)
				{
					// Create a new log file with every application.
					var name = string.Format(logFileNamePattern, DateTime.Now);
					var fi = new System.IO.FileInfo(name);
					try
					{
						if (!fi.Directory.Exists) fi.Directory.Create();
					}
					catch (Exception ex)
					{
						ex.Data.Add("FullPath", fi.FullName);
						throw;
					}
					saveFile = fi.FullName;
				}
				if (IsDisposing) return;
				if (tw == null) tw = new StreamWriter(saveFile);
				if (tw.AutoFlush != LogAutoFlush) tw.AutoFlush = LogAutoFlush;
				tw.Write(message);
			}
		}

		public void Flush()
		{
			lock (streamWriterLock)
			{
				if (tw != null) tw.Flush();
			}
		}

		#region IDisposable

		// Dispose() calls Dispose(true)
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		//// NOTE: Leave out the finalizer altogether if this class doesn't 
		//// own unmanaged resources itself, but leave the other methods
		//// exactly as they are. 
		//~LogFileWriter()
		//{
		//    // Finalizer calls Dispose(false)
		//    Dispose(false);
		//}

		bool IsDisposing;

		// The bulk of the clean-up code is implemented in Dispose(bool)
		void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (streamWriterLock)
				{
					// Don't dispose twice.
					if (IsDisposing) return;
					IsDisposing = true;
					if (tw != null)
					{
						tw.Close();
						tw.Dispose();
						tw = null;
					}
				}
			}
		}

		#endregion

	}
}


