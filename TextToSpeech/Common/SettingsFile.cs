using System;
using System.IO;
using System.Xml.Serialization;
using JocysCom.ClassLibrary.ComponentModel;
using JocysCom.TextToSpeech.Monitor.Capturing;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.ClassLibrary.Configuration;

namespace JocysCom.TextToSpeech.Monitor
{
	[Serializable]
	public class SettingsFile
	{

		[NonSerialized]
		SortableBindingList<message> _Defaults;

		[NonSerialized]
		SortableBindingList<sound> _Sounds;

		public SettingsFile()
		{
			_Defaults = new SortableBindingList<message>();
			_Sounds = new SortableBindingList<sound>();
			FolderPath = MainHelper.AppDataPath;
			try
			{
				if (!Directory.Exists(FolderPath))
					Directory.CreateDirectory(FolderPath);
			}
			catch (Exception) { }
		}

		static SettingsFile _current;
		public static SettingsFile Current
		{
			get { return _current = _current ?? new SettingsFile(); }
		}

		public SortableBindingList<message> Defaults { get { return _Defaults; } }
		public SortableBindingList<sound> Sounds { get { return _Sounds; } }

		[XmlIgnore]
		public string FolderPath;

		[XmlIgnore]
		public string FileName = "Monitor.Settings.xml";

		object saveReadFileLock = new object();

		public void Save()
		{
			lock (saveReadFileLock)
			{
				var fullName = System.IO.Path.Combine(FolderPath, FileName);
				Serializer.SerializeToXmlFile(this, fullName, System.Text.Encoding.UTF8, true);
			}
		}

		SettingsFile GetDefault()
		{
			SettingsFile data = null;
			bool compressed = false;
			var resource = MainHelper.GetResource(FileName + ".gz");
			// If internal preset was found.
			if (resource != null) compressed = true;
			// Try to get uncompressed resource.
			else resource = MainHelper.GetResource(FileName);
			// If resource was found.
			if (resource != null)
			{
				var sr = new StreamReader(resource);
				var bytes = default(byte[]);
				using (var memstream = new MemoryStream())
				{
					sr.BaseStream.CopyTo(memstream);
					bytes = memstream.ToArray();
				}
				if (compressed) bytes = SettingsHelper.Decompress(bytes);
				data = Serializer.DeserializeFromXmlBytes<SettingsFile>(bytes);
			}
			return data;
		}

		public void ResetSoundsToDefault()
		{
			var defaultData = GetDefault();
			var sounds = defaultData.Sounds;
			Sounds.Clear();
			if (sounds != null) for (int i = 0; i < sounds.Count; i++) Sounds.Add(sounds[i]);
		}

		public void Load()
		{
			// If configuration file exists then...
			var fullName = System.IO.Path.Combine(FolderPath, FileName);
			SettingsFile data = null;
			var defaultData = GetDefault();
			SortableBindingList<message> defaults;
			SortableBindingList<sound> sounds;
			if (System.IO.File.Exists(fullName))
			{
				// Deserialize and load data.
				lock (saveReadFileLock)
				{
					data = Serializer.DeserializeFromXmlFile<SettingsFile>(fullName);
				}
			}
			defaults = data != null && data.Defaults != null && data.Defaults.Count > 0 ? data.Defaults : defaultData.Defaults;
			sounds = data != null && data.Sounds != null && data.Sounds.Count > 0 ? data.Sounds : defaultData.Sounds;
			Defaults.Clear();
			if (defaults != null) for (int i = 0; i < defaults.Count; i++) Defaults.Add(defaults[i]);
			Sounds.Clear();
			if (sounds != null) for (int i = 0; i < sounds.Count; i++) Sounds.Add(sounds[i]);
		}

	}
}
