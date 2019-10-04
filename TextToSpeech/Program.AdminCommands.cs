using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

namespace JocysCom.TextToSpeech.Monitor
{
	public enum AdminCommand
	{
		FixProgramSettingsPermissions,
	}

	/// <summary>
	/// Some actions require for application to run as Administrator.
	/// In most cases application will run with permissions of normal user.
	/// In order to get around this issue, application will run second copy of itself with 
	/// Administrative permissions.
	/// </summary>
	static partial class Program
	{

		/// <summary>
		/// Returns true if command was executed locally.
		/// </summary>
		public static bool RunElevated(AdminCommand command, string param = null)
		{
			// If program is running as Administrator already.
			var argument = command.ToString();
			if (param != null)
			{
				argument = string.Format("{0}=\"{1}\"", command, param);
			}
			if (JocysCom.ClassLibrary.Security.PermissionHelper.IsElevated)
			{
				// Run command directly.
				var args = new string[] { argument };
				ProcessAdminCommands(true, args);
				return true;
			}
			else
			{
				// Run copy of application as Administrator.
				RunElevated(
					System.Windows.Forms.Application.ExecutablePath,
					argument,
					System.Diagnostics.ProcessWindowStyle.Hidden
				);
				return false;
			}
		}

		static bool ProcessAdminCommands(bool direct, string[] args)
		{
			// Requires System.Configuration.Installl reference.
			var ic = new System.Configuration.Install.InstallContext(null, args);
			if (ic.Parameters.ContainsKey(AdminCommand.FixProgramSettingsPermissions.ToString()))
			{
				var path = SettingsFile.Current.FolderPath;
				var rights = FileSystemRights.Write | FileSystemRights.Modify;
				var users = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
				JocysCom.ClassLibrary.Security.PermissionHelper.SetRights(path, rights, users);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Start program in elevated mode.
		/// </summary>
		/// <param name="fileName"></param>
		public static int RunElevated(string fileName, string arguments, ProcessWindowStyle style, bool useFileWorkingFolder = false)
		{
			int exitCode = -1;
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("Executable file name must be specified");
			using (Process process = CreateElevatedProcess(fileName, arguments, useFileWorkingFolder))
			{
				try
				{
					process.StartInfo.WindowStyle = style;
					process.Start();
				}
				catch (Win32Exception)
				{
					// The user refused to allow privileges elevation
					// or other error happened. Do nothing and return...
					return exitCode;
				}
				process.WaitForExit();
				exitCode = process.ExitCode;
			}
			return exitCode;
		}

		public static Process CreateElevatedProcess(string fileName, string arguments = null, bool useFileWorkingFolder = false)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.UseShellExecute = true;
			psi.WorkingDirectory = useFileWorkingFolder
				? new System.IO.FileInfo(fileName).DirectoryName
				: Environment.CurrentDirectory;
			psi.FileName = fileName;
			if (arguments != null) psi.Arguments = arguments;
			psi.CreateNoWindow = true;
			psi.Verb = "runas";
			var process = new Process();
			// Must enable Exited event for both sync and async scenarios.
			process.EnableRaisingEvents = true;
			process.StartInfo = psi;
			return process;
		}


	}
}
