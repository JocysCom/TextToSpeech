using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Update working directory.
                var fi = new System.IO.FileInfo(Application.ExecutablePath);
                System.IO.Directory.SetCurrentDirectory(fi.Directory.FullName);
                if (!RuntimePolicyHelper.LegacyV2RuntimeEnabledSuccessfully)
                {
                    // Failed to enable useLegacyV2RuntimeActivationPolicy at runtime.
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
              // if settings doesn't exists then...
                if (string.IsNullOrEmpty(Properties.Settings.Default.PitchMaxComboBoxText))
                {
                    Properties.Settings.Default.PitchMaxComboBoxText = "0";
                    Properties.Settings.Default.PitchMinComboBoxText = "0";
                    Properties.Settings.Default.RateMaxComboBoxText = "1";
                    Properties.Settings.Default.RateMinComboBoxText = "1";
                    Properties.Settings.Default.GenderComboBoxText = "Male";
                    Properties.Settings.Default.MonitorClipboardComboBoxText = "Disabled";
                    Properties.Settings.Default.PortNumericUpDownValue = 3724;
                    Properties.Settings.Default.Save();
                }
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
                    box.MainLinkLabel.Text = "http://www.microsoft.com/en-us/download/details.aspx?id=35";
                    box.MainLinkLabel.Visible = true;
                }
                var result = box.ShowForm(message, "Exception!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel) Application.Exit();
            }
        }
    }
}
