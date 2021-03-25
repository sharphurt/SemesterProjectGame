using UnityEngine;
using Utils;

namespace LevelData
{
    public class LevelDataLoader : MonoBehaviour
    {
        public LevelData LoadLevelData()
        {
            if (JsonParser.TryParse<LevelData>($"LevelsData/{gameObject.scene.name}", out var data))
                return data;
            Debug.LogError($"Level data isn't specified for level \"{gameObject.scene.name}\"");
            return new LevelData();
        }
    }
}