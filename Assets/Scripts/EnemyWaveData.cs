using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


[Serializable]
public class EnemyWaveData
{
    public uint repeats;
    public List<WaveElement> waveElements;
}