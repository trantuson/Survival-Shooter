using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExpBar expBar = collision.GetComponent<ExpBar>();
            if (expBar != null)
            {
                expBar.ExpPlus(2);
            }
            Destroy(gameObject);
        }
    }
}
