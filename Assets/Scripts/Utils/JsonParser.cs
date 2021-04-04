using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utils
{
    public static class JsonParser
    {
        public static bool TryParse<T>(string path, out T data)
        {
            if (TryGetJsonFile(path, out var json))
            {
                data = Parse<T>(json);
                return true;
            }

            data = default;
            return false;
        }

        public static T Parse<T>(string json) => JsonUtility.FromJson<T>(json.Replace("\uFEFF", ""));

        public static IEnumerable<TextAsset> GetAllJsonFiles(string path)
        {
            try
            {
                return Resources.LoadAll<TextAsset>(path);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return new List<TextAsset>();
            }
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
}