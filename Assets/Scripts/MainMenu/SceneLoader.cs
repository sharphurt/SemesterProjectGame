using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoading;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, new LoadSceneParameters());
        }

        private void OnSceneLoading(Scene scene, LoadSceneMode sceneMode)
        {
            Debug.Log($"{scene.name} loaded");
        }
    }
}