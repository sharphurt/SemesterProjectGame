using System.IO;
using UnityEngine;

public static class JsonParser
{
    public static bool TryParse<T>(string path, out T levelData)
    {
        if (TryGetJsonFile(path, out var json))
        {
            levelData = JsonUtility.FromJson<T>(json.Replace("\uFEFF", ""));
            return true;
        }

        levelData = default;
        return false;
    }

    private static bool TryGetJsonFile(string path, out string text)
    {
        try
        {
            text = Resources.Load<TextAsset>(path).text;
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