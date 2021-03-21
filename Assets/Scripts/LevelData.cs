using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public List<WaveData> wavesData;

    public override string ToString() => JsonUtility.ToJson(this);
}