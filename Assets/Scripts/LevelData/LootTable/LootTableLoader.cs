using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace LevelData.LootTable
{
    public static class LootTableLoader
    {
        public static Dictionary<string, LootTable> LoadLootTables() =>
            JsonParser.GetAllJsonFiles("LootTables/").ToDictionary(f => f.name, f => JsonParser.Parse<LootTable>(f.text));
    }
}