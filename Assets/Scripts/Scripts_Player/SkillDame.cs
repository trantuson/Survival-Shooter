using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDame : MonoBehaviour
{
    private int damage = 5;

    public void SetDamage(int value)
    {
        damage = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HeathManager heathManager = collision.GetComponent<HeathManager>();
            if (heathManager != null)
            {
                heathManager.TakeDame(damage);
            }
        }
    }
}
