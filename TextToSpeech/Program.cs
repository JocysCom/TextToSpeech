using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.WoW.TextToSpeech
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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
            Application.Run(new MainForm());
        }
    }
}
