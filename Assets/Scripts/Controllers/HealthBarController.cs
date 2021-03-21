using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    private int currentValue;
    private int reachingHealth;
    private int maxHealth;

    private void Update()
    {
        if (currentValue != reachingHealth)
        {
            currentValue = (int) Math.Round(Mathf.Lerp(currentValue, reachingHealth, 1.5f * Time.deltaTime));
            healthBar.fillAmount = 1f / maxHealth * currentValue;
        }
    }

    public void SetHealthBar(int value, int max, bool isInstantly)
    {
        reachingHealth = value;
        maxHealth = max;
        if (isInstantly)
            currentValue = reachingHealth;
    }
}