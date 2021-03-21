using System.Runtime.Serialization;

public enum LocationMethod
{
    [EnumMember(Value = "random")] Random,
    [EnumMember(Value = "specified")] Specified
}