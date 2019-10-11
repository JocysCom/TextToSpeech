using JocysCom.ClassLibrary.Runtime;
using SharpDX.DirectSound;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public class EffectsPreset
	{

		public EffectsPreset()
		{
			Chorus = new ChorusSettings();
		}

		public string Name { get; set; }

		static string _fileSufix = ".Preset.xml";

		//public static Guid[] effects = new Guid[] {
		//    DSoundHelper.StandardChorusGuid,
		//    DSoundHelper.StandardCompressorGuid,
		//    DSoundHelper.StandardDistortionGuid,
		//    DSoundHelper.StandardEchoGuid,
		//    DSoundHelper.StandardFlangerGuid,
		//    DSoundHelper.StandardGargleGuid,
		//    DSoundHelper.StandardInteractive3DLevel2ReverbGuid,
		//    DSoundHelper.StandardParamEqGuid,
		//    DSoundHelper.StandardWavesReverbGuid,
		//};

		public static DirectoryInfo GetPresetsFolder()
		{
			var folder = SettingsFile.Current.FolderPath;
			folder = System.IO.Path.Combine(folder, "Presets");
			var dir = new DirectoryInfo(folder);
			if (!dir.Exists)
			{
				dir.Create();
			}
			return dir;
		}

		public static BindingList<EffectsPreset> GetPresets()
		{
			var dir = GetPresetsFolder();
			var presetNames = dir.GetFiles("*" + _fileSufix, System.IO.SearchOption.AllDirectories)
				.Select(x => x.Name.Replace(_fileSufix, ""))
				.Distinct()
				.OrderBy(x => x);
			var list = new BindingList<EffectsPreset>();
			foreach (var name in presetNames)
			{
				var preset = LoadPreset(name);
				list.Add(preset);
			}
			// move default to the top.
			var def = list.FirstOrDefault(x => x.Name == "Default");
			// Create if not exists.
			if (def == null)
			{
				def = new EffectsPreset();
				def.Name = "Default";
				var assembly = Assembly.GetExecutingAssembly();
				var names = assembly.GetManifestResourceNames().Where(x => x.EndsWith(_fileSufix)).ToArray();
				foreach (var name in names)
				{
					var stream = MainHelper.GetResource(name);
					var sr = new StreamReader(stream, Encoding.UTF8, true);
					var xml = sr.ReadToEnd();
					sr.Close();
					var preset = Serializer.DeserializeFromXmlString<EffectsPreset>(xml);
					SavePreset(preset);
					if (name.Contains("Default"))
					{
						def = preset;
					}
					else
					{
						list.Add(preset);
					}
				}
			}
			else
			{
				list.Remove(def);
			}
			list.Insert(0, def);
			return list;
		}

		public static void SavePreset(EffectsPreset preset)
		{
			var fileName = preset.Name + _fileSufix;
			var dir = GetPresetsFolder();
			var presetsDir = new System.IO.DirectoryInfo("Presets");
			if (!presetsDir.Exists) presetsDir = dir;
			var file = dir.GetFiles(fileName, System.IO.SearchOption.AllDirectories).FirstOrDefault();
			// if file doesn't exist anywhere then...
			if (file == null)
			{
				var path = System.IO.Path.Combine(presetsDir.FullName, fileName);
				file = new System.IO.FileInfo(path);
			}
			var xml = Serializer.SerializeToXmlString(preset);
			System.IO.File.WriteAllText(file.FullName, xml, System.Text.Encoding.UTF8);
		}

		public static EffectsPreset LoadPreset(string name)
		{
			var dir = GetPresetsFolder();
			var fi = dir.GetFiles(name + _fileSufix, System.IO.SearchOption.AllDirectories).FirstOrDefault();
			if (fi == null) return null;
			var preset = Serializer.DeserializeFromXmlFile<EffectsPreset>(fi.FullName);
			return preset;
		}

		public static EffectsPreset NewPreset()
		{
			var i = 1;
			var dir = GetPresetsFolder();
			// Find unused name;
			while (true)
			{
				var name = string.Format("NewPrest{0}", i);
				var fi = dir.GetFiles(name + _fileSufix, System.IO.SearchOption.AllDirectories).FirstOrDefault();
				if (fi == null)
				{
					var preset = new EffectsPreset();
					preset.Name = name;
					SavePreset(preset);
					return preset;
				}
				i++;
			}
		}

		public bool GeneralEnabled { get; set; }
		public bool ChorusEnabled { get; set; }
		public bool CompressorEnabled { get; set; }
		public bool DistortionEnabled { get; set; }
		public bool EchoEnabled { get; set; }
		public bool FlangerEnabled { get; set; }
		public bool GargleEnabled { get; set; }
		public bool ParamEqEnabled { get; set; }
		public bool ReverbEnabled { get; set; }
		public bool Reverb3DEnabled { get; set; }

		public EffectsGeneral General { get; set; }
		public ChorusSettings Chorus { get; set; }
		public CompressorSettings Compressor { get; set; }
		public DistortionSettings Distortion { get; set; }
		public EchoSettings Echo { get; set; }
		public FlangerSettings Flanger { get; set; }
		public GargleSettings Gargle { get; set; }
		public ParametricEqualizerSettings ParamEq { get; set; }
		public WavesReverbSettings Reverb { get; set; }
		public I3DL2ReverbSettings Reverb3D { get; set; }

	}
}
