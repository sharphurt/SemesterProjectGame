using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        public GameObject loadingObject;

        public static int LastLevel
        {
            get
            {
                if (PlayerPrefs.HasKey("lastLevel"))
                {
                    return PlayerPrefs.GetInt("lastLevel");
                }
                else
                {
                    PlayerPrefs.SetInt("lastLevel", 1);
                    return 1;
                }
            }
        }

        public static int CurrentLevel;
    
        private void Start()
        {
            
            SceneManager.sceneLoaded += OnSceneLoading;
        }

        public void LoadScene(int levelNumber)
        {
            loadingObject.SetActive(true);
            SceneManager.LoadScene($"Level{levelNumber}", new LoadSceneParameters());
            CurrentLevel = levelNumber;
        }

        private void OnSceneLoading(Scene scene, LoadSceneMode sceneMode)
        {
            loadingObject.SetActive(false);
            Debug.Log($"{scene.name} loaded");
        }
    }
}