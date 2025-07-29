using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    public int dame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthManagerPlayer healthmanagerPlayer = collision.GetComponent<HealthManagerPlayer>();
            if(healthmanagerPlayer != null)
            {
                bool alive = healthmanagerPlayer.TakeDame(dame);
                if (alive == true)
                {
                    healthmanagerPlayer.animator.SetBool("Hit", true);
                    healthmanagerPlayer.StartCoroutine(healthmanagerPlayer.ResetHit());
                }
            }
        }
        else if (collision.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            HeathManager healthmanager = collision.GetComponent<HeathManager>();
            if (healthmanager != null)
            {
                healthmanager.TakeDame(dame);
            }
        }
    }
}
