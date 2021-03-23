using System;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class WaveElement
    {
        public string prefab;
        public LocationMethod locationMethod;
        public Vector2 position;
        public float spawningDelay;
    }
}