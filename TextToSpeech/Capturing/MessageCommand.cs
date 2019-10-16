using System.Runtime.Serialization;

namespace JocysCom.TextToSpeech.Monitor.Capturing
{

	/// <summary>Use lowercase.</summary>
	public static class MessageCommands
	{
		/// <summary>Add text for the playback.</summary>
		public const string Add = "add";
		/// <summary>Play all collected information.</summary>
		public const string Play = "play";
		/// <summary>Stop current playback and clear playback list.</summary>
		public const string Stop = "stop";
		/// <summary>Save NPC name, type and effect.</summary>
		public const string Save = "save";
		/// <summary>Set PlayerName, PlayerNameChanged, PlayerClass values.</summary>
		public const string Player = "player";
	}
}
