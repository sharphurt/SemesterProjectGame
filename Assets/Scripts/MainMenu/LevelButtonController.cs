using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class LevelButtonController : MonoBehaviour
    {
        public int levelNumber;
        
        private void Start()
        {
            if (SceneLoader.LastLevel < levelNumber)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
                transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().enabled = false;
            }
        }
    }
}