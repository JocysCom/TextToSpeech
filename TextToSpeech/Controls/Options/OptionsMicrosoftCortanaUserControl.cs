using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class OptionsMicrosoftCortanaUserControl : UserControl
	{
		public OptionsMicrosoftCortanaUserControl()
		{
			InitializeComponent();
		}

		// Mobile (Cortana) voices are installed under HKEY_LOCAL_MACHINE:
		const string mTokens32 = @"SOFTWARE\Microsoft\Speech_OneCore\Voices\Tokens";
		//const string mTokens64 = @"SOFTWARE\WOW6432Node\Microsoft\Speech_OneCore\Voices\Tokens";
		// TTS API voices are installed under HKEY_LOCAL_MACHINE:
		const string aTokens32 = @"SOFTWARE\Microsoft\Speech\Voices\Tokens";
		//const string aTokens64 = @"SOFTWARE\WOW6432Node\Microsoft\SPEECH\Voices\Tokens";

		private void CortanaForm_Load(object sender, EventArgs e)
		{
			var lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
			var tokens = lm.OpenSubKey(mTokens32);
			if (tokens == null)
				return;
			var list = new List<KeyValuePair<string, string>>();
			foreach (var name in tokens.GetSubKeyNames())
			{
				var token = tokens.OpenSubKey(name);
				var value = (string)token.GetValue(null);
				var item = new KeyValuePair<string, string>(name, value);

				var voicePath = string.Format("{0}", token.GetValue("VoicePath"));
				if (string.IsNullOrEmpty(voicePath))
					continue;
				var path = Environment.ExpandEnvironmentVariables(voicePath) + ".APM";
				if (!System.IO.File.Exists(path))
					continue;
				list.Add(item);
				token.Dispose();
			}
			tokens.Dispose();
			lm.Dispose();
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

		private void MobileVoiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtons();
			LoadSelected();
		}

		void LoadSelected()
		{
			var item = (KeyValuePair<string, string>)MobileVoiceComboBox.SelectedItem;
			if (string.IsNullOrEmpty(item.Key))
				return;
			var sourceKey = string.Format("{0}\\{1}", mTokens32, item.Key);
			var lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
			var key = lm.OpenSubKey(sourceKey);
			if (key == null)
			{
				ClearDetails();
				return;
			}
			// Get main details.
			//DefaultTextBox.Text = string.Format("{0}", key.GetValue(null));
			ClsidTextBox.Text = string.Format("{0}", key.GetValue("CLSID"));
			LangDataPathTextBox.Text = string.Format("{0}", key.GetValue("LangDataPath"));
			VoicePathTextBox.Text = string.Format("{0}", key.GetValue("VoicePath"));
			var att = key.OpenSubKey("Attributes");
			if (att != null)
			{
				// Get attribute details.
				AgeAttributeTextBox.Text = string.Format("{0}", att.GetValue("Age"));
				GenderAttributeTextBox.Text = string.Format("{0}", att.GetValue("Gender"));
				LanguageAttributeTextBox.Text = string.Format("{0}", att.GetValue("Language"));
				SharedPronunciationAttributeTextBox.Text = string.Format("{0}", att.GetValue("SharedPronunciation"));
				VendorAttributeTextBox.Text = string.Format("{0}", att.GetValue("Vendor"));
				NameAttributeTextBox.Text = string.Format("{0}", att.GetValue("Name"));
				DataVersionAttributeTextBox.Text = string.Format("{0}", att.GetValue("DataVersion"));
				VersionAttributeTextBox.Text = string.Format("{0}", att.GetValue("Version"));
				att.Dispose();
			}
			if (!string.IsNullOrEmpty(LanguageAttributeTextBox.Text))
			{
				LanguageIdTextBox.Text = LanguageAttributeTextBox.Text;
				NameTextBox.Text = string.Format("{0}", key.GetValue(LanguageAttributeTextBox.Text));
			}
			key.Dispose();
			lm.Dispose();
		}

		void ClearDetails()
		{
			// Clear Main.
			//DefaultTextBox.Clear();
			LanguageIdTextBox.Clear();
			ClsidTextBox.Clear();
			NameTextBox.Clear();
			LangDataPathTextBox.Clear();
			VoicePathTextBox.Clear();
			// Clear Attributes.
			AgeAttributeTextBox.Clear();
			GenderAttributeTextBox.Clear();
			LanguageIdTextBox.Clear();
			SharedPronunciationAttributeTextBox.Clear();
			VendorAttributeTextBox.Clear();
			NameAttributeTextBox.Clear();
			DataVersionAttributeTextBox.Clear();
			VersionAttributeTextBox.Clear();
		}

		void UpdateButtons()
		{
			var item = (KeyValuePair<string, string>)MobileVoiceComboBox.SelectedItem;
			bool targetExist = false;
			if (!string.IsNullOrEmpty(item.Key))
			{
				var lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
				var tokens = lm.OpenSubKey(aTokens32);
				var token = tokens.OpenSubKey(item.Key);
				targetExist = token != null;
				tokens.Dispose();
				lm.Dispose();
			}
			ImportButton.Enabled = !targetExist;
			RemoveButton.Enabled = targetExist;
		}

		private void ImportButton_Click(object sender, EventArgs e)
		{
			var item = (KeyValuePair<string, string>)MobileVoiceComboBox.SelectedItem;
			if (string.IsNullOrEmpty(item.Key))
				return;
			// Confirm Import.
			var form = new MessageBoxForm();
			form.StartPosition = FormStartPosition.CenterParent;
			var message = string.Format("Are you sure you want to import TTS {0} voice?", item.Value);
			var result = form.ShowForm(message, "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
			if (result != DialogResult.Yes)
				return;
			// Do import.
			var sourceKey = string.Format("{0}\\{1}", mTokens32, item.Key);
			var targetKey = string.Format("{0}\\{1}", aTokens32, item.Key);
			var lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
			RegistryHelper.Copy(lm, sourceKey, targetKey, true);
			lm.Dispose();
			if (Environment.Is64BitOperatingSystem)
			{
				lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				sourceKey = string.Format("{0}\\{1}", mTokens32, item.Key);
				targetKey = string.Format("{0}\\{1}", aTokens32, item.Key);
				RegistryHelper.Copy(lm, sourceKey, targetKey, true);
				lm.Dispose();
			}
			UpdateButtons();
		}

		private void RemoveButton_Click(object sender, EventArgs e)
		{
			var item = (KeyValuePair<string, string>)MobileVoiceComboBox.SelectedItem;
			if (string.IsNullOrEmpty(item.Key))
				return;
			// Confirm Removal.
			var form = new MessageBoxForm();
			form.StartPosition = FormStartPosition.CenterParent;
			var message = string.Format("Are you sure you want to remove TTS {0} voice?", item.Value);
			var result = form.ShowForm(message, "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
			if (result != DialogResult.Yes)
				return;
			// Do Removal.
			var targetKey = string.Format("{0}\\{1}", aTokens32, item.Key);
			var lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
			lm.DeleteSubKeyTree(targetKey);
			lm.Dispose();
			if (Environment.Is64BitOperatingSystem)
			{
				lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				targetKey = string.Format("{0}\\{1}", aTokens32, item.Key);
				lm.DeleteSubKeyTree(targetKey);
				lm.Dispose();
			}
			UpdateButtons();
		}
	}
}
