using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JocysCom.ClassLibrary.Security
{
	/// <summary>
	/// Microsoft Private Key Format helper. PVK is a proprietary Microsoft format
	/// that stores a cryptographic private key and can be password-protected.
	/// PVK files can be used in Microsoft SQL Server certificate operations.
	/// </summary>
	public class PrivateKeyHelper
	{
		// Example:
		//
		//// Load Certificate (Public Key)
		//var certPub = new X509Certificate2("SqlTestCertificate01.cer", (string)null, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
		//var rsaPub = (RSACryptoServiceProvider)certPub.PublicKey.Key;
		//// Get unencrypted bytes.
		//var bytes = System.Text.Encoding.Unicode.GetBytes("Test");
		//// Encrypt text with certificate.
		//var encryptedBytes = rsaPub.Encrypt(bytes, true);
		//// Load PVK file (Private key) bytes.
		//var raw = System.IO.File.ReadAllBytes("SqlTestCertificate01.pvk");
		//// Convert PVK file to RSACryptoServiceProvider.
		//var rsaPri = PrivateKeyHelper.Convert(raw, "password1234$");
		//// Decrypt bytes.
		//var decryptedBytes = rsaPri.Decrypt(encryptedBytes, true);
		//// Convert bytes back to "Test" text.
		//var text = System.Text.Encoding.Unicode.GetString(decryptedBytes);

		const uint _Magic = 0xb0b5f11e;

		/// <summary>
		/// Convert PVK file bytes to RSACryptoServiceProvider.
		/// </summary>
		/// <param name="pvk">PVK File bytes.</param>
		/// <param name="password">Optional password.</param>
		/// <param name="weak">Weak encryption option (US export restrictions)</param>
		/// <returns>RSACryptoServiceProvider</returns>
		public static RSACryptoServiceProvider Convert(byte[] pvk, string password = null, bool weak = false)
		{
			var br = new BinaryReader(new MemoryStream(pvk));
			var magic = br.ReadUInt32();
			if (magic != _Magic)
				return null;
			var reserved = br.ReadUInt32();
			if (reserved != 0x0)
				return null;
			var rsa = new RSACryptoServiceProvider();
			var keyType = br.ReadInt32();
			var encrypted = br.ReadUInt32() == 1;
			var saltLength = br.ReadInt32();
			var keyLength = br.ReadInt32();
			byte[] salt = null;
			// If salt is present i.e. key is encrypted then...
			if (saltLength > 0)
				salt = br.ReadBytes(saltLength);
			var keyBlob = br.ReadBytes(keyLength);
			if (saltLength > 0 && encrypted)
			{
				var secretKey = DeriveKey(salt, password);
				// If weak encryption is enabled due to US export restrictions then...
				if (weak)
					// Truncate 128-bit key to 40 bits.
					Array.Clear(secretKey, 5, 11);
				// 8 byte header part of the BLOB is not encrypted.
				Transform(secretKey, keyBlob, 8, keyBlob.Length - 8, keyBlob, 8);
				// Cleanup.
				Array.Clear(salt, 0, salt.Length);
				Array.Clear(secretKey, 0, secretKey.Length);
			}
			rsa.ImportCspBlob(keyBlob);
			// Cleanup key pair, which may include an unencrypted key pair.
			Array.Clear(keyBlob, 0, keyBlob.Length);
			return rsa;
		}

		/// <summary>
		/// Convert RSACryptoServiceProvider to PVK file bytes.
		/// </summary>
		/// <param name="rsa">RSACryptoServiceProvider</param>
		/// <param name="password">Optional password</param>
		/// <param name="weak">Weak encryption option (US export restrictions)</param>
		/// <returns>PVK File bytes</returns>
		public static byte[] Convert(RSACryptoServiceProvider rsa, string password = null, bool weak = false)
		{
			var ms = new MemoryStream();
			var fs = new BinaryWriter(ms);
			int keyType = 2;
			int reserved = 0;
			// header
			byte[] empty = new byte[4];
			fs.Write(_Magic);
			fs.Write(reserved);
			fs.Write(keyType);
			var encrypted = !string.IsNullOrEmpty(password);
			fs.Write(encrypted ? 1 : 0);
			var saltlen = encrypted ? 16 : 0;
			fs.Write(saltlen);
			var keyBlob = rsa.ExportCspBlob(true);
			var keylen = keyBlob.Length;
			fs.Write(keylen);
			if (encrypted)
			{
				var salt = new byte[saltlen];
				// generate new salt (16 bytes)
				var rng = RandomNumberGenerator.Create();
				rng.GetBytes(salt);
				fs.Write(salt);
				var secretKey = DeriveKey(salt, password);
				// If weak encryption is enabled due to US export restrictions then...
				if (weak)
					Array.Clear(secretKey, 5, 11);
				// 8 byte header part of the BLOB is not encrypted.
				Transform(secretKey, keyBlob, 8, keyBlob.Length - 8, keyBlob, 8);
				// Cleanup.
				Array.Clear(salt, 0, salt.Length);
				Array.Clear(secretKey, 0, secretKey.Length);
			}
			fs.Write(keyBlob, 0, keyBlob.Length);
			// Cleanup BLOB, which may include an unencrypted key pair.
			Array.Clear(keyBlob, 0, keyBlob.Length);
			fs.Flush();
			var pvk = ms.ToArray();
			fs.Close();
			return pvk;
		}

		static byte[] DeriveKey(byte[] salt, string password)
		{
			var pwd = Encoding.ASCII.GetBytes(password);
			var sha1 = SHA1.Create();
			sha1.TransformBlock(salt, 0, salt.Length, salt, 0);
			sha1.TransformFinalBlock(pwd, 0, pwd.Length);
			var key = new byte[16];
			Buffer.BlockCopy(sha1.Hash, 0, key, 0, 16);
			sha1.Clear();
			Array.Clear(pwd, 0, pwd.Length);
			return key;
		}

		#region RC4 Encryption/Decryption

		static void Transform(byte[] key, byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			var data = new byte[inputCount];
			Array.Copy(inputBuffer, inputOffset, data, 0, data.Length);
			var output = EncryptOutput(key, data).ToArray();
			Array.Copy(output, 0, outputBuffer, outputOffset, output.Length);
		}

		static byte[] EncryptInitalize(byte[] key)
		{
			byte[] s = Enumerable.Range(0, 256)
			  .Select(i => (byte)i)
			  .ToArray();
			for (int i = 0, j = 0; i < 256; i++)
			{
				j = (j + key[i % key.Length] + s[i]) & 255;
				Swap(s, i, j);
			}
			return s;
		}

		static byte[] EncryptOutput(byte[] key, byte[] data)
		{
			byte[] s = EncryptInitalize(key);
			int i = 0;
			int j = 0;
			return data.Select((b) =>
			{
				i = (i + 1) & 255;
				j = (j + s[i]) & 255;
				Swap(s, i, j);
				return (byte)(b ^ s[(s[i] + s[j]) & 255]);
			}).ToArray();
		}

		static void Swap(byte[] s, int i, int j)
		{
			byte c = s[i];
			s[i] = s[j];
			s[j] = c;
		}

		#endregion
	}
}
