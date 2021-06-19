using UnityEngine;

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
    }
}