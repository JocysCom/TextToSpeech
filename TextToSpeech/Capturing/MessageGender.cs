using System.Runtime.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	[DataContract]
	public enum MessageGender
	{
		[EnumMember] Male,
		[EnumMember] Female,
		[EnumMember] Neutral,
	}
}
