using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack2 : MonoBehaviour
{
    PlayerStats stats;

    TargetManager2 targetManager2;
    GunCtrl2 gunCtrl2;
    BulletSpawner2 bulletSpawner2;

    [SerializeField] private float detectionRadius = 5f;
    //[SerializeField] private Transform nearestMonster;

    public float fireRate;        // Tần suất bắn (số lần bắn mỗi giây)
    public float nextFireTime;        // Thời gian tiếp theo có thể bắn
    private void Start()
    {
        targetManager2 = GetComponent<TargetManager2>();
        gunCtrl2 = GetComponent<GunCtrl2>();
        bulletSpawner2 = GetComponent<BulletSpawner2>();

        stats = GetComponent<PlayerStats>();
        fireRate = stats.fireRate;
        stats.OnFireRateChanged += HandleFireRateChange;
    }
    private void Update()
    {
        Attack();
    }
    protected void HandleFireRateChange(float newFireRate)
    {
        fireRate = newFireRate;
    }
    protected void Attack()
    {
        Transform nearestMonster = targetManager2.FindNearestMonster(transform, detectionRadius);
        if(nearestMonster != null)
        {
            gunCtrl2.RotateTowards(nearestMonster);
            if (Time.time >= nextFireTime)
            {
                bulletSpawner2.SpawnBullet(nearestMonster);
                nextFireTime = Time.time + fireRate;
            }
        }
    }
}
