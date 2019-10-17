using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
	/// <remarks>
	/// Test Text:
	/// This is my fault. I. I ignored his warnings, his complaints, everything. I couldn't help myself - I saw those Scourge just beyond the tower, and 
	/// I. I HAD to go kill them. Argus says that Gidwin was taken northwest, and that's our only lead, so that's where we'll head. Northdale and the 
	/// Argent Crusade be damned! We're getting Gidwin back from those Scourge trash. Your objective is to Hop in Fiona's Caravan and ride to Northpass 
	/// Tower. You will be rewarded!
	/// </remarks>
	public class PlayItem : INotifyPropertyChanged, IDisposable
	{

		System.Timers.Timer playTimer;

		public PlayItem()
		{
		}

		/// <summary>Game Name.</summary>
		[DefaultValue(null)]
		public string Game
		{
			get { return _Game; }
			set { _Game = value; OnPropertyChanged(); }
		}
		string _Game;

		/// <summary>NPC Name.</summary>
		[DefaultValue(null)]
		public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
		string _Name;

		/// <summary>NPC Gender.</summary>
		[DefaultValue(null)]
		public MessageGender Gender { get { return _Gender; } set { _Gender = value; OnPropertyChanged(); } }
		MessageGender _Gender;

		/// <summary>Volume.</summary>
		[DefaultValue(100)]
		public int Volume { get { return _Volume; } set { _Volume = value; OnPropertyChanged(); } }
		int _Volume;

		/// <summary>NPC Effect.</summary>
		[DefaultValue(null)]
		public string Effect
		{
			get { return _Effect; }
			set { _Effect = value; OnPropertyChanged(); }
		}
		string _Effect;

		public string PlayerName { get; set; }
		public string PlayerNameChanged { get; set; }
		public string PlayerClass { get; set; }

		public string VoiceSourceKeys { get; set; }

		/// <summary>
		/// Generic means with player name and class replaced so it will be usable for all names and classes.
		/// </summary>
		/// <param name="generalize">Required when quest is manually voiced and generalized to work for all names and classes</param>
		/// <returns></returns>
		public string GetUniqueFilePath(bool generalize = false)
		{
			var gamePath = JocysCom.ClassLibrary.Text.Filters.GetKey(Game, false);
			var charPath = "Data";
			string fileName;
			var encoding = System.Text.Encoding.UTF8;
			// If data then...
			if (string.IsNullOrEmpty(Name))
			{
				var bytes = encoding.GetBytes(Xml);
				var hash = JocysCom.ClassLibrary.Security.MD5Helper.GetGuid(bytes);
				fileName = string.Format("{0:N}", hash);
			}
			else
			{
				charPath = JocysCom.ClassLibrary.Text.Filters.GetKey(string.Format("{0}_{1}_{2}", Name, Gender, Effect ?? ""), false);
				// Generalize text if needed.
				var text = generalize ? GetGeneralizedText() : Text;
				fileName = JocysCom.ClassLibrary.Text.Filters.GetKey(text, false);
				// If file name will be short then...
				if (fileName.Length >= 64)
				{
					var bytes = encoding.GetBytes(fileName);
					var hash = ClassLibrary.Security.CRC32Helper.GetHashAsString(bytes);
					// Return trimmed name with hash.
					fileName = string.Format("{0}_{1}", fileName.Substring(0, 64), hash);
				}
			}
			return string.Format("{0}\\{1}\\{2}\\{3}", gamePath, Group, charPath, fileName);
		}

		public string GetGeneralizedText()
		{
			var text = Text;
			if (!string.IsNullOrEmpty(PlayerName))
				text = text.Replace(PlayerName, "Traveler");
			if (!string.IsNullOrEmpty(PlayerNameChanged))
				text = text.Replace(PlayerNameChanged, "Traveler");
			if (!string.IsNullOrEmpty(PlayerClass))
				text = text.Replace(PlayerClass, "Traveler");
			return text;
		}


		public void StartPlayTimer()
		{
			if (IsDisposing) return;
			// Set up timer.
			_Status = JobStatusType.Playing;
			OnPropertyChanged(nameof(Status));
			playTimer = new System.Timers.Timer();
			playTimer.AutoReset = false;
			playTimer.Elapsed += playTimer_Elapsed;
			playTimer.Interval = Duration;
			playTimer.Start();
		}

		void playTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_Status = JobStatusType.Played;
			OnPropertyChanged(nameof(Status));
		}

		public bool IsComment
		{
			get { return _IsComment; }
			set { _IsComment = value; OnPropertyChanged(); }
		}
		bool _IsComment;

		public string Text
		{
			get { return _Text; }
			set { _Text = value; OnPropertyChanged(); }
		}
		string _Text;

		public string Group
		{
			get { return _Group; }
			set { _Group = value; OnPropertyChanged(); }
		}
		string _Group;

		public string Xml
		{
			get { return _Xml; }
			set { _Xml = value; OnPropertyChanged(); }
		}
		string _Xml;

		/// <summary>
		/// Contains information about the WAV Data.
		/// </summary>
		public SharpDX.Multimedia.WaveFormat WavHead
		{
			get { return _WavHead; }
			set { _WavHead = value; OnPropertyChanged(); }
		}
		SharpDX.Multimedia.WaveFormat _WavHead;

		/// <summary>
		/// Contains WAV data for processing.
		/// </summary>
		public byte[] WavData
		{
			get { return _WavData; }
			set { _WavData = value; OnPropertyChanged(); }
		}
		byte[] _WavData;

		public Stream StreamData
		{
			get { return _StreamData; }
			set { _StreamData = value; OnPropertyChanged(); }
		}
		Stream _StreamData;

		public int Duration
		{
			get { return _Duration; }
			set { _Duration = value; OnPropertyChanged(); }
		}
		int _Duration;

		public JobStatusType Status
		{
			get { return _Status; }
			set { _Status = value; OnPropertyChanged(); }
		}
		JobStatusType _Status;

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;
			ControlsHelper.Invoke(() => { handler(this, new PropertyChangedEventArgs(propertyName)); });
		}

		#endregion

		#region IDisposable

		bool IsDisposing;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected virtual void Dispose(bool disposing)
		{
			IsDisposing = true;
			if (disposing)
			{
				if (playTimer != null)
				{
					playTimer.Dispose();
					playTimer = null;
				}
				if (StreamData != null)
				{
					StreamData.Close();
					StreamData = null;
				}
			}
		}

		#endregion

	}
}
