using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class LevelData
    {
        public LevelEnding levelEnd;
        public List<WaveData> waves;

        public override string ToString() => JsonUtility.ToJson(this);
    }
}