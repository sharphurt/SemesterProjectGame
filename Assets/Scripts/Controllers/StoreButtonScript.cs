using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class StoreButtonScript : MonoBehaviour
    {
        private void Start()
        {
            
        }

        public void OpenStoreScene()
        {
            SceneManager.LoadScene("PartsStore");
        }
    }
}
