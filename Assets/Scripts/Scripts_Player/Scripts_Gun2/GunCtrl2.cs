using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl2 : MonoBehaviour
{
    public List<Transform> gunTransforms = new List<Transform>();

    public void RotateTowards(Transform target)
    {
        if (target == null) return;

        foreach (Transform gunTransform in gunTransforms)
        {
            if(gunTransform == null) continue;

            Vector3 direction = target.position - gunTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (angle > 90 || angle < -90)
            {
                gunTransform.localScale = new Vector3(0.4f, -0.4f, 1); // Lật ngược
            }
            else
            {
                gunTransform.localScale = new Vector3(0.4f, 0.4f, 1); // Bình thường
            }
        }
    }
}
