using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeathManager : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int currentHealth;

    [SerializeField] private GameObject expPrefabs;

    Animator animator;

    public bool isDie = false;

    private void Start()
    {
        currentHealth = health;
        expPrefabs = GameObject.Find("Exp");

        animator = GetComponent<Animator>();
    }
    public void TakeDame(int dame)
    {
        if (isDie) return;

        currentHealth -= dame;

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        if (isDie) return;
        isDie = true;
        Instantiate(expPrefabs, transform.position, Quaternion.identity);
        animator.SetBool("Die", true);
        StartCoroutine(DestroyAfterDieAnimation());
    }
    private IEnumerator DestroyAfterDieAnimation()
    {
        // Giả sử animation chết dài 1 giây
        yield return new WaitForSeconds(0.5f); // 0.8f
        gameObject.SetActive(false);
    }
}
