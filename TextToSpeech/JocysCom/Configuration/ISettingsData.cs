using JocysCom.ClassLibrary.ComponentModel;
using System.IO;

namespace JocysCom.ClassLibrary.Configuration
{
	public interface ISettingsData
	{
		bool ResetToDefault();
		void Save();
		void SaveAs(string fileName);
		void Load();
		void LoadFrom(string fileName);
		void Remove(params object[] items);

		FileInfo XmlFile { get; }

	}
}
