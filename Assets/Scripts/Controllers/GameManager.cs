using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using LevelData;
using Spawning;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameManager : MonoBehaviour
    {
        public static Dictionary<string, Enemy> Prefabs { get; private set; }

        public static LevelData.LevelData LevelData { get; private set; }

        private Player player;
        private EntitySpawner entitySpawner;

        private void Start()
        {
            LevelData = GetComponent<LevelDataLoader>().LoadLevelData();
            entitySpawner = GetComponent<EntitySpawner>();
            player = FindObjectOfType<Player>();
            
            Prefabs = PreparePrefabs();

            player.OnPlayerDeath += PlayerDeathHandler;
            entitySpawner.OnWavesOver += WavesAreOverHandler;

            entitySpawner.SpawnWaves();
        }

        private Dictionary<string, Enemy> PreparePrefabs() =>
            LevelData.waves
                .SelectMany(s => s.waveElements)
                .GroupBy(e => e.enemy)
                .Select(g => g.First().enemy)
                .ToDictionary(k => k, v => Resources.Load<Enemy>($"Prefabs/Enemies/{v}"));

        private void PlayerDeathHandler(string killer)
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void WavesAreOverHandler()
        {
            Debug.Log("Waves are over");
        }
    }
}