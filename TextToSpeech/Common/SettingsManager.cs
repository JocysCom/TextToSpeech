using JocysCom.ClassLibrary.Configuration;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.Linq;

namespace JocysCom.TextToSpeech.Monitor
{
	public class SettingsManager
	{

		public SettingsManager()
		{
			Acronyms = new SettingsData<Acronym>("Monitor.Acronyms.xml");
			Acronyms.ApplyOrder = Acronyms_ApplyOrder;
			Acronyms.Load();
			OptionsData = new SettingsData<Options>("Monitor.Options.xml");
			OptionsData.Load();
			if (OptionsData.Items.Count == 0)
			{
				var o = new Options();
				OptionsData.Items.Add(o);
				OptionsData.Items[0].InitDefaults();
			}
			OptionsData.Items[0].InitDefaults(true);
		}

		void Acronyms_ApplyOrder(SettingsData<Acronym> source)
		{
			var items = source.Items.OrderByDescending(x => x.Group).OrderBy(x => x.Key).ToArray();
			source.Items.Clear();
			foreach (var item in items)
			{
				source.Items.Add(item);
			}
		}

		/// <summary>Acronym Settings.</summary>
		public SettingsData<Acronym> Acronyms;

		/// <summary>Options</summary>
		public SettingsData<Options> OptionsData;

		public static Options Options { get { return Current.OptionsData.Items[0]; } }

		public string ReplaceAcronyms(string source)
		{
			var list = Acronyms.Items.Where(x => x.RegexValue != null && x.Enabled).OrderByDescending(x => x.Group).ThenByDescending(x => x.Key);
			foreach (var item in list)
			{
				source = item.RegexValue.Replace(source, item.Value ?? "");
			}
			return source;
		}

		#region Public Properties

		/// <summary>
		/// Gets the SettingManager singleton instance.
		/// </summary>
		public static SettingsManager Current
		{
			get { return _current = _current ?? new SettingsManager(); }
		}
		static SettingsManager _current;

		#endregion // Public Properties

		static object saveReadFileLock = new object();

		public void UpsertDefaultsRecord(message v)
		{
			// Try to find existing default record from the list.
			var dm = SettingsFile.Current.Defaults.FirstOrDefault(x => string.Equals(x.name, v.name, StringComparison.InvariantCultureIgnoreCase));
			// If there is no default message then...
			if (dm == null)
			{
				dm = new message();
				dm.name = v.name;
				SettingsFile.Current.Defaults.Add(dm);
			}
			dm.UpdateMissingAndChangedValuesFrom(v);
		}
		public void Save()
		{
			lock (saveReadFileLock)
			{
				Acronyms.Save();
				OptionsData.Save();
			}
		}
	}
}
