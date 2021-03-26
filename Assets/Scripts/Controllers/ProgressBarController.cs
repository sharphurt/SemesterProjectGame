using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ProgressBarController : MonoBehaviour
    {
        public Gradient gradient;
        public Image healthBar;

        private Slider slider;
        private float currentValue;
        private float reachingHealth;
        private float maxHealth;

        private void Awake()
        {
            slider ??= GetComponent<Slider>();
        }

        private void Update()
        {
            if (Math.Abs(currentValue - reachingHealth) > 0.0001)
            {
                currentValue = Mathf.Lerp(currentValue, reachingHealth, 10 * Time.deltaTime);
                slider.value = 1f / maxHealth * currentValue;
                healthBar.color = gradient.Evaluate(slider.normalizedValue);
            }
        }

        public void SetHealthBar(float value, float max, bool isInstantly)
        {
            reachingHealth = value;
            maxHealth = max;
            if (isInstantly)
            {
                currentValue = reachingHealth;
                healthBar.color = gradient.Evaluate(1f / maxHealth * currentValue);
                slider.value = 1f / maxHealth * currentValue;
            }
        }
        
        public void UpdateValue(float value) => reachingHealth = value;
    }
}