using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthBarController : MonoBehaviour
    {
        public Gradient gradient;
        public Image healthBar;

        private Slider slider;
        private float currentValue;
        private int reachingHealth;
        private int maxHealth;

        private void Update()
        {
            if (Math.Abs(currentValue - reachingHealth) > 0.0001)
            {
                currentValue = Mathf.Lerp(currentValue, reachingHealth, 10 * Time.deltaTime);
                slider.value = 1f / maxHealth * currentValue;
                healthBar.color = gradient.Evaluate(slider.normalizedValue);
            }
        }

        public void SetHealthBar(int value, int max, bool isInstantly)
        {
            slider ??= GetComponent<Slider>();

            reachingHealth = value;
            maxHealth = max;
            if (isInstantly)
            {
                currentValue = reachingHealth;
                healthBar.color = gradient.Evaluate(1f / maxHealth * currentValue);
                slider.value = 1f / maxHealth * currentValue;
            }
        }
    }
}