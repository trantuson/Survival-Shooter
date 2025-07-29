using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpClip : MonoBehaviour
{
    AudioManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.PlaySfx(manager.expClip);
        }
    }
}
