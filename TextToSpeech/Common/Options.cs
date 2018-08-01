using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class Options : INotifyPropertyChanged
	{
		public Options()
		{
		}

		/// <summary>
		/// Avoid deserialization duplicates by using separate method.
		/// </summary>
		public void InitDefaults()
		{
			JocysCom.ClassLibrary.Runtime.Helper.ResetPropertiesToDefault(this);
		}

		[DefaultValue("")]
		public string VoicesData { get; set; }

		
		[DefaultValue("")]
		public string ProgramComboBoxText { get; set; }
		
		[DefaultValue(true)]
		public bool MonitorPortChecked { get; set; }
		
		[DefaultValue(typeof(Decimal), "0")]
		public decimal DelayBeforeValue { get; set; }
		
		[DefaultValue(typeof(Decimal), "0")]
		public decimal AddSilcenceBeforeMessage { get; set; }
		
		[DefaultValue(false)]
		public bool LogEnable { get; set; }
		
		[DefaultValue("me66age")]
		public string LogText { get; set; }
		
		[DefaultValue(true)]
		public bool LogSound { get; set; }
		
		[DefaultValue(false)]
		public bool UseWinCap { get; set; }
		
		[DefaultValue("")]
		public string RateMinComboBoxText { get; set; }
		
		[DefaultValue("")]
		public string PitchMinComboBoxText { get; set; }
		
		[DefaultValue("")]
		public string RateMaxComboBoxText { get; set; }
		
		[DefaultValue("")]
		public string PitchMaxComboBoxText { get; set; }
		
		[DefaultValue("")]
		public string GenderComboBoxText { get; set; }
	
		[DefaultValue("")]
		public string MonitorClipboardComboBoxText { get; set; }
		
		[DefaultValue("")]
		public string DefaultIntroSoundComboBox { get; set; }

		[DefaultValue("")]
		public string PlaybackDevice { get; set; }

		[DefaultValue(false)]
		public bool CacheDataGeneralize { get; set; }

		[DefaultValue(true)]
		public bool CacheDataRead { get; set; }

		[DefaultValue(false)]
		public bool CacheDataWrite { get; set; }


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
