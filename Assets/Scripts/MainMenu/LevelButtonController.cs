using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class LevelButtonController : MonoBehaviour
    {
        public int levelNumber;
        
        private void Start()
        {
            if (Vars.LastLevel < levelNumber)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
                transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().enabled = false;
            }
        }
    }
}