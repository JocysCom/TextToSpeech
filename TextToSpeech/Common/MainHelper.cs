using JocysCom.WoW.TextToSpeech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JocysCom.WoW.TextToSpeech
{
    public class MainHelper
    {

        #region Serializers

        static Dictionary<Type, XmlSerializer> _XmlSerializers;
        static Dictionary<Type, XmlSerializer> XmlSerializers
        {
            get
            {
                return _XmlSerializers = _XmlSerializers ?? new Dictionary<Type, XmlSerializer>();
            }
            set { _XmlSerializers = value; }
        }

        static XmlSerializer GetXmlSerializer(Type type)
        {
            lock (XmlSerializers)
            {
                if (!XmlSerializers.ContainsKey(type))
                {
                    XmlSerializers.Add(type, new XmlSerializer(type, new Type[] { typeof(string) }));
                }
            }
            return XmlSerializers[type];
        }

        /// <summary>
        /// Serialize object to XML string.
        /// </summary>
        /// <param name="o">The object to serialize.</param>
        /// <param name="encoding">The encoding to generate.</param>
        /// <param name="namespaces">Contains the XML namespaces and prefixes that the XmlSerializer  uses to generate qualified names in an XML-document instance.</param>
        /// <returns>XML string.</returns>
        static string SerializeToXmlString(object o, Encoding encoding, bool omitXmlDeclaration = false)
        {
            encoding = encoding ?? Encoding.UTF8;
            var ms = new MemoryStream();
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Encoding = encoding;
            settings.Indent = true;
            var xw = XmlTextWriter.Create(ms, settings);
            var serializer = GetXmlSerializer(o.GetType());
            lock (serializer) { serializer.Serialize(xw, o); }
            var tr = new StreamReader(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var xml = tr.ReadToEnd();
            xw.Flush();
            xw.Close();
            //ms.Close();
            return xml;
        }

        public static string SerializeToXmlString(object o)
        {
            return SerializeToXmlString(o, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// Deserialize object from XML string.
        /// </summary>
        /// <param name="xml">Xml string representing object.</param>
        /// <param name="type">Type of object.</param>
        /// <returns>Object.</returns>
        static object DeserializeFromXmlString(string xml, Type type, Encoding encoding)
        {
            MemoryStream ms = new MemoryStream(encoding.GetBytes(xml));
            // Use stream reader to avoid error: There is no Unicode byte order mark. Cannot switch to Unicode.
            var sr = new StreamReader(ms, encoding);
            XmlSerializer serializer = GetXmlSerializer(type);
            object o;
            lock (serializer) { o = serializer.Deserialize(sr); }
            sr.Close();
            return o;
        }

        public static T DeserializeFromXmlString<T>(string xml)
        {
            return (T)DeserializeFromXmlString(xml, typeof(T), System.Text.Encoding.UTF8);
        }

        #endregion

        /// <summary>
        /// It will take too long to convert large amount of text to WAV data and apply all filters.
        /// This function will split text into smaller sentences.
        /// </summary>
        public static string[] SplitText(string text)
        {
            List<string> blocks = new List<string>();
            var splitItems = SplitText(text, new string[] { ". ", "! ", "? " });
            var sentences = splitItems.Select(x => (x.Value + x.Key).Trim()).Where(x=>x.Length > 0).ToArray();
            //var sentences = text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            string block = "";
            // Loop trough each sentence.
            for (int i = 0; i < sentences.Length; i++)
            {
                block += sentences[i];
                try
                {
                    // Make sure that each sentence is a valid XML.
                    XDocument document = XDocument.Parse("<p>" + block + "</p>");
                    blocks.Add(block);
                    block = "";
                }
                catch (Exception)
                {
                    // Probably failed, because XML tag ends in another sentence.
                    // Continue adding
                }

            }
            return blocks.ToArray();
        }

        static KeyValue[] SplitText(string s, string[] separators)
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
            return s;
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


    }
}
