namespace JocysCom.TextToSpeech.Monitor.Audio
{
    public enum JobStatusType
    {
        /// <summary>New Item. Play item contaisns text only.</summary>
        None = 0,
        /// <summary>Play item 'Text' was parsed to SAPI XML.</summary>
        Parsed = 2,
        /// <summary>Converting SAPI XML to Wav Data in progress...</summary>
        Synthesizing  = 3,
        /// <summary>Play item SAPI XML was converted to WavData.</summary>
        Synthesized = 4,
        /// <summary>Apply pitch to Wav Data in progress...</summary>
        Pitching = 5,
        /// <summary>Applying pitch Wav Data finished.</summary>
        Pitched = 6,
        /// <summary>Playing WAV.</summary>
        Playing = 7,
        /// <summary>WAV plaing finished.</summary>
        Played = 8,
        /// <summary>Error happend.</summary>
        Error = 10,
    }
}
