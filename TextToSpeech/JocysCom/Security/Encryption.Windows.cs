using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

#if NETFRAMEWORK
using System.Web.Security;
#elif NETCOREAPP
// NuGet Package
using Microsoft.AspNetCore.DataProtection;
#endif

namespace JocysCom.ClassLibrary.Security
{
	/// <summary>
	/// Summary description for Encryption
	/// </summary>
	public partial class Encryption
	{


		#region Machine Key

		/// <summary>Encrypts text with MachineKey and converts to Base64 string.</summary>
		public static string ProtectWithMachineKey(string text, string purpose, Encoding encoding = null)
		{
			if (string.IsNullOrEmpty(text))
				return text;
			var bytes = (encoding ?? Encoding.UTF8).GetBytes(text);
#if NETFRAMEWORK
			var encrypted = MachineKey.Protect(bytes, purpose);
#elif NETCOREAPP
			var appName = Assembly.GetEntryAssembly().GetName().Name;
			var provider = DataProtectionProvider.Create(appName);
			var protector = provider.CreateProtector(purpose);
			var encrypted = protector.Protect(bytes);
#endif
			var base64 = System.Convert.ToBase64String(encrypted);
			return base64;
		}

		/// <summary>Decrypts text with MachineKey from Base64 string.</summary>
		public static string UnProtectWithMachineKey(string base64, string purpose, Encoding encoding = null)
		{
			if (string.IsNullOrEmpty(base64))
				return base64;
			var bytes = System.Convert.FromBase64String(base64);
#if NETFRAMEWORK
			var decrypted = MachineKey.Unprotect(bytes, purpose);
#elif NETCOREAPP
			var appName = Assembly.GetEntryAssembly().GetName().Name;
			var provider = DataProtectionProvider.Create(appName);
			var protector = provider.CreateProtector(purpose);
			var decrypted = protector.Unprotect(bytes);
#endif
			var text = (encoding ?? Encoding.UTF8).GetString(decrypted);
			return text;
		}

#endregion

			#region RSA Sign

			/// <summary>
			/// Private RSA key is required to sign data.
			/// </summary>
			/// <param name="bytes"></param>
			/// <returns></returns>
			/// <remarks>Private RSA key is required to sign data.</remarks>
			string RsaGenerateSignature(byte[] bytes)
		{
			//byte[] hash = RsaSignatureHashAlgorithm.ComputeHash(bytes);
			//byte[] sign = RsaProvider.SignHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"));
			byte[] sign;
			lock (RsaProviderLock)
			{
				sign = RsaProvider.SignData(bytes, RsaSignatureHashAlgorithm);
			}
			string signature = System.Convert.ToBase64String(sign);
			return signature;
		}

		/// <summary>
		/// Computes the hash value of the specified byte array using the specified hash
		/// algorithm, and signs the resulting hash value.
		/// </summary>
		/// <param name="bytes">The input data for which to compute the hash.</param>
		/// <returns> The System.Security.Cryptography.RSA signature in base64 format for the specified data.</returns>
		/// <remarks>Private RSA key is required to sign data.</remarks>
		public string RsaSignData(byte[] bytes)
		{
			string signature = RsaGenerateSignature(bytes);
			return signature;
		}

		/// <summary>
		/// Computes the hash value of the specified byte array using the specified hash
		/// algorithm, and signs the resulting hash value.
		/// </summary>
		/// <param name="text">The input data for which to compute the hash.</param>
		/// <returns> The System.Security.Cryptography.RSA signature in base64 format for the specified data.</returns>
		public string RsaSignData(string text)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
			return RsaSignData(bytes);
		}

		/// <summary>
		/// Computes the hash value of the specified file.
		/// </summary>
		/// <param name="text">Name of file to compute the hash.</param>
		/// <returns>The System.Security.Cryptography.RSA signature in base64 format for the specified data.</returns>
		public string RsaSignFile(string fileName)
		{
			System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
			if (!fi.Exists) return null;
			if (fi.Extension.ToLower() == ".xml")
			{
				// Create a new XML document.
				XmlDocument doc = new XmlDocument();
				// Load an XML file into the XmlDocument object.
				doc.PreserveWhitespace = true;
				doc.Load(fileName);
				// Sign document.
				XmlElement sign = RsaSignData(doc);
				// Save the document.
				doc.Save(fileName);
				// Save signature to *.rsa file.
				// return signature.
				return sign.InnerText;
			}
			else
			{
				string signFile = System.IO.Path.Combine(fi.FullName, ".rsa");
				string sign = RsaSignData(System.IO.File.ReadAllBytes(fi.FullName));
				System.IO.File.WriteAllText(signFile, sign);
				return sign;
			}
		}

		/// <summary>
		/// Computes the hash value of the specified System.Xml.XmlDocument object.
		/// </summary>
		/// <param name="doc">The System.Xml.XmlDocument object to sign.</param>
		/// <returns>The System.Security.Cryptography.RSA signature as XmlElement</returns>
		public XmlElement RsaSignData(XmlDocument doc)
		{
			// Create a SignedXml object.
			SignedXml signedXml = new SignedXml(doc);
			// Add the key to the SignedXml document.
			signedXml.SigningKey = RsaProvider;
			// Create a reference to be signed.
			Reference reference = new Reference();
			reference.Uri = "";
			// Add an enveloped transformation to the reference.
			XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
			reference.AddTransform(env);
			// Add the reference to the SignedXml object.
			signedXml.AddReference(reference);
			// Compute the signature.
			signedXml.ComputeSignature();
			// Get the XML representation of the signature and save
			// it to an XmlElement object.
			XmlElement xmlDigitalSignature = signedXml.GetXml();
			// Append the element to the XML document.
			doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
			return xmlDigitalSignature;
		}

		/// <summary>
		/// Verifies the specified signature data by comparing it to the signature computed
		/// for the specified data.
		/// </summary>
		/// <param name="text"> The data that was signed.</param>
		/// <param name="signature">The base64 signature data to be verified.</param>
		/// <returns>True if the signature verifies as valid; otherwise, false.</returns>
		public bool RsaVerifyData(string text, string signature)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
			return RsaVerifyData(bytes, signature);
		}

		/// <summary>
		/// Verifies the specified signature data by comparing it to the signature computed
		/// for the specified data.
		/// </summary>
		/// <param name="text"> The data that was signed.</param>
		/// <param name="signature">The base64 signature data to be verified.</param>
		/// <returns>True if the signature verifies as valid; otherwise, false.</returns>
		public bool RsaVerifyData(byte[] bytes, string signature)
		{
			//byte[] hash = SHA1.ComputeHash(bytes);
			//byte[] sign = RsaProvider.SignatureAlgorithm
			//VerifyHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"));
			string actualSignature = RsaGenerateSignature(bytes);
			return actualSignature.Equals(signature);
		}

		/// <summary>
		/// Determines whether the System.Security.Cryptography.Xml.SignedXml.Signature
		/// property verifies for the specified key.
		/// </summary>
		/// <param name="doc">The System.Xml.XmlDocument object to verify.</param>
		/// <returns>
		/// True if the System.Security.Cryptography.Xml.SignedXml.Signature property
		/// verifies for the specified key; otherwise, false.
		/// </returns>
		public bool RsaVerifyData(XmlDocument doc)
		{
			// Create a new SignedXml object and pass it
			// the XML document class.
			SignedXml signedXml = new SignedXml(doc);
			// Find the "Signature" node and create a new
			// XmlNodeList object.
			XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
			// Throw an exception if no signature was found.
			if (nodeList.Count <= 0)
			{
				throw new System.Security.Cryptography.CryptographicException("Verification failed: No Signature was found in the document.");
			}
			// This example only supports one signature for
			// the entire XML document.  Throw an exception 
			// if more than one signature was found.
			if (nodeList.Count >= 2)
			{
				throw new System.Security.Cryptography.CryptographicException("Verification failed: More that one signature was found for the document.");
			}
			// Load the first <signature> node.  
			signedXml.LoadXml((XmlElement)nodeList[0]);
			// Check the signature and return the result.
			return signedXml.CheckSignature(RsaProvider);
		}

		/// <summary>
		/// Computes the hash value of the specified file.
		/// </summary>
		/// <param name="text">Name of file to compute the hash.</param>
		/// <returns>True if the signature verifies as valid; otherwise, false.</returns>
		public bool RsaVerifyFile(string fileName)
		{
			System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
			if (!fi.Exists) return false;
			if (fi.Extension.ToLower() == ".xml")
			{
				// Create a new XML document.
				XmlDocument doc = new XmlDocument();
				// Load an XML file into the XmlDocument object.
				doc.PreserveWhitespace = true;
				doc.Load(fileName);
				// Verify document.
				return RsaVerifyData(doc);
			}
			else
			{
				string signFile = System.IO.Path.Combine(fi.FullName, ".rsa");
				string signature = System.IO.File.ReadAllText(signFile);
				return RsaVerifyData(System.IO.File.ReadAllBytes(fi.FullName), signature);
			}
		}

		#endregion


	}
}
