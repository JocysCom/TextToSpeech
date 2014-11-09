using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Audio
{

    [Flags]
    public enum EffectType
    {
        None = 0,
        Chorus = 1,
        Compressor = 2,
        Distortion = 4,
        Echo = 8,
        Flanger = 16,
        Gargle = 32,
        ParamEq = 64,
        Reverb = 128,
        Reverb3D = 256,
    }
}
