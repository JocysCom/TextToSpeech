using System;

namespace SoundStretch
{
	public class RunParameters
	{

		/// <summary>Change sound tempo by n percents  (n=-95..+5000 %)</summary>
		public float TempoDelta { get { return _TempoDelta; } set { _TempoDelta = Math.Min(Math.Max(value, -95f), 5000f); } }
		float _TempoDelta;

		/// <summary>Change sound pitch by n semitones(n= -60..+60 semitones)</summary>
		public float PitchDelta { get { return _PitchDelta; } set { _PitchDelta = Math.Min(Math.Max(value, -60f), 60f); } }
		float _PitchDelta;

		/// <summary>Change sound rate by n percents   (n=-95..+5000 %)</summary>
		public float RateDelta { get { return _RateDelta; } set { _RateDelta = Math.Min(Math.Max(value, -95f), 5000f); } }
		float _RateDelta;

		/// <summary>
		/// Detect the BPM rate of sound."
		/// </summary>
		public bool DetectBpm { get; set; }

		/// <summary>Adjust tempo to meet BPMs.</summary>
		public float GoalBpm { get; set; }

		/// <summary>Use quicker tempo change algorithm (gain speed, lose quality)</summary>
		public int Quick { get; set; }

		/// <summary>Don't use anti-alias filtering (gain speed, lose quality)</summary>
		public int NoAntiAlias { get; set; }

		/// <summary>Tune algorithm for speech processing (default is for music).</summary>
		public bool Speech { get; set; }

		public RunParameters()
		{
			TempoDelta = 0;
			PitchDelta = 0;
			RateDelta = 0;
			Quick = 0;
			NoAntiAlias = 0;
			GoalBpm = 0;
			Speech = false;
			DetectBpm = false;
		}
	}
}