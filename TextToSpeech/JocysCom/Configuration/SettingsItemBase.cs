using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace JocysCom.ClassLibrary.Configuration
{
	public class SettingsItemBase
	{

		public object this[string propertyName]
		{
			get
			{
				return GetPropertyValueByName(propertyName);
			}
			set
			{
				SetPropertyValueByName(propertyName, value);
			}
		}

		PropertyInfo[] _Properties;
		object PropertiesLock = new object();

		PropertyInfo[] Properties
		{
			get
			{

				lock (PropertiesLock)
				{
					if (_Properties == null)
					{
						_Properties = GetType().GetProperties();
					}
					return _Properties;
				}
			}
		}

		object GetPropertyValueByName(string propertyName)
		{
			var property = Properties.FirstOrDefault(x => x.Name == propertyName);
			if (property == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("Settings property \"{0}\" not found.", propertyName));
			}
			if (!property.CanRead)
			{
				throw new SettingsPropertyNotFoundException(string.Format("Settings property \"{0}\" can not be read.", propertyName));
			}
			var propertyValue = property.GetValue(this, null);
			return propertyValue;
		}

		void SetPropertyValueByName(string propertyName, object propertyValue)
		{
			var property = Properties.FirstOrDefault(x => x.Name == propertyName);
			if (property == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("Settings property \"{0}\" not found.", propertyName));
			}
			if (!property.CanWrite)
			{
				throw new SettingsPropertyNotFoundException(string.Format("Settings property \"{0}\" can not be written.", propertyName));
			}
			if (propertyValue != null && !property.PropertyType.IsInstanceOfType(propertyValue))
			{
				throw new SettingsPropertyWrongTypeException(string.Format("Settings property \"{0}\" wrong type.", propertyName));
			}
			property.SetValue(this, propertyValue, null);
		}

	}
}
