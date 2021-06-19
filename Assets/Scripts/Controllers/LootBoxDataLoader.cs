using System;
using System.Collections.Generic;
using System.Linq;
using CarParts;
using Utils;

namespace Controllers
{
    public static class LootBoxDataLoader
    {
        public static List<CarPartData> Load()
        {
            return JsonParser.GetAllJsonFiles("LootTables/LootBoxLootTables")
                .Select(e => JsonParser.Parse<CarPartData>(e.text)).ToList();
        }
    }
}