using System.Runtime.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Google
{
	[DataContract]
	public enum SsmlVoiceGender
	{
		/// <summary>
		/// An unspecified gender.In VoiceSelectionParams, this means that the client doesn't care which gender the selected voice will have. In the Voice field of ListVoicesResponse, this may mean that the voice doesn't fit any of the other categories in this enum, or that the gender of the voice isn't known.
		/// </summary>
		[EnumMember] SSML_VOICE_GENDER_UNSPECIFIED,
		/// <summary>A male voice.</summary>
		[EnumMember] MALE,
		/// <summary>A female voice.</summary>
		[EnumMember] FEMALE,
		/// <summary>A gender-neutral voice.</summary>
		[EnumMember] NEUTRAL,
	}
}
