using System.Runtime.Serialization;

namespace LevelData
{
    public enum LocationMethod
    {
        [EnumMember(Value = "random")] Random,
        [EnumMember(Value = "specified")] Specified
    }
}