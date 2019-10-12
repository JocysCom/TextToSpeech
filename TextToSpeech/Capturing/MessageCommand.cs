using System.Runtime.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{
	[DataContract]
	public enum MessageCommand
	{
		/// <summary>Add text for the playback.</summary>
		[EnumMember] Add,
		/// <summary>Play all collected information.</summary>
		[EnumMember] Play,
		/// <summary>Stop current playback and clear playback list.</summary>
		[EnumMember] Stop,
		/// <summary>Save NPC name, type and effext.</summary>
		[EnumMember] Save,
		/// <summary>Set PlayerName, PlayerNameChanged, PlayerClass values.</summary>
		[EnumMember] Player,
	}
}
