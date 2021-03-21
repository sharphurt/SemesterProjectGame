using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    [HideInInspector] public LevelData levelData;

    private void Start() => LoadLevelData();

    private void LoadLevelData()
    {
        if (JsonParser.TryParse<LevelData>($"LevelsData/{gameObject.scene.name}", out var data))
            levelData = data;
        else
        {
            Debug.LogError($"Level data isn't specified for level \"{gameObject.scene.name}\"");
            levelData = new LevelData();
        }
    }
}