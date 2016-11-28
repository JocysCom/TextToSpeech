using JocysCom.ClassLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor
{
	public class Acronym : INotifyPropertyChanged, ISettingsItem
	{

		public Acronym()
		{
			_Enabled = true;
		}

		[XmlAttribute]
		public bool Enabled { get { return _Enabled; } set { _Enabled = value; NotifyPropertyChanged("Enabled"); } }
		bool _Enabled;

		[XmlAttribute]
		public string Group
		{
			get { return _Group; }
			set
			{
				_Group = string.IsNullOrEmpty((value ?? "").Trim()) ? null : value;
				NotifyPropertyChanged("Group");
			}
		}
		string _Group;

		[XmlAttribute]
		public string Key
		{
			get { return _Key; }
			set
			{
				_Key = string.IsNullOrEmpty((value ?? "").Trim()) ? null : value;
				NotifyPropertyChanged("Key");
			}
		}
		string _Key;

		[XmlAttribute]
		public string Value
		{
			get { return _Value; }
			set
			{
				_Value = string.IsNullOrEmpty((value ?? "").Trim()) ? null : value;
				NotifyPropertyChanged("Value");
			}
		}
		string _Value;

		[XmlAttribute]
		public string Rx
		{
			get { return _Rx; }
			set
			{
				_Rx = string.IsNullOrEmpty((value ?? "").Trim()) ? null : value;
				NotifyPropertyChanged("Rx");
			}
		}
		string _Rx;

		[XmlIgnore]
		public bool IsEmpty
		{
			get
			{
				return
					string.IsNullOrEmpty(_Group) &&
					string.IsNullOrEmpty(_Key) &&
					string.IsNullOrEmpty(_Value) &&
					string.IsNullOrEmpty(_Rx);
			}
		}

		object RegexValueLock = new object();

		[XmlIgnore]
		public Regex RegexValue
		{
			get
			{
				lock (RegexValueLock)
				{
					var customRx = string.IsNullOrEmpty(_Rx);
					var rx = customRx ? _Key : _Rx;
					if (_RegexValue == null && !string.IsNullOrEmpty(rx))
					{
						var options = customRx
							? RegexOptions.Compiled
							: RegexOptions.IgnoreCase | RegexOptions.Compiled;
						_RegexValue = new Regex("\\b" + rx + "\\b", options);
					}
					return _RegexValue;
				}
			}
		}
		private Regex _RegexValue;

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName = "")
		{
			var ev = PropertyChanged;
			lock (RegexValueLock)
			{
				if (propertyName == "Key" || propertyName == "Rx")
				{
					_RegexValue = null;
				}
			}
			if (ev == null) return;
			ev(this, new PropertyChangedEventArgs(propertyName));
		}



		#endregion
	}

}
