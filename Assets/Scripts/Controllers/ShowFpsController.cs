using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ShowFpsController : MonoBehaviour
    {
        private Text fpsText;
        public float hudRefreshRate = 0.5f;
        
        private float timer;

        private void Start() => fpsText = GetComponent<Text>();

        private void Update()
        {
            if (Time.unscaledTime <= timer)
                return;
            
            var fps = (int) (1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}