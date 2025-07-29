using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossMoveAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private AIDestinationSetter setter;
    private AIPath aIPath;

    Animator animator;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject stompPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform firePoint;
    private bool isAttacking = false; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();   

        SetTargetSetter();
    }
    private void Update()
    {
        DistanceTarget();
    }
    protected void SetTargetSetter()
    {
        if(player != null && setter != null)
        {
            setter.target = player.transform;
        }
    }
    protected void DistanceTarget()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 4f)
        {
            aIPath.canMove = false;
            setter.target = null;
            if (!isAttacking)
            {
                StartCoroutine(TimeFallAttack());
            }
        }
        else
        {
            aIPath.canMove = true;
            setter.target = player.transform;
            //animator.SetBool("Fall", false);
        }
    }

    private IEnumerator TimeFallAttack() // khoảng cách để tấn công
    {
        isAttacking = true;
        while (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            // bắn đạn
            yield return StartCoroutine(ShootBurst());

            // skill dậm chân
            animator.SetBool("Fall", true);
            StompAttack();
            yield return new WaitForSeconds(1f);
        }
        isAttacking = false;
    }
    private IEnumerator ShootBurst() // đạn 3 viên xong dừng 1f
    {
        for (int i = 0; i < 3; i++)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
    }
    protected void ShootAtPlayer() // đạn theo player
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
    protected void StompAttack() // skill dậm chân 4 hướng
    {
        //Vector2[] direction = new Vector2[]
        //{
        //    new Vector2(1,1).normalized,
        //    new Vector2(-1,1).normalized,
        //    new Vector2(1,-1).normalized,
        //    new Vector2(-1,-1).normalized,
        //};
        Vector2[] direction = GetEvenDirections(7);
        foreach (var dir in direction)
        {
            GameObject wave = Instantiate(stompPrefab, firePoint.position, Quaternion.identity);
            wave.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        }
    }
    private Vector2[] GetEvenDirections(int count) // skill dậm chân nhiều hướng
    {
        Vector2[] directions = new Vector2[count];
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;
            directions[i] = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }
        return directions;
    }
    public void ResetFall() // animation Event
    {
        animator.SetBool("Fall", false);
    }
}
