using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	public class ItemPlayOptions
	{
		public InstalledVoiceEx Voice { get; set; }
		public int Pitch { get; set; }
		public int Rate { get; set; }
		public int Volume { get; set; }
		public MessageGender Gender { get; set; }
		public string Effect { get; set; }

	}
}
