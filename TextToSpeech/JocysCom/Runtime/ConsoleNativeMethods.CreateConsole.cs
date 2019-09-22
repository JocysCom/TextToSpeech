using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace JocysCom.ClassLibrary.Runtime
{
	public partial class ConsoleNativeMethods
	{

		/// <summary>
		/// Allocates a new console for the calling process.
		/// </summary>
		/// <returns>If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// To get extended error information, call Marshal.GetLastWin32Error.
		/// </returns>
		[DllImport("kernel32.dll")]
		public static extern bool AllocConsole();

		/// <summary>
		/// Detaches the calling process from its console
		/// </summary>
		/// <returns>If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.
		/// To get extended error information, call Marshal.GetLastWin32Error.
		/// </returns>
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("kernel32.dll")]
		public static extern bool FreeConsole();

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern IntPtr CreateFile(string lpFileName
			, [MarshalAs(UnmanagedType.U4)] DesiredAccess dwDesiredAccess
			, [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode
			, uint lpSecurityAttributes
			, [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition
			, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes
			, uint hTemplateFile
		);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetStdHandle(StdHandle nStdHandle, IntPtr hHandle);

		enum StdHandle : int
		{
			Input = -10,
			Output = -11,
			Error = -12
		}

		[Flags]
		enum DesiredAccess : uint
		{
			GenericRead = 0x80000000,
			GenericWrite = 0x40000000,
			GenericExecute = 0x20000000,
			GenericAll = 0x10000000
		}

		/// <summary>
		/// Includes workaround for when Console.Out Output is showing in Output Window.
		/// </summary>
		public static bool CreateConsole()
		{
			if (!AllocConsole())
				return false;
			// https://developercommunity.visualstudio.com/content/problem/12166/console-output-is-gone-in-vs2017-works-fine-when-d.html
			// Console.OpenStandardOutput eventually calls into GetStdHandle.
			// As per MSDN documentation of GetStdHandle: http://msdn.microsoft.com/en-us/library/windows/desktop/ms683231(v=vs.85).aspx 
			// will return the redirected handle and not the allocated console:
			// "The standard handles of a process may be redirected by a call to  SetStdHandle, in which case  GetStdHandle returns the redirected handle.
			// If the standard handles have been redirected, you can specify the CONIN$ value in a call to the CreateFile function to get a handle to
			// a console's input buffer. Similarly, you can specify the CONOUT$ value to get a handle to a console's active screen buffer."
			// Get the handle to CONOUT$.    
			var stdOutHandle = CreateFile("CONOUT$",
				DesiredAccess.GenericRead | DesiredAccess.GenericWrite, FileShare.ReadWrite
				, 0, FileMode.Open, FileAttributes.Normal, 0);
			if (stdOutHandle == new IntPtr(-1))
				throw new Win32Exception(Marshal.GetLastWin32Error());
			if (!SetStdHandle(StdHandle.Output, stdOutHandle))
				throw new Win32Exception(Marshal.GetLastWin32Error());
			var standardOutput = new StreamWriter(Console.OpenStandardOutput());
			standardOutput.AutoFlush = true;
			Console.SetOut(standardOutput);
			return true;
		}

	}
}
