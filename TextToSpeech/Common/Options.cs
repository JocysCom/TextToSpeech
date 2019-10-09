using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Speech.AudioFormat;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class Options : INotifyPropertyChanged
	{
		public Options()
		{
			JocysCom.ClassLibrary.Runtime.RuntimeHelper.ResetPropertiesToDefault(this, false);
		}

		/// <summary>
		/// Avoid deserialization duplicates by using separate method.
		/// </summary>
		public void InitDefaults(bool onlyIfNull = false)
		{
			JocysCom.ClassLibrary.Runtime.RuntimeHelper.ResetPropertiesToDefault(this, onlyIfNull);
		}

		[DefaultValue("")]
		public string VoicesData { get; set; }


		[DefaultValue("WoW")]
		public string ProgramComboBoxText { get; set; }


		[DefaultValue(typeof(decimal), "0")]
		public decimal AddSilenceAfterMessage { get; set; }

		[DefaultValue(typeof(decimal), "0")]
		public decimal AddSilenceBeforeMessage { get; set; }

		[DefaultValue(false)]
		public bool LogEnable { get; set; }

		[DefaultValue("me66age")]
		public string LogText { get; set; }

		[DefaultValue(true)]
		public bool LogSound { get; set; }

		[DefaultValue(true)]
		public bool MonitorsEnabled { get { return _MonitorsEnabled; } set { _MonitorsEnabled = value; OnPropertyChanged(); } }
		bool _MonitorsEnabled = true;

		#region UDP Monitor

		[DefaultValue(false)]
		public bool UdpMonitorEnabled { get { return _UdpMonitorEnabled; } set { _UdpMonitorEnabled = value; OnPropertyChanged(); } }
		bool _UdpMonitorEnabled;

		[DefaultValue(42500)]
		public int UdpMonitorPort { get { return _UdpMonitorPort; } set { _UdpMonitorPort = value; OnPropertyChanged(); } }
		int _UdpMonitorPort;

		#endregion

		#region Clipboard Monitor

		[DefaultValue(false)]
		public bool ClipboardMonitorEnabled { get { return _ClipboardMonitorEnabled; } set { _ClipboardMonitorEnabled = value; OnPropertyChanged(); } }
		bool _ClipboardMonitorEnabled;

		[DefaultValue(200)]
		public int ClipboardMonitorInterval { get { return _ClipboardMonitorInterval; } set { _ClipboardMonitorInterval = value; OnPropertyChanged(); } }
		int _ClipboardMonitorInterval;

		#endregion

		#region Network Monitor

		[DefaultValue(false)]
		public bool NetworkMonitorEnabled { get { return _NetworkMonitorEnabled; } set { _NetworkMonitorEnabled = value; OnPropertyChanged(); } }
		bool _NetworkMonitorEnabled;

		[DefaultValue(CapturingType.SocPcap)]
		public CapturingType NetworkMonitorCapturingType { get { return _CapturingType; } set { _CapturingType = value; OnPropertyChanged(); } }
		CapturingType _CapturingType;
		public string NetworkMonitorLogFolder { get { return _NetworkMonitorLogsFolder; } set { _NetworkMonitorLogsFolder = value; OnPropertyChanged(); } }
		string _NetworkMonitorLogsFolder;

		#endregion

		#region Display Monitor

		[DefaultValue(true)]
		public bool DisplayMonitorEnabled { get { return _DisplayMonitorEnabled; } set { _DisplayMonitorEnabled = value; OnPropertyChanged(); } }
		bool _DisplayMonitorEnabled = true;

		[DefaultValue(100)]
		public int DisplayMonitorInterval { get { return _DisplayMonitorInterval; } set { _DisplayMonitorInterval = value; OnPropertyChanged(); } }
		int _DisplayMonitorInterval = 100;

		[DefaultValue("#220000,#002200,#000022,#220000,#002200,#000022")]
		public string DisplayMonitorPrefix { get { return _DisplayMonitorPrefix; } set { _DisplayMonitorPrefix = value; OnPropertyChanged(); } }
		string _DisplayMonitorPrefix = "#220000,#002200,#000022,#220000,#002200,#000022";

		[DefaultValue(0)]
		public int DisplayMonitorPositionX { get { return _DisplayMonitorPositionX; } set { _DisplayMonitorPositionX = value; OnPropertyChanged(); } }
		int _DisplayMonitorPositionX;

		[DefaultValue(0)]
		public int DisplayMonitorPositionY { get { return _DisplayMonitorPositionY; } set { _DisplayMonitorPositionY = value; OnPropertyChanged(); } }
		int _DisplayMonitorPositionY;

		public void DisplayMonitorResetSettings()
		{
			DisplayMonitorEnabled = ClassLibrary.Runtime.Attributes.GetDefaultValue<Options, bool>(nameof(DisplayMonitorEnabled));
			DisplayMonitorInterval = ClassLibrary.Runtime.Attributes.GetDefaultValue<Options, int>(nameof(DisplayMonitorInterval));
			DisplayMonitorPrefix = ClassLibrary.Runtime.Attributes.GetDefaultValue<Options, string>(nameof(DisplayMonitorPrefix));
			DisplayMonitorPositionX = ClassLibrary.Runtime.Attributes.GetDefaultValue<Options, int>(nameof(DisplayMonitorPositionX));
			DisplayMonitorPositionY = ClassLibrary.Runtime.Attributes.GetDefaultValue<Options, int>(nameof(DisplayMonitorPositionY));
		}

		#endregion

		#region Voices: Amazon

		[DefaultValue(null)]
		public string AmazonRegionSystemName { get { return _AmazonRegionSystemName; } set { _AmazonRegionSystemName = value; OnPropertyChanged(); } }
		string _AmazonRegionSystemName;

		[XmlIgnore]
		public string AmazonAccessKey
		{
			get { return UserDecrypt(_AmazonAccessKeyEncrypted); }
			set { _AmazonAccessKeyEncrypted = UserEncrypt(value); OnPropertyChanged(); }
		}
		[DefaultValue(null), XmlElement(ElementName = nameof(AmazonAccessKey))]
		public string _AmazonAccessKeyEncrypted { get; set; }

		[XmlIgnore]
		public string AmazonSecretKey
		{
			get { return UserDecrypt(_AmazonSecretKeyEncrypted); }
			set { _AmazonSecretKeyEncrypted = UserEncrypt(value); OnPropertyChanged(); }
		}
		[DefaultValue(null), XmlElement(ElementName = nameof(AmazonSecretKey))]
		public string _AmazonSecretKeyEncrypted { get; set; }

		#endregion

		#region Encrypt Settings 

		static string UserEncrypt(string text)
		{
			try
			{
				//var user = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
				var user = "AppContext";
				return JocysCom.ClassLibrary.Security.Encryption.ProtectWithMachineKey(text, user);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return null;
		}

		static string UserDecrypt(string base64)
		{
			try
			{
				//var user = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
				var user = "AppContext";
				return JocysCom.ClassLibrary.Security.Encryption.UnProtectWithMachineKey(base64, user);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return null;
		}

		#endregion

		// Non default pitch adds noise.
		[DefaultValue(0)]
		public int PitchMin { get; set; }

		// Non default pitch adds noise.
		[DefaultValue(0)]
		public int PitchMax { get; set; }

		[DefaultValue(2)]
		public int RateMin { get; set; }

		[DefaultValue(2)]
		public int RateMax { get; set; }

		[DefaultValue("Male")]
		public string GenderComboBoxText { get; set; }

		[DefaultValue("Disable")]
		public string MonitorClipboardComboBoxText { get; set; }

		[DefaultValue("Radio")]
		public string DefaultIntroSoundComboBox { get; set; }

		[DefaultValue("")]
		public string PlaybackDevice { get; set; }

		[DefaultValue(false)]
		public bool CacheDataGeneralize { get; set; }

		[DefaultValue(true)]
		public bool CacheDataRead { get; set; }

		[DefaultValue(false)]
		public bool CacheDataWrite { get; set; }

		[DefaultValue(100)]
		public int Volume { get; set; }

		[DefaultValue(AudioChannel.Mono)]
		public AudioChannel AudioChannels { get; set; }

		[DefaultValue(22050)]
		public int AudioSampleRate { get; set; }

		[DefaultValue(16)]
		public int AudioBitsPerSample
		{
			get { return _AudioBitsPerSample; }
			set { _AudioBitsPerSample = value; OnPropertyChanged(); }
		}
		int _AudioBitsPerSample;

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion


	}
}
