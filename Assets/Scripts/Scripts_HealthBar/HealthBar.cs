using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(int currentHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        valueText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
