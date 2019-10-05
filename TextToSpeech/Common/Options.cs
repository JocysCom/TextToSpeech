using JocysCom.TextToSpeech.Monitor.Capturing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Speech.AudioFormat;

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

		[DefaultValue(false)]
		public bool MonitorsEnabled { get { return _MonitorsEnabled; } set { _MonitorsEnabled = value; OnPropertyChanged(); } }
		bool _MonitorsEnabled = false;

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

		[DefaultValue(false)]
		public bool DisplayMonitorEnabled { get { return _DisplayMonitorEnabled; } set { _DisplayMonitorEnabled = value; OnPropertyChanged(); } }
		bool _DisplayMonitorEnabled;

		[DefaultValue(200)]
		public int DisplayMonitorInterval { get { return _DisplayMonitorInterval; } set { _DisplayMonitorInterval = value; OnPropertyChanged(); } }
		int _DisplayMonitorInterval;

		public string DisplayMonitorPrefix { get { return _DisplayMonitorPrefix; } set { _DisplayMonitorPrefix = value; OnPropertyChanged(); } }
		string _DisplayMonitorPrefix = "#220000,#002200,#000022,#220000,#002200,#000022";

		[DefaultValue(0)]
		public int DisplayMonitorPositionX { get { return _DisplayMonitorPositionX; } set { _DisplayMonitorPositionX = value; OnPropertyChanged(); } }
		int _DisplayMonitorPositionX;

		[DefaultValue(0)]
		public int DisplayMonitorPositionY { get { return _DisplayMonitorPositionY; } set { _DisplayMonitorPositionY = value; OnPropertyChanged(); } }
		int _DisplayMonitorPositionY;

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
