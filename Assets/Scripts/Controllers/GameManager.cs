using System;
using System.Collections;
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
        public static Dictionary<string, Enemy> EnemyPrefabs { get; private set; }
        public static LevelEndPoint LevelEndPoint { get; private set; }

        public static LevelData.LevelData LevelData { get; private set; }

        private Player player;
        private EntitySpawner entitySpawner;
        private VerticalBackgroundLoopController backgroundDriver;

        private void Start()
        {
            LevelData = GetComponent<LevelDataLoader>().LoadLevelData();
            entitySpawner = GetComponent<EntitySpawner>();
            player = FindObjectOfType<Player>();
            backgroundDriver = FindObjectOfType<VerticalBackgroundLoopController>();

            EnemyPrefabs = LoadEnemyPrefabs();
            LevelEndPoint = Resources.Load<LevelEndPoint>($"Prefabs/LevelEndPoints/{LevelData.levelEnd.prefab}");

            player.OnPlayerDeath += PlayerDeathHandler;
            entitySpawner.OnWavesOver += WavesAreOverHandler;

            entitySpawner.SpawnWaves();
        }

        private Dictionary<string, Enemy> LoadEnemyPrefabs() =>
            LevelData.waves
                .SelectMany(s => s.waveElements)
                .GroupBy(e => e.prefab)
                .Select(g => g.First().prefab)
                .ToDictionary(k => k, v => Resources.Load<Enemy>($"Prefabs/Enemies/{v}"));

        private void PlayerDeathHandler(string killer)
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void WavesAreOverHandler()
        {
            StartCoroutine(EndLevelCoroutine());
        }

        private IEnumerator EndLevelCoroutine()
        {
            yield return new WaitForSeconds(LevelData.levelEnd.delay);
            var instance = Instantiate(LevelEndPoint, LevelData.levelEnd.spawnPosition, Quaternion.identity);
            instance.OnGameWin += GameWinHandler;
            backgroundDriver.StopSmoothly();
            instance.MoveTo(backgroundDriver.scrollSpeed);
        }

        private void GameWinHandler()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}