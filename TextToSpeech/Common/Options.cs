using JocysCom.TextToSpeech.Monitor.Capturing;
using NAudio.Wave;
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

		#region General Options

		[DefaultValue(0)]
		public long AddSilenceBeforeMessage { get { return _AddSilenceBeforeMessage; } set { _AddSilenceBeforeMessage = value; OnPropertyChanged(); } }
		long _AddSilenceBeforeMessage;

		[DefaultValue(0)]
		public long AddSilenceAfterMessage { get { return _AddSilenceAfterMessage; } set { _AddSilenceAfterMessage = value; OnPropertyChanged(); } }
		long _AddSilenceAfterMessage;

		[DefaultValue(true)]
		public bool SplitMessageIntoSentences { get { return _SplitMessageIntoSentences; } set { _SplitMessageIntoSentences = value; OnPropertyChanged(); } }
		bool _SplitMessageIntoSentences = true;

		#endregion

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
		public bool AmazonEnabled { get { return _AmazonEnabled; } set { _AmazonEnabled = value; OnPropertyChanged(); } }
		bool _AmazonEnabled;

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

		[DefaultValue(MessageGender.Male)]
		public MessageGender GenderComboBoxText { get; set; }

		[DefaultValue("Disable")]
		public string MonitorClipboardComboBoxText { get; set; }

		[DefaultValue("Radio")]
		public string DefaultIntroSoundComboBox { get; set; }

		[DefaultValue("")]
		public string PlaybackDevice { get; set; }

		[DefaultValue(false)]
		public bool CacheDataWrite { get { return _CacheDataWrite; } set { _CacheDataWrite = value; OnPropertyChanged(); } }
		bool _CacheDataWrite;

		[DefaultValue(true)]
		public bool CacheDataRead { get { return _CacheDataRead; } set { _CacheDataRead = value; OnPropertyChanged(); } }
		bool _CacheDataRead;

		[DefaultValue(false)]
		public bool CacheDataGeneralize { get { return _CacheDataGeneralize; } set { _CacheDataGeneralize = value; OnPropertyChanged(); } }
		bool _CacheDataGeneralize;

		[DefaultValue(100)]
		public int Volume { get; set; }

		[DefaultValue(AudioChannel.Mono)]
		public AudioChannel AudioChannels { get { return _AudioChannels; } set { _AudioChannels = value; OnPropertyChanged(); } }
		AudioChannel _AudioChannels = AudioChannel.Mono;

		[DefaultValue(22050)]
		public int AudioSampleRate { get { return _AudioSampleRate; } set { _AudioSampleRate = value; OnPropertyChanged(); } }
		int _AudioSampleRate = 22050;

		[DefaultValue(16)]
		public int AudioBitsPerSample { get { return _AudioBitsPerSample; } set { _AudioBitsPerSample = value; OnPropertyChanged(); } }
		int _AudioBitsPerSample = 16;

		#region Audio File Cache

		[DefaultValue(false)]
		public bool CacheAudioConvert { get { return _CacheAudioConvert; } set { _CacheAudioConvert = value; OnPropertyChanged(); } }
		bool _CacheAudioConvert;

		[DefaultValue(Audio.CacheFileFormat.ULaw)]
		public Audio.CacheFileFormat CacheAudioFormat { get { return _CacheAudioFormat; } set { _CacheAudioFormat = value; OnPropertyChanged(); } }
		Audio.CacheFileFormat _CacheAudioFormat = Audio.CacheFileFormat.ULaw;

		[DefaultValue(AudioChannel.Mono)]
		public AudioChannel CacheAudioChannels { get { return _CacheAudioChannels; } set { _CacheAudioChannels = value; OnPropertyChanged(); } }
		AudioChannel _CacheAudioChannels = AudioChannel.Mono;

		[DefaultValue(22050)]
		public int CacheAudioSampleRate { get { return _CacheAudioSampleRate; } set { _CacheAudioSampleRate = value; OnPropertyChanged(); } }
		int _CacheAudioSampleRate = 22050;

		[DefaultValue(16)]
		public int CacheAudioBitsPerSample { get { return _CacheAudioBitsPerSample; } set { _CacheAudioBitsPerSample = value; OnPropertyChanged(); } }
		int _CacheAudioBitsPerSample = 16;

		// Data rates(hence, bit-rates) are always expressed in powers of 10, not 2.
		// 128 kilobits per second = 128,000 bits per second = 16,000 bytes per second.
		//
		// 32 kbit/s – generally acceptable only for speech
		// 96 kbit/s – generally used for speech or low-quality streaming
		// 128 or 160 kbit/s – mid-range bitrate quality
		// 192 kbit/s – medium quality bitrate
		// 256 kbit/s – a commonly used high-quality bitrate
		// 320 kbit/s – highest level supported by the MP3 standard	
		//	
		[DefaultValue(128000)]
		public int CacheAudioAverageBitsPerSecond { get { return _CacheAudioAverageBitsPerSecond; } set { _CacheAudioAverageBitsPerSecond = value; OnPropertyChanged(); } }
		int _CacheAudioAverageBitsPerSecond = 128000;

		// Block Alignment = Bytes per Sample x Number of Channels
		// For example, the block alignment value for 16-bit PCM format mono audio is 2 (2 bytes per sample x 1 channel). For 16-bit PCM format stereo audio, the block alignment value is 4.
		[DefaultValue(2)]
		public int CacheAudioBlockAlign { get { return _CacheAudioBlockAlign; } set { _CacheAudioBlockAlign = value; OnPropertyChanged(); } }
		int _CacheAudioBlockAlign = 2;

		#endregion

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
