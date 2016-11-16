using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.Objects.DataClasses;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.ClassLibrary.ComponentModel;
using System.Reflection;
using System.Linq;
using System.IO.Compression;

namespace JocysCom.ClassLibrary.Configuration
{
	[Serializable, XmlRoot("Data")]
	public class SettingsData<T>
	{

		public SettingsData() { }

		public SettingsData(string fileName, bool userLevel = false)
		{
			Items = new SortableBindingList<T>();
			var specialFolder = userLevel
				? Environment.SpecialFolder.ApplicationData
				: Environment.SpecialFolder.CommonApplicationData;
			var file = string.IsNullOrEmpty(fileName)
				? string.Format("{0}.xml", typeof(T).Name)
				: fileName;
			var folder = string.Format("{0}\\{1}\\{2}",
				Environment.GetFolderPath(specialFolder),
				Application.CompanyName,
				Application.ProductName);
			var path = Path.Combine(folder, file);
			_XmlFile = new FileInfo(path);
		}

		[XmlIgnore]
		public FileInfo XmlFile { get { return _XmlFile; } }
		[NonSerialized]
		FileInfo _XmlFile;

		public SortableBindingList<T> Items;

		/// <summary>
		/// File Version.
		/// </summary>
		[XmlAttribute]
		public int Version { get; set; }

		object initialFileLock = new object();
		object saveReadFileLock = new object();

		public void SaveAs(string fileName)
		{
			lock (saveReadFileLock)
			{
				for (int i = 0; i < Items.Count; i++)
				{
					var o = Items[i] as EntityObject;
					if (o != null) o.EntityKey = null;
				}
				var fi = new FileInfo(fileName);
				if (!fi.Directory.Exists)
				{
					fi.Directory.Create();
				}
				Serializer.SerializeToXmlFile(this, fileName, Encoding.UTF8, true);
			}
		}

		public void Save()
		{
			SaveAs(_XmlFile.FullName);
		}


		public delegate IList<T> FilterListDelegate(IList<T> items);

		[NonSerialized, XmlIgnore]
		public FilterListDelegate FilterList;

		public void Load()
		{
			bool settingsLoaded = false;
			// If configuration file exists then...
			if (_XmlFile.Exists)
			{
				// Try to read file until success.
				while (true)
				{
					SettingsData<T> data;
					// Deserialize and load data.
					lock (saveReadFileLock)
					{
						try
						{
							data = Serializer.DeserializeFromXmlFile<SettingsData<T>>(_XmlFile.FullName);
							if (data != null)
							{
								Items.Clear();
								if (data != null)
								{
									var m = FilterList;
									var items = (m == null)
										? data.Items
										: m(data.Items);
									if (items != null)
									{
										for (int i = 0; i < items.Count; i++) Items.Add(items[i]);
									}
								}
								settingsLoaded = true;
							}
							break;
						}
						catch (Exception)
						{
							var form = new Controls.MessageBoxForm();
							var backupFile = _XmlFile.FullName + ".bak";
							form.StartPosition = FormStartPosition.CenterParent;
							var text = string.Format("{0} file has become corrupted.\r\n" +
								"Program must reset {0} file in order to continue.\r\n\r\n" +
								"   Click [Yes] to reset and continue.\r\n" +
								"   Click [No] if you wish to attempt manual repair.\r\n\r\n" +
								" File: {1}", _XmlFile.Name, _XmlFile.FullName);
							var caption = string.Format("Corrupt {0} of {1}", _XmlFile.Name, Application.ProductName);
							var result = form.ShowForm(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
							if (result == DialogResult.Yes)
							{
								if (File.Exists(backupFile))
								{
									File.Copy(backupFile, _XmlFile.FullName, true);
									_XmlFile.Refresh();
								}
								else
								{
									File.Delete(_XmlFile.FullName);
									break;
								}
							}
							else
							{
								// Avoid the inevitable crash by killing application first.
								Process.GetCurrentProcess().Kill();
								return;
							}
						}
					}
				}
			}
			// If settings failed to load then...
			if (!settingsLoaded)
			{
				ResetToDefault();
			}
			if (!settingsLoaded)
			{
				Save();
			}
		}

		public bool ResetToDefault()
		{
			var success = false;
			var assembly = Assembly.GetExecutingAssembly();
			var names = assembly.GetManifestResourceNames();
			// Get compressed resource name.
			var name = names.FirstOrDefault(x => x.EndsWith(_XmlFile.Name + ".gz"));
			if (string.IsNullOrEmpty(name))
			{
				// Get uncompressed resource name.
				name = names.FirstOrDefault(x => x.EndsWith(_XmlFile.Name));
			}
			// If internal preset was found.
			if (!string.IsNullOrEmpty(name))
			{
				var resource = assembly.GetManifestResourceStream(name);
				var sr = new StreamReader(resource);
				byte[] bytes;
				using (var memstream = new MemoryStream())
				{
					sr.BaseStream.CopyTo(memstream);
					bytes = memstream.ToArray();
				}
				if (name.EndsWith(".gz"))
				{

					bytes = Decompress(bytes);
				}
				var xml = Encoding.UTF8.GetString(bytes);
				var data = Serializer.DeserializeFromXmlString<SettingsData<T>>(xml);
				Items.Clear();
				for (int i = 0; i < data.Items.Count; i++) Items.Add(data.Items[i]);
				success = true;
			}
			return success;
		}

		static byte[] Decompress(byte[] bytes)
		{
			int numRead;
			var srcStream = new MemoryStream(bytes);
			var dstStream = new MemoryStream();
			srcStream.Position = 0;
			var stream = new GZipStream(srcStream, CompressionMode.Decompress);
			var buffer = new byte[0x1000];
			while (true)
			{
				numRead = stream.Read(buffer, 0, buffer.Length);
				if (numRead == 0) break;
				dstStream.Write(buffer, 0, numRead);
			}
			dstStream.Close();
			return dstStream.ToArray();
		}


	}
}
