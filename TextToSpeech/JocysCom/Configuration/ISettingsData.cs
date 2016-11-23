using JocysCom.ClassLibrary.ComponentModel;
using System.IO;

namespace JocysCom.ClassLibrary.Configuration
{
	public interface ISettingsData
	{
		bool ResetToDefault();
		void Save();
		void Load();
		void Remove(params object[] items);

		FileInfo XmlFile { get; }

	}
}
