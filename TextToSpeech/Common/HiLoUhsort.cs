using System.Runtime.InteropServices;

namespace JocysCom.WoW.TextToSpeech
{
	[StructLayout(LayoutKind.Explicit)]
	public struct HiLoUshort
	{
		[FieldOffset(0)]
		public ushort Number;
		[FieldOffset(0)]
		public byte Low;
		[FieldOffset(1)]
		public byte High;
	}
}
