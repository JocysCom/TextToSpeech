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
		/// <summary>Acronym Settings.</summary>
		public SettingsData<Acronym> Acronyms = new SettingsData<Acronym>("Monitor.Acronyms.xml");

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
