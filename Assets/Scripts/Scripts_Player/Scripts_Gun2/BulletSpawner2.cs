using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner2 : MonoBehaviour
{
    AudioManager audioManager;

    PlayerStats playerStats;

    [SerializeField] private List<GameObject> bulletPrefab;
    [SerializeField] private GameObject effect;
    //private GameObject effectInstan;
    //private GameObject bulletinstan;
    public List<Transform> firePoints = new List<Transform>();

    private int currentBulletIndex = 0;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void SpawnBullet(Transform target)
    {
        if (bulletPrefab == null) return;
        if (target == null) return;

        audioManager.PlaySfx(audioManager.shotClip);

        foreach (Transform firePoint in firePoints)
        {
            if (firePoint == null) continue;

            //GameObject bulletinstan = Instantiate(bulletPrefab[currentBulletIndex], firePoint.position, Quaternion.identity);

            Vector3 direction = target.position - firePoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            // TEST OBJECT POOLING
            string bulletTag = "Bullet" + (currentBulletIndex + 1); // Ví dụ: Bullet1, Bullet2,...
            GameObject bulletinstan = ObjectPooler.Instance.SpawnFromPool(bulletTag, firePoint.position, rotation);

            Bullet2 bulletScript = bulletinstan.GetComponent<Bullet2>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(target, playerStats.dame);
            }
        }
    }
    public void UpgradeBullet(int newIndex)
    {
        if(newIndex >= 0 && newIndex < bulletPrefab.Count)
        {
            currentBulletIndex = newIndex;
        }
    }
}
