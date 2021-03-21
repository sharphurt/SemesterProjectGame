using System.IO;
using UnityEngine;

public static class JsonLevelParser
{
    public static bool TryParse(string path, out LevelData levelData)
    {
        if (TryGetJsonFile(path, out var json))
        {
            levelData = JsonUtility.FromJson<LevelData>(json);
            return true;
        }

        levelData = new LevelData();
        return false;
    }

    private static bool TryGetJsonFile(string path, out string text)
    {
        try
        {
            text = File.ReadAllText(path);
            return true;
        }
        catch (IOException e)
        {
            Debug.LogException(e);
            text = string.Empty;
            return false;
        }
    }
}