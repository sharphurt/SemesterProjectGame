using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[Serializable]
public class WaveData
{
    public uint repeats;
    public float spawningDelay;
    public List<WaveElement> waveElements;
}