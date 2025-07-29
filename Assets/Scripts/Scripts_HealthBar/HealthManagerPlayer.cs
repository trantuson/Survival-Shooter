using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagerPlayer : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField]
    private int health;
    public int currentHealth;

    public HealthBar healthBar;

    public Animator animator;

    [SerializeField] private ShowSetting showSetting;

    private void Start()
    {
        showSetting = FindObjectOfType<ShowSetting>();
        animator = GetComponent<Animator>();

        playerStats = GetComponent<PlayerStats>();
        health = playerStats.maxHealth;
        playerStats.OnHealthChangedd += HandleHealthChange;

        currentHealth = health;
        healthBar.UpdateBar(currentHealth, health);
    }
    private void HandleHealthChange(int newHealth, int newCurrentHealth)
    {
        health = newHealth;
        currentHealth = newCurrentHealth;
        healthBar.UpdateBar(currentHealth, health);
    }
    public bool TakeDame(int dame)
    {
        currentHealth -= dame;

        if (playerStats != null)
        {
            playerStats.currentHealth = currentHealth;
            healthBar.UpdateBar(currentHealth, health);
        }

        if (currentHealth <= 0)
        {
            Die();
            return false;
        }
        return true;
    }
    private void Die()
    {
        gameObject.SetActive(false);
        showSetting.ShowGameOver();
        Time.timeScale = 0f;
    }

    public IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hit", false);
    }
}
