namespace JocysCom.ClassLibrary.Text
{

	/// <summary>
	/// Most Intel and AMD CPUs are little-endian, ARM CPUs can be little-endian or big-endian.
	/// PA-RISC (big-endian, though the processor supports both), PowerPC (big-endian), and SPARC (big-endian).
	/// </summary>
	public enum EncodingType: int
	{
		Auto = 0,
		ASCII = 1,
		/// <summary>UTF-8 is variable 1 to 4 bytes.</summary>
		UTF8 = 2,
		/// <summary>UTF-8 is variable 1 to 4 bytes.</summary>
		UTF8BOM = 3,
		/// <summary>UCS-2 is fixed 2 bytes. U+D800–U+DFFF range is same as in UTF-16.</summary>
		UCS2LE = 4,
		/// <summary>UCS-2 is fixed 2 bytes. U+D800–U+DFFF range is same as in UTF-16.</summary>
		UCS2BE = 5,
		/// <summary>UTF-16 is variable 2 or 4 bytes.</summary>
		UTF16LE = 6,
		/// <summary>UTF-16 is variable 2 or 4 bytes.</summary>
		UTF16BE = 7,
		/// <summary>UTF-32 is fixed 4 bytes. Also known as UCS-4.</summary>
		UTF32LE = 8,
		/// <summary>UTF-32 is fixed 4 bytes. Also known as UCS-4.</summary>
		UTF32BE = 9,
	}
}
