using JocysCom.ClassLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocysCom.TextToSpeech.Monitor
{
	public class SettingsManager
	{

		public SettingsManager()
		{
			Acronyms = new SettingsData<Acronym>("Monitor.Acronyms.xml");
			Acronyms.ApplyOrder = Acronyms_ApplyOrder;
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

		public string ReplaceAcronyms(string source)
		{
			var list = Acronyms.Items.Where(x => x.RegexValue != null).OrderByDescending(x => x.Group).ThenByDescending(x => x.Key);
			foreach (var item in list)
			{
				source = item.RegexValue.Replace(source, item.Value);
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

		public void Save(bool updateGameDatabase = false)
		{
			lock (saveReadFileLock)
			{
				Acronyms.Save();
			}
		}
	}
}
