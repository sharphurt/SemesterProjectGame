using System;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class WaveElement
    {
        public string prefab;
        public Vector2 position;
        public Vector2 spawnPosition;
        public bool moveByArc;
        public float movingSpeed;
        public float hp;
        public float damage;
        public float spawningDelay;
    }
}