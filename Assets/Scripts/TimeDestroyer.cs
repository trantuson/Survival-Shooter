using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    public float time;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
