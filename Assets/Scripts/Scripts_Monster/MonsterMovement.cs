using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Animator animator;

    private AIDestinationSetter destinationSetter;


    [SerializeField] private float minDistanceToPlayer = 1f;
    [SerializeField] private float avoidRadiusNear = 0f;
    [SerializeField] private float avoidRadiusFar = 0.2f;
    [SerializeField] private LayerMask enemyLayer;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        SetTarget();
    }
    private void Update()
    {
        if(player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance > 1f)
            {
                float avoidRadius = distance <= minDistanceToPlayer ? avoidRadiusNear : avoidRadiusFar;
                AvoidOtherEnemies(avoidRadius);

            }
        }
    }
    private void SetTarget()
    {
        if (player != null && destinationSetter != null)
        {
            destinationSetter.target = player.transform;
        }
        
    }
    void AvoidOtherEnemies(float radius)
    {
        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        foreach (var other in others)
        {
            if (other.gameObject != this.gameObject)
            {
                Vector2 pushDir = (transform.position - other.transform.position).normalized;
                transform.position += (Vector3)(pushDir * 0.01f); // đẩy nhẹ ra
            }
        }
    }
}
