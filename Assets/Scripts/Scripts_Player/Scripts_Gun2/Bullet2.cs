using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Transform target;
    public int dame;

    public void SetTarget(Transform target, int dame)
    {
        this.target = target;
        this.dame = dame;

    }
    private void Update()
    {
        BulletMoveToTarget();
    }
    public void BulletMoveToTarget()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        Collider2D collider = target.GetComponent<Collider2D>();
        if (collider == null || !collider.enabled)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HeathManager healManager = collision.GetComponent<HeathManager>();
            if (healManager != null)
            {
                healManager.TakeDame(dame);
            }
            gameObject.SetActive(false);
        }
    }
}
