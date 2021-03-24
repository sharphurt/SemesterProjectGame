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
        public static LevelEndPoint LevelEndPointPrefab { get; private set; }

        public static LevelData.LevelData LevelData { get; private set; }

        public static float LevelMovementSpeed = 2;

        private Player player;
        private EntitySpawner entitySpawner;

        private bool isFinishing;
        
        private void Start()
        {
            Application.targetFrameRate = 300;
            
            LevelData = GetComponent<LevelDataLoader>().LoadLevelData();
            entitySpawner = GetComponent<EntitySpawner>();
            player = FindObjectOfType<Player>();

            EnemyPrefabs = LoadEnemyPrefabs();
            LevelEndPointPrefab = Resources.Load<LevelEndPoint>($"Prefabs/LevelEndPoints/{LevelData.levelEnd.prefab}");

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

        private void Update()
        {
            if (isFinishing)
                LevelMovementSpeed = Mathf.Lerp(LevelMovementSpeed, 0, 0.01f);
        }

        private IEnumerator EndLevelCoroutine()
        {
            yield return new WaitForSeconds(LevelData.levelEnd.delay);
            isFinishing = true;
            var instance = Instantiate(LevelEndPointPrefab, LevelData.levelEnd.spawnPosition, Quaternion.identity);
            instance.OnGameWin += GameWinHandler;
        }

        private void GameWinHandler()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}