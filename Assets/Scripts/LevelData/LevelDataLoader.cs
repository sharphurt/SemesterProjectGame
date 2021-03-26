using UnityEngine;
using Utils;

namespace LevelData
{
    public static class LevelDataLoader
    {
        public static LevelData LoadLevelData(string sceneName)
        {
            if (JsonParser.TryParse<LevelData>($"LevelsData/{sceneName}", out var data))
                return data;
            Debug.LogError($"Level data isn't specified for level \"{sceneName}\"");
            return new LevelData();
        }
    }
}