namespace JocysCom.TextToSpeech.Monitor.Audio
{
    public enum JobStatusType
    {
        None = 0,
        Parsed = 2,
        Synthesizing  = 3,
        Synthesized = 4,
        Pitching = 5,
        Pitched = 6,
        Playing = 7,
        Played = 8,
        Error = 10,
    }
}
