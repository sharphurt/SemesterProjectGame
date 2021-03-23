using System;
using System.Collections.Generic;

namespace LevelData
{
    [Serializable]
    public class WaveData
    {
        public uint repeats;
        public List<WaveElement> waveElements;
    }
}