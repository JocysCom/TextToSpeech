using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public class KeyValue : KeyValue<string>
    {
        public KeyValue() { }

        public KeyValue(string key, string value) : base(key, value) { }
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public class KeyValue<T> : INotifyPropertyChanged
    {

        public KeyValue()
        {
        }

        public KeyValue(T key, T value)
        {
            _key = key;
            _value = value;
        }

        T _key;
        T _value;
        public T Key
        {
            get { return _key; }
            set
            {
                if (!IEquatable<T>.Equals(_key, value))
                {
                    _key = value;
                    NotifyPropertyChanged("Key");
                }
            }
        }
        public T Value
        {
            get { return _value; }
            set
            {
                if (!IEquatable<T>.Equals(_value, value))
                {
                    _value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", Key, Value);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            var ev = PropertyChanged;
            if (ev == null) return;
            ev(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
