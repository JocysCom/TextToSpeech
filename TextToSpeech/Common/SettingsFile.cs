using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using JocysCom.ClassLibrary.ComponentModel;
using JocysCom.TextToSpeech.Monitor.Network;
using JocysCom.ClassLibrary.Runtime;

namespace JocysCom.TextToSpeech.Monitor
{
	[Serializable]
	public class SettingsFile
	{

		[NonSerialized]
		SortableBindingList<voice> _Overrides;

		public SettingsFile()
		{
			_Overrides = new SortableBindingList<voice>();
			_Overrides.AddingNew += _Overrides_AddingNew;
			_Overrides.ListChanged += _Overrides_ListChanged;
			FolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\JocysCom TextToSpeech Monitor";
			try { if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath); }
			catch (Exception) { }
		}

		void _Overrides_ListChanged(object sender, ListChangedEventArgs e)
		{

		}

		void _Overrides_AddingNew(object sender, AddingNewEventArgs e)
		{

		}

		static SettingsFile _current;
		public static SettingsFile Current
		{
			get { return _current = _current ?? new SettingsFile(); }
		}

		public SortableBindingList<voice> Overrides { get { return _Overrides; } }

		public string FolderPath;
		public string FileName = "Settings.xml";
		object saveReadFileLock = new object();

		public void Save()
		{
			lock (saveReadFileLock)
			{
				var fullName = System.IO.Path.Combine(FolderPath, FileName);
				Serializer.SerializeToXmlFile(this, fullName, System.Text.Encoding.UTF8);
			}
		}

		public void Load()
		{
			// If configuration file exists then...
			var fullName = System.IO.Path.Combine(FolderPath, FileName);
			if (System.IO.File.Exists(fullName))
			{
				SettingsFile data;
				// Deserialize and load data.
				lock (saveReadFileLock)
				{
					data = Serializer.DeserializeFromXmlFile<SettingsFile>(fullName);
				}
				if (data == null) return;
				Overrides.Clear();
				if (data.Overrides != null) for (int i = 0; i < data.Overrides.Count; i++) Overrides.Add(data.Overrides[i]);
			}
			else
			{
				var resource = MainHelper.GetResource(FileName + ".gz");
				// If internal preset was found.
				if (resource != null)
				{
					var sr = new StreamReader(resource);
					var compressedBytes = default(byte[]);
					using (var memstream = new MemoryStream())
					{
						sr.BaseStream.CopyTo(memstream);
						compressedBytes = memstream.ToArray();
					}
					var bytes = MainHelper.Decompress(compressedBytes);
					var xml = System.Text.Encoding.UTF8.GetString(bytes);
					var items = Serializer.DeserializeFromXmlString<List<voice>>(xml);
					Overrides.Clear();
					for (int i = 0; i < items.Count; i++) Overrides.Add(items[i]);
				}
			}
		}

	}
}
