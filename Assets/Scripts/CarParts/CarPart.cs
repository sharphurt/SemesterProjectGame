using System;

namespace CarParts
{
    [Serializable]
    public class CarPart
    {
        public string Name { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public string PrefabName { get; set; }
        public PartType PartType { get; set; }
        public float Chance { get; set; }
        public float ImprovementValue { get; set; }
    }
}