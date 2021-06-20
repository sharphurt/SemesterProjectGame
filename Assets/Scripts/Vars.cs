using System.Collections.Generic;
using System.Linq;
using CarParts;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
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
                    PlayerPrefs.SetString("carParts", JsonConvert.SerializeObject(new List<CarPart>()));
                return JsonConvert.DeserializeObject<List<CarPart>>(PlayerPrefs.GetString("carParts"));
            }
            set
            {
                var s = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString("carParts", s);
                Debug.Log(s);
            }
        }
    }
}