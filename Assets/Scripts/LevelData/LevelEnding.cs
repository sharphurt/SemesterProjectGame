using System;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class LevelEnding
    {
        public float delay;
        public string prefab;
        public Vector2 spawnPosition;
        public Vector2 targetPosition;
    }
}