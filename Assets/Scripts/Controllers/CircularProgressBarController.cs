using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CircularProgressBarController : MonoBehaviour
    {
        public Image progressBar;

        private float currentValue;
        private float reachingHealth;
        private float maxHealth;

        private void Update()
        {
            if (Math.Abs(currentValue - reachingHealth) > 0.0001)
            {
                currentValue = Mathf.Lerp(currentValue, reachingHealth, 10 * Time.deltaTime);
                progressBar.fillAmount = 1f / maxHealth * currentValue;
            }
        }

        public void SetProgressBar(float value, float max)
        {
            reachingHealth = value;
            maxHealth = max;
        }
        
        public void UpdateValue(float value) => reachingHealth = value;
    }
}