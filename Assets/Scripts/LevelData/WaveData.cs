using System;
using System.Collections.Generic;

namespace LevelData
{
    [Serializable]
    public class WaveData
    {
        public uint repeats;
        public float spawningDelay;
        public List<WaveElement> waveElements;
    }
}