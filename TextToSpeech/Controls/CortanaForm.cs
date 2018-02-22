using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
    public partial class CortanaForm : Form
    {
        public CortanaForm()
        {
            InitializeComponent();
        }

        private void CortanaForm_Load(object sender, EventArgs e)
        {
            // Mobile (Cortana) voices are installed here:
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech_OneCore\Voices\Tokens
            // HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Speech_OneCore\Voices\Tokens
            // TTS API voices are installed here:
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens
            // HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\SPEECH\Voices\Tokens
            var tokens = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Speech_OneCore\Voices\Tokens");
            if (tokens == null)
                return;
            var list = new List<KeyValuePair<string, string>>();
            foreach (var name in tokens.GetSubKeyNames())
            {
                var token = tokens.OpenSubKey(name);
                var value = (string)token.GetValue(null);
                var item = new KeyValuePair<string, string>(name, value);
                list.Add(item);
                token.Dispose();
            }
            tokens.Dispose();
            MobileVoiceComboBox.DataSource = list;
            MobileVoiceComboBox.DisplayMember = "Value";
            MobileVoiceComboBox.ValueMember = "Key";
        }

        static RegistryKey UpsertKey(RegistryKey key, string name)
        {
            var subKey = key.OpenSubKey(name);
            // If key was not found then...
            if (subKey == null)
            {
                // Create new key for URL protocol.
                subKey = key.CreateSubKey(name);
            }
            return subKey;
        }

        static void UpsertValue(RegistryKey key, string name, string value)
        {
            if ((key.GetValue(name) as string) != value) key.SetValue(name, value);
        }


    }
}
