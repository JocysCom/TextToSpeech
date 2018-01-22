using JocysCom.ClassLibrary.Configuration;
using System.ComponentModel;
using System.Text.RegularExpressions;
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

		public bool IsMatch(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			var value = text.ToLower();
			return
				(!string.IsNullOrEmpty(Group) && Group.ToLower().Contains(value)) ||
                (!string.IsNullOrEmpty(Key) && Key.ToLower().Contains(value)) ||
				(!string.IsNullOrEmpty(Value) && Value.ToLower().Contains(value));
		}

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
					var customRx = !string.IsNullOrEmpty(_Rx);
					var rx = customRx ? _Rx : _Key;
					if (_RegexValue == null && !string.IsNullOrEmpty(rx))
					{
						if (customRx)
						{
							_RegexValue = new Regex(rx, RegexOptions.Compiled);
						}
						else
						{
							_RegexValue = new Regex("\\b" + rx + "\\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
						}
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
