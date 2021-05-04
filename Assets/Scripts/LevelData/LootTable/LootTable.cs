using System;
using System.Collections.Generic;

namespace LevelData.LootTable
{
    [Serializable]
    public class LootTable
    {
        public List<BoosterData> boosters;
        public Dictionary<string, int> partsMaxCount;
    }
}