using JocysCom.ClassLibrary.ComponentModel;

namespace JocysCom.ClassLibrary.Configuration
{
	public interface ISettingsData
	{
		bool ResetToDefault();
		void Save();
		void Load();

	}
}
