using JocysCom.ClassLibrary.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
		public bool Enabled { get { return _Enabled; } set { _Enabled = value; OnPropertyChanged(); } }
		bool _Enabled;

		[XmlAttribute]
		public string Group
		{
			get { return _Group; }
			set
			{
				_Group = string.IsNullOrEmpty((value ?? "").Trim()) ? null : value;
				OnPropertyChanged();
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
				OnPropertyChanged();
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
				OnPropertyChanged();
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
				OnPropertyChanged();
			}
		}
		string _Rx;

		public bool IsMatch(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			var value = text.ToUpper();
			return
				(!string.IsNullOrEmpty(Group) && Group.ToUpper().Contains(value)) ||
				(!string.IsNullOrEmpty(Key) && Key.ToUpper().Contains(value)) ||
				(!string.IsNullOrEmpty(Value) && Value.ToUpper().Contains(value));
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

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			lock (RegexValueLock)
			{
				if (propertyName == nameof(Key) || propertyName == nameof(Rx))
					_RegexValue = null;
			}
			var handler = PropertyChanged;
			if (handler == null)
				return;
			handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}

}
