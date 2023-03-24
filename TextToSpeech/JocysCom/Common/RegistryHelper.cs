#if NETSTANDARD // .NET Standard
#else // .NET Framework

using Microsoft.Win32;
using System;

namespace JocysCom.ClassLibrary
{
	public static class RegistryHelper
	{
		/// <summary>
		///  Moves a specified key to a new location.
		/// </summary>
		/// <param name="parentKey">The registry key that contains the subkey you want to rename.</param>
		/// <param name="sourceSubKey">The name of the subkey to move.</param>
		/// <param name="targetSubKey">The new name for the subkey.</param>
		/// <returns>True if succeeds</returns>
		public static bool Move(RegistryKey parentKey, string sourceSubKey, string targetSubKey)
		{
			if (parentKey == null)
				throw new ArgumentNullException(nameof(parentKey));
			Copy(parentKey, sourceSubKey, targetSubKey, true);
			parentKey.DeleteSubKeyTree(sourceSubKey);
			return true;
		}

		/// <summary>
		/// Copies an existing subkey to a new subkey.
		/// </summary>
		/// <param name="parentKey">The registry key that contains the subkey you want to copy.</param>
		/// <param name="sourceKeyName">The subkey to copy.</param>
		/// <param name="targetKeyName">The name of the destination subkey.</param>
		/// <returns></returns>
		public static bool Copy(RegistryKey parentKey, string sourceKeyName, string targetKeyName, bool recursive = true)
		{
			if (parentKey == null)
				throw new ArgumentNullException(nameof(parentKey));
			var sourceKey = parentKey.OpenSubKey(sourceKeyName);
			var targetKey = parentKey.CreateSubKey(targetKeyName);
			Copy(sourceKey, targetKey, recursive);
			sourceKey.Close();
			targetKey.Close();
			return true;
		}

		/// <summary>
		/// Copies an existing key to a new key.
		/// </summary>
		/// <param name="sourceKey">The key to copy.</param>
		/// <param name="targetKey">The name of the destination key.</param>
		static void Copy(RegistryKey sourceKey, RegistryKey targetKey, bool recursive = true)
		{
			// For each value...
			foreach (string valueName in sourceKey.GetValueNames())
			{
				// Copy value.
				var value = sourceKey.GetValue(valueName);
				var valueKind = sourceKey.GetValueKind(valueName);
				targetKey.SetValue(valueName, value, valueKind);
			}
			if (!recursive)
				return;
			// For each subKey...
			foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
			{
				// Create a new subKey in destinationKey 
				var sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName);
				var targetSubKey = targetKey.CreateSubKey(sourceSubKeyName);
				Copy(sourceSubKey, targetSubKey, recursive);
				sourceSubKey.Close();
				targetSubKey.Close();
			}
		}
	}
}
#endif

