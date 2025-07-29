using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager2 : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask bossLayer;
    [SerializeField] float bossPriorityRange = 5f; // phạm vi ưu tiên boss
    public Transform FindNearestMonster(Transform player, float detectionradius)
    {
        // boss
        Collider2D[] collider2DsBoss = Physics2D.OverlapCircleAll(player.position, bossPriorityRange, bossLayer);
        Transform nearestBoss = null; // biến lưu boss gần nhất
        float minDistanceBoss = Mathf.Infinity;
        foreach(var boss in collider2DsBoss)
        {
            float distance = Vector3.Distance(player.position, boss.transform.position);
            if(distance < minDistanceBoss)
            {
                minDistanceBoss = distance;
                nearestBoss = boss.transform;
            }
        }
        if (nearestBoss != null)
            return nearestBoss;

        // enemy
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(player.position, detectionradius, enemyLayer);
        Transform nearestMonster = null; // biến lưu monster gần nhất
        float minDistance = Mathf.Infinity; // biến để lưu khoảng cách gần nhất tìm được. ban đầu là nhỏ nhất
        foreach(var collider in collider2Ds)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(player.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestMonster = collider.transform;
                }
            }
        }
        return nearestMonster;
    }
}
