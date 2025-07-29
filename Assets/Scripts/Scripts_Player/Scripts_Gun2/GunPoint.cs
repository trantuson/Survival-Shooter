using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    public Transform gunPoint;

    private Vector3 offset;

    private void Start()
    {
        if (gunPoint != null)
        {
            offset = transform.position - gunPoint.position;
        }
    }

    private void Update()
    {
        if (gunPoint != null)
        {
            transform.position = gunPoint.position + offset;
        }
    }
}
