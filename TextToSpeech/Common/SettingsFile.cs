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
        SortableBindingList<message> _Overrides;

        [NonSerialized]
        SortableBindingList<sound> _Sounds;

        public SettingsFile()
        {
            _Overrides = new SortableBindingList<message>();
            _Sounds = new SortableBindingList<sound>();
            FolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\JocysCom TextToSpeech Monitor";
            try { if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath); }
            catch (Exception) { }
        }

        static SettingsFile _current;
        public static SettingsFile Current
        {
            get { return _current = _current ?? new SettingsFile(); }
        }

        public SortableBindingList<message> Overrides { get { return _Overrides; } }
        public SortableBindingList<sound> Sounds { get { return _Sounds; } }

        public string FolderPath;
        public string FileName = "Settings.Monitor.xml";
        object saveReadFileLock = new object();

        public void Save()
        {
            lock (saveReadFileLock)
            {
                var fullName = System.IO.Path.Combine(FolderPath, FileName);
                Serializer.SerializeToXmlFile(this, fullName, System.Text.Encoding.UTF8);
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
                if (compressed) bytes = MainHelper.Decompress(bytes);
                var xml = System.Text.Encoding.UTF8.GetString(bytes);
                data = Serializer.DeserializeFromXmlString<SettingsFile>(xml);
            }
            return data;
        }

        public void Load()
        {
            // If configuration file exists then...
            var fullName = System.IO.Path.Combine(FolderPath, FileName);
            SettingsFile data = null;
            var defaultData = GetDefault();
            SortableBindingList<message> overrides;
            SortableBindingList<sound> sounds;
            if (System.IO.File.Exists(fullName))
            {
                // Deserialize and load data.
                lock (saveReadFileLock)
                {
                    data = Serializer.DeserializeFromXmlFile<SettingsFile>(fullName);
                }
            }
            overrides = data != null && data.Overrides != null && data.Overrides.Count > 0 ? data.Overrides : defaultData.Overrides;
            sounds = data != null && data.Sounds != null && data.Sounds.Count > 0 ? data.Sounds : defaultData.Sounds;
            Overrides.Clear();
            if (overrides != null) for (int i = 0; i < overrides.Count; i++) Overrides.Add(overrides[i]);
            Sounds.Clear();
            if (sounds != null) for (int i = 0; i < sounds.Count; i++) Sounds.Add(sounds[i]);
        }

    }
}
