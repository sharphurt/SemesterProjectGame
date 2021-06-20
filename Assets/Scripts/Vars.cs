using System.Collections.Generic;
using CarParts;
using UnityEngine;
using Utils;

public class Vars
{
    public static int LastLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("lastLevel"))
                PlayerPrefs.SetInt("lastLevel", 1);
                
            return PlayerPrefs.GetInt("lastLevel");
        }

        set => PlayerPrefs.SetInt("lastLevel", value);
    }
        
    public static int Coins
    {
        get
        {
            if (!PlayerPrefs.HasKey("coins"))
                PlayerPrefs.SetInt("coins", 0);

            return PlayerPrefs.GetInt("coins");
        }

        set => PlayerPrefs.SetInt("coins", value);
    }

    public static List<CarPart> CarParts
    {
        get
        {
            if (!PlayerPrefs.HasKey("carParts"))
                PlayerPrefs.SetString("carParts", "[]");
            return JsonParser.Parse<List<CarPart>>(PlayerPrefs.GetString("carParts"));
        }
        set
        {
            PlayerPrefs.SetString("carParts", JsonUtility.ToJson(value));
            Debug.Log(PlayerPrefs.GetString("carParts"));
        }
    }
}