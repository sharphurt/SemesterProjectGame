using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Entities;
using Items;
using LevelData;
using LevelData.LootTable;
using MainMenu;
using Spawning;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameManager : MonoBehaviour
{
    public static Dictionary<string, Enemy> EnemyPrefabs { get; private set; }
    public static Dictionary<string, Item> ItemPrefabs { get; private set; }

    public static LevelEndPoint LevelEndPointPrefab { get; private set; }

    public static LevelData.LevelData LevelData { get; private set; }
    public static Dictionary<string, LootTable> LootTables { get; private set; }

    public static float MovementSpeed;
    public float levelMovementSpeed;

    private Player player;
    private EntitySpawner entitySpawner;
    public Text scoreText;

    private bool isFinishing;

    public int Score
    {
        get => Vars.Coins;
        set
        {
            Vars.Coins = value;
            scoreText.text = value.ToString();
        }
    }

    private void Start()
    {
        scoreText.text = Score.ToString();
        
        Application.targetFrameRate = 300;

        MovementSpeed = levelMovementSpeed;

        LevelData = LevelDataLoader.LoadLevelData(gameObject.scene.name);
        LootTables = LootTableLoader.LoadLootTables();

        entitySpawner = GetComponent<EntitySpawner>();
        player = FindObjectOfType<Player>();

        EnemyPrefabs = LoadEnemyPrefabs();
        ItemPrefabs = LoadItems();

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

    private Dictionary<string, Item> LoadBoosters() =>
        LootTables.Values
            .SelectMany(t => t.boosters)
            .GroupBy(b => b.name)
            .Select(n => n.First().name)
            .ToDictionary(k => k, v => Resources.Load<Item>($"Prefabs/Boosters/{v}"));

    private (string, Item) LoadPartsItem() => ("Part", Resources.Load<Item>("Prefabs/Parts/Part"));

    private Dictionary<string, Item> LoadItems()
    {
        var boosters = LoadBoosters();
        var (name, item) = LoadPartsItem();
        boosters.Add(name, item);
        return boosters;
    }

    private void PlayerDeathHandler(string killer) => SceneManager.LoadScene("MainMenu");

    private void WavesAreOverHandler() => StartCoroutine(EndLevelCoroutine());

    private void Update()
    {
        if (isFinishing)
            MovementSpeed = Mathf.Lerp(MovementSpeed, 0, 0.01f);
    }

    private IEnumerator EndLevelCoroutine()
    {
        yield return new WaitForSeconds(LevelData.levelEnd.delay);
        var instance = Instantiate(LevelEndPointPrefab, LevelData.levelEnd.spawnPosition, Quaternion.identity);
        instance.OnGameWin += GameWinHandler;
        player.DisableJoystickControl();
        player.MoveTo(instance.GetComponentInChildren<Transform>().position, 0.1f, true);
        isFinishing = true;
    }

    private void GameWinHandler()
    {
        if (SceneLoader.CurrentLevel == Vars.LastLevel)
        {
            Vars.LastLevel = SceneLoader.CurrentLevel + 1;
        }
        SceneManager.LoadScene("MainMenu");
    }
}