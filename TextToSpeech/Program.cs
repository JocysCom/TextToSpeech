using JocysCom.ClassLibrary.Controls;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	static partial class Program
	{
		public static MainForm TopForm;

		static byte[] HexToBytes(string hex)
		{
			int offset = 0;
			if ((hex.Length % 2) != 0) return new byte[0];
			byte[] bytes = new byte[(hex.Length - offset) / 2];
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = byte.Parse(hex.Substring(offset, 2), System.Globalization.NumberStyles.HexNumber);
				offset += 2;
			}
			return bytes;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(params string[] args)
		{
			// ------------------------------------------------
			// Administrator commands.
			// ------------------------------------------------
			var executed = ProcessAdminCommands(false, args);
			// If valid command was executed then...
			if (executed)
				return;
			// ------------------------------------------------
			//var h = "6000000000aa06402a020c7fc4218f00cd3b5a2cb0ecc03f2a04e80050192273028cfafffefbf006";
			//var bytes = HexToBytes(h);
			//Network.Ip6Header header;
			//var p = Network.Ip6Header.TryParse(bytes, out header);

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
			Application.ThreadException += Application_ThreadException;
			Application.ApplicationExit += Application_ApplicationExit;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			// Check if settings file is valid.
			if (!CheckSettings()) return;
			try
			{
				InitMonitors();
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				var message = "";
				MainHelper.AddExceptionMessage(ex, ref message);
				if (ex.InnerException != null) MainHelper.AddExceptionMessage(ex.InnerException, ref message);
				var box = new JocysCom.ClassLibrary.Controls.MessageBoxForm();
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

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{


		}

		public static bool CheckSettings()
		{
			try
			{
				// Load default settings.
				var isSettignsValid = !string.IsNullOrEmpty(SettingsManager.Options.ProgramComboBoxText);
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
			if (string.IsNullOrEmpty(SettingsManager.Options.ProgramComboBoxText))
			{
				SettingsManager.Current.OptionsData.ResetToDefault();
			}
			// Check if settings are writable.
			var path = SettingsFile.Current.FolderPath;
			var rights = FileSystemRights.Write | FileSystemRights.Modify;
			var hasRights = JocysCom.ClassLibrary.Security.PermissionHelper.HasRights(path, rights);
			if (!hasRights)
			{
				var caption = string.Format("Folder Access Denied - {0}", path);
				var text = "Old settings were written with administrator permissions.\r\n";
				text += "You'll need to provide administrator permissions to fix access and save settings.";
				var form = new MessageBoxForm();
				form.StartPosition = FormStartPosition.CenterParent;
				var result2 = form.ShowForm(text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
				form.Dispose();
				if (result2 == DialogResult.OK)
					RunElevated(AdminCommand.FixProgramSettingsPermissions);
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

		/// <summary>
		/// Load referenced libraries which were included as Embedded Resources into current assembly.
		/// </summary>
		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
		{
			string dllName = e.Name.Contains(",")
				? e.Name.Substring(0, e.Name.IndexOf(','))
				: e.Name.Replace(".dll", "");
			dllName += ".dll";
			var assembly = Assembly.GetExecutingAssembly();
			var names = assembly.GetManifestResourceNames();
			var resourceName = names.FirstOrDefault(x => x.EndsWith(dllName));
			if (string.IsNullOrEmpty(resourceName))
			{
				return null;
			}
			var sr = assembly.GetManifestResourceStream(resourceName);
			byte[] bytes = new byte[sr.Length];
			sr.Read(bytes, 0, bytes.Length);
			return Assembly.Load(bytes);
		}

	}
}
