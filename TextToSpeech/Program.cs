using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	static class Program
	{
		public static MainForm TopForm;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Update working directory.
			var fi = new System.IO.FileInfo(Application.ExecutablePath);
			System.IO.Directory.SetCurrentDirectory(fi.Directory.FullName);
			// Load embedded assemblies.
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			if (!RuntimePolicyHelper.LegacyV2RuntimeEnabledSuccessfully)
			{
				// Failed to enable useLegacyV2RuntimeActivationPolicy at runtime.
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			System.Windows.Forms.Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			// Check if settings file is valid.
			if (!CheckSettings()) return;
			try
			{
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				var message = "";
				MainHelper.AddExceptionMessage(ex, ref message);
				if (ex.InnerException != null) MainHelper.AddExceptionMessage(ex.InnerException, ref message);
				var box = new Controls.MessageBoxForm();
				if (message.Contains("Could not load file or assembly 'Microsoft.DirectX"))
				{
					message += "===============================================================\r\n";
					message += "You can click the link below to download Microsoft DirectX.";
					box.MainLinkLabel.Text = "http://www.microsoft.com/en-gb/download/details.aspx?id=8109";
					box.MainLinkLabel.Visible = true;
				}
				var result = box.ShowForm(message, "Exception!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				if (result == DialogResult.Cancel) Application.Exit();
			}
		}

		public static bool CheckSettings()
		{
			try
			{
				// Load default settings.
				Properties.Settings.Default.Reload();
				var isSettignsValid = !string.IsNullOrEmpty(Properties.Settings.Default.PitchMaxComboBoxText);
			}
			catch (ConfigurationErrorsException)
			{
				var result = MessageBox.Show("User settings file has become corrupted.\r\nProgram must reset your user settings in order to continue.\r\n\r\n   Click [Yes] to reset your user settings and continue.\r\n   Click [No] if you wish to attempt manual repair.\r\n", "Corrupt user settings of " + Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
				if (result == DialogResult.Yes)
				{
					var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
					if (System.IO.File.Exists(configFile))
					{
						System.IO.File.Copy(configFile, configFile + ".bak", true);
						System.IO.File.Delete(configFile);
					}
					Application.Restart();
					return false;
				}
				else
				{
					// Avoid the inevitable crash by killing application first.
					//Process.GetCurrentProcess().Kill();
				}
			}
			if (string.IsNullOrEmpty(Properties.Settings.Default.PitchMaxComboBoxText))
			{
				Properties.Settings.Default.DefaultIntroSoundComboBox = "Radio";
				Properties.Settings.Default.PitchMaxComboBoxText = "0";
				Properties.Settings.Default.PitchMinComboBoxText = "0";
				Properties.Settings.Default.RateMaxComboBoxText = "1";
				Properties.Settings.Default.RateMinComboBoxText = "1";
				Properties.Settings.Default.GenderComboBoxText = "Male";
				Properties.Settings.Default.MonitorClipboardComboBoxText = "Disabled";
				Properties.Settings.Default.MonitorPortChecked = true;
				Properties.Settings.Default.PortNumericUpDownValue = 3724;
				Properties.Settings.Default.Save();
			}

			return true;
		}

		public static int ExceptionsCount = 3;
		public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.ToString());
		}
		public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var errorText = ((Exception)e.ExceptionObject).ToString();
			System.IO.File.WriteAllText("JocysCom.TextToSpeech.Monitor.Error.txt", errorText);
			MessageBox.Show(errorText);
		}

		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
		{
			string dllName = e.Name.Contains(",") ? e.Name.Substring(0, e.Name.IndexOf(',')) : e.Name.Replace(".dll", "");
			string path = null;
			switch (dllName)
			{
				case "SharpDX":
					path = "Resources.SharpDX.SharpDX.dll";
					break;
				case "SharpDX.DirectSound":
					path = "Resources.SharpDX.SharpDX.DirectSound.dll";
					break;
				default:
					break;
			}
			if (path == null) return null;
			var assembly = Assembly.GetExecutingAssembly();
			var sr = assembly.GetManifestResourceStream(typeof(MainForm).Namespace + "." + path);
			if (sr == null)
			{
				return null;
			}
			byte[] bytes = new byte[sr.Length];
			sr.Read(bytes, 0, bytes.Length);
			var asm = Assembly.Load(bytes);
			return asm;
		}

	}
}
