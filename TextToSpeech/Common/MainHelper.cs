
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects.DataClasses;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JocysCom.TextToSpeech.Monitor
{
	public class MainHelper
	{

		#region Compression

		public static byte[] EmptyGzip = { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

		public static byte[] Compress(byte[] bytes)
		{
			int numRead;
			var srcStream = new MemoryStream(bytes);
			var dstStream = new MemoryStream();
			srcStream.Position = 0;
			var stream = new GZipStream(dstStream, CompressionMode.Compress);
			byte[] buffer = new byte[0x1000];
			while (true)
			{
				numRead = srcStream.Read(buffer, 0, buffer.Length);
				if (numRead == 0) break;
				stream.Write(buffer, 0, numRead);
			}
			stream.Close();
			return dstStream.ToArray();
		}

		public static byte[] Decompress(byte[] bytes)
		{
			int numRead;
			var srcStream = new MemoryStream(bytes);
			var dstStream = new MemoryStream();
			srcStream.Position = 0;
			var stream = new GZipStream(srcStream, CompressionMode.Decompress);
			var buffer = new byte[0x1000];
			while (true)
			{
				numRead = stream.Read(buffer, 0, buffer.Length);
				if (numRead == 0) break;
				dstStream.Write(buffer, 0, numRead);
			}
			dstStream.Close();
			return dstStream.ToArray();
		}

		#endregion

		public static KeyValue[] SplitText(string s, string[] separators)
		{
			var list = new List<KeyValue>();
			var sepIndex = new List<int>();
			var sepValue = new List<string>();
			var prevSepIndex = 0;
			var prevSepValue = "";
			// Loop trough every character of the string.
			for (int i = 0; i < s.Length; i++)
			{
				// Loop trough separators.
				for (int j = 0; j < separators.Length; j++)
				{
					var sep = separators[j];
					// If separator is empty then continue.
					if (string.IsNullOrEmpty(sep)) continue;
					int sepLen = sep.Length;
					// If separator is one char length and chars match or
					// separator is in the bounds of the string and string match then...
					if ((sepLen == 1 && s[i] == sep[0]) || (sepLen <= (s.Length - i) && string.CompareOrdinal(s, i, sep, 0, sepLen) == 0))
					{
						// Find string value from last separator.
						var prevIndex = prevSepIndex + prevSepValue.Length;
						var prevValue = s.Substring(prevIndex, i - prevIndex);
						var item = new KeyValue(sep, prevValue);
						list.Add(item);
						prevSepIndex = i;
						prevSepValue = sep;
						sepIndex.Add(i);
						sepValue.Add(sep);
						i += sepLen - 1;
						break;
					}
				}
			}
			// If no split were done then add complete string.
			if (list.Count == 0)
			{
				list.Add(new KeyValue("", s));
			}
			else
			{
				// Add last value
				var prevI = prevSepIndex + prevSepValue.Length;
				var value = s.Substring(prevI, s.Length - prevI);
				list.Add(new KeyValue("", value));
			}
			return list.ToArray();
		}

		public static string GetProductFullName()
		{
			Version v = new Version(Application.ProductVersion);
			var s = string.Format("{0} {1} {2}", Application.CompanyName, Application.ProductName, v.ToString(3));
			// Version = major.minor.build.revision
			switch (v.Build)
			{
				case 0: s += " Alpha"; break;  // Alpha Release (AR)
				case 1: s += " Beta 1"; break; // Master Beta (MB)
				case 2: s += " Beta 2"; break; // Feature Complete (FC)
				case 3: s += " Beta 3"; break; // Technical Preview (TP)
				case 4: s += " RC"; break;     // Release Candidate (RC)
				case 5: s += " RTM"; break; // Release to Manufacturing (RTM)
				default:
					// General Availability (GA) - Gold
					break;
			}
			DateTime buildDate = GetBuildDateTime(Application.ExecutablePath);
			s += buildDate.ToString(" (yyyy-MM-dd)");
			return s;
		}

		public static DateTime GetBuildDateTime(string filePath)
		{
			const int c_PeHeaderOffset = 60;
			const int c_LinkerTimestampOffset = 8;
			byte[] b = new byte[2048];
			System.IO.Stream s = null;
			try
			{
				s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				s.Read(b, 0, 2048);
			}
			finally
			{
				if (s != null)
				{
					s.Close();
				}
			}
			int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
			int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			dt = dt.AddSeconds(secondsSince1970);
			dt = dt.ToLocalTime();
			return dt;
		}

		public static Stream GetResource(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			foreach (var key in assembly.GetManifestResourceNames())
			{
				if (key.Contains(name)) return assembly.GetManifestResourceStream(key);
			}
			return null;
		}

		public static void OpenUrl(string url)
		{
			try
			{
				System.Diagnostics.Process.Start(url);
			}
			catch (System.ComponentModel.Win32Exception noBrowser)
			{
				if (noBrowser.ErrorCode == -2147467259)
					MessageBox.Show(noBrowser.Message);
			}
			catch (System.Exception other)
			{
				MessageBox.Show(other.Message);
			}
		}

		/// <summary>Add information about missing libraries and DLLs</summary>
		public static void AddExceptionMessage(Exception ex, ref string message)
		{
			var ex1 = ex as ConfigurationErrorsException;
			var ex2 = ex as ReflectionTypeLoadException;
			var m = "";
			if (ex1 != null)
			{
				m += string.Format("Filename: {0}\r\n", ex1.Filename);
				m += string.Format("Line: {0}\r\n", ex1.Line);
			}
			else if (ex2 != null)
			{
				foreach (Exception x in ex2.LoaderExceptions) m += x.Message + "\r\n";
			}
			if (message.Length > 0)
			{
				message += "===============================================================\r\n";
			}
			message += ex.ToString() + "\r\n";
			foreach (var key in ex.Data.Keys)
			{
				m += string.Format("{0}: {1}\r\n", key, ex1.Data[key]);
			}
			if (m.Length > 0)
			{
				message += "===============================================================\r\n";
				message += m;
			}
		}

		public static int GetNumber(int min, int max, string key, string value)
		{
			var s = key + value;
			var hash = Crc32.ComputeHash(s);
			var d = ((uint)max - (uint)min);
			if (d == uint.MaxValue) return (int)hash;
			return min + (int)(hash % (d + 1));
		}

		#region Path Converter

		/// <summary>
		/// Allows case insensitive replace.
		/// </summary>
		public static string Replace(string s, string oldValue, string newValue, StringComparison comparison)
		{
			StringBuilder sb = new StringBuilder();
			int previousIndex = 0;
			int index = s.IndexOf(oldValue, comparison);
			while (index != -1)
			{
				sb.Append(s.Substring(previousIndex, index - previousIndex));
				sb.Append(newValue);
				index += oldValue.Length;
				previousIndex = index;
				index = s.IndexOf(oldValue, index, comparison);
			}
			sb.Append(s.Substring(previousIndex));
			return sb.ToString();
		}

		static object SpecialFoldersLock = new object();
		
		static Dictionary<string, string> _SpecialFolders;

		static Dictionary<string, string> SpecialFolders
		{
			get
			{
				lock (SpecialFoldersLock)
				{
					if (_SpecialFolders == null)
					{
						var keys = (System.Environment.SpecialFolder[])Enum.GetValues(typeof(System.Environment.SpecialFolder));
						var items = new List<KeyValuePair<string, string>>();
						foreach (var key in keys)
						{
							var item = new KeyValuePair<string, string>(string.Format("$({0})", key), System.Environment.GetFolderPath(key));
							// Make sure all values are not empty and uniqe.
							if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
							{
								items.Add(item);
							}
						}
						_SpecialFolders = new Dictionary<string, string>();
						// Order forders descending.
						var list = items.OrderByDescending(x => x.Value).ToArray();
						foreach (var listItem in list)
						{
							// If list doesn't contains key then...
							if (!SpecialFolders.ContainsKey(listItem.Key))
							{
								SpecialFolders.Add(listItem.Key, listItem.Value);
							}
						}
					}
				}
				return _SpecialFolders;
			}
		}

		public static string ConvertToSpecialFoldersPattern(string path)
		{
			foreach (var key in SpecialFolders.Keys)
			{
				path = Replace(path, SpecialFolders[key], key, StringComparison.InvariantCultureIgnoreCase);
			}
			return path;
		}

		public static string ConvertFromSpecialFoldersPattern(string path)
		{
			foreach (var key in SpecialFolders.Keys)
			{
				path = Replace(path, key, SpecialFolders[key], StringComparison.InvariantCultureIgnoreCase);
			}
			return path;
		}

		#endregion

	}
}
