using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Speech.AudioFormat;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class Options : INotifyPropertyChanged
	{
		public Options()
		{
			JocysCom.ClassLibrary.Runtime.Helper.ResetPropertiesToDefault(this, false);
		}

		/// <summary>
		/// Avoid deserialization duplicates by using separate method.
		/// </summary>
		public void InitDefaults(bool onlyIfNull = false)
		{
			JocysCom.ClassLibrary.Runtime.Helper.ResetPropertiesToDefault(this, onlyIfNull);
		}

		[DefaultValue("")]
		public string VoicesData { get; set; }

		
		[DefaultValue("WoW")]
		public string ProgramComboBoxText { get; set; }
		
		[DefaultValue(true)]
		public bool MonitorPortChecked { get; set; }
		
		[DefaultValue(typeof(decimal), "0")]
		public decimal DelayBeforeValue { get; set; }
		
		[DefaultValue(typeof(decimal), "0")]
		public decimal AddSilcenceBeforeMessage { get; set; }
		
		[DefaultValue(false)]
		public bool LogEnable { get; set; }
		
		[DefaultValue("me66age")]
		public string LogText { get; set; }
		
		[DefaultValue(true)]
		public bool LogSound { get; set; }
		
		[DefaultValue(false)]
		public bool UseWinCap { get; set; }
		
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
		public int AudioBitsPerSample { get; set; }

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public static string GetName(Expression<Func<Options, object>> selector)
		{
			var body = selector.Body as MemberExpression;
			if (body == null)
			{
				var ubody = (UnaryExpression)selector.Body;
				body = ubody.Operand as MemberExpression;
			}
			return body.Member.Name;
		}

		public void ReportPropertyChanged(Expression<Func<Options, object>> selector)
		{
			var ev = PropertyChanged;
			if (ev == null) return;
			var name = GetName(selector);
			ev(this, new PropertyChangedEventArgs(name));
		}

		#endregion


	}
}
