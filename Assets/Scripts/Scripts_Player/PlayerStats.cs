using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int dame = 10;

    public float speed = 1.5f;

    public float fireRate = 0.8f;

    public event Action<float> OnSpeedChanged;
    public event Action<int, int> OnHealthChangedd;
    public event Action<float> OnFireRateChanged;


    [SerializeField] private List<Transform> gunSpawnPoints; // các vị trí súng bám theo player
    [SerializeField] private List<GameObject> gunPrefabs;    // prefab các loại súng
    [SerializeField] private BulletSpawner2 bulletSpawner2;  // script quản lý bắn đạn
    [SerializeField] private GunCtrl2 gunCtrl;
    [SerializeField] private PlayerSkill playerSkill;

    private int currentGunCount = 0;
    private void Start()
    {
        bulletSpawner2 = GetComponent<BulletSpawner2>();
        gunCtrl = GetComponent<GunCtrl2>();
        playerSkill = GetComponent<PlayerSkill>();
        currentHealth = maxHealth;
    }
    public void IncreaseStats(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Health:
                maxHealth += (int)amount;
                currentHealth = Mathf.Min(currentHealth + (int)amount, maxHealth);
                OnHealthChangedd?.Invoke(maxHealth, currentHealth);
                break;

            case StatType.Damage:
                dame += (int)amount;
                break;

            case StatType.Speed:
                speed += amount;
                OnSpeedChanged?.Invoke(speed);
                break;
            case StatType.FireRate:
                fireRate += amount;
                OnFireRateChanged?.Invoke(fireRate);
                break;
            case StatType.AddGun:
                AddGun();
                break;
            case StatType.Upgrade:
                bulletSpawner2.UpgradeBullet((int)amount);
                break;
            case StatType.Skill:
                playerSkill.ActivateAndUpgradeSkill();
                break;
        }
    }
    public void AddGun()
    {
        if (currentGunCount < gunPrefabs.Count && currentGunCount < gunSpawnPoints.Count)
        {
            // Spawn gun tại vị trí ban đầu
            Transform spawnPoint = gunSpawnPoints[currentGunCount];
            GameObject newGun = Instantiate(gunPrefabs[currentGunCount], spawnPoint.position, spawnPoint.rotation);
            newGun.transform.SetParent(spawnPoint); // để súng follow theo spawnPoint
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
            newGun.transform.localScale = new Vector3(0.5f, 0.5f, 1);

            // Gán vị trí bắn đạn
            Transform point = newGun.transform.Find("Point");
            if (point != null)
            {
                if (bulletSpawner2.firePoints.Count > currentGunCount)
                {
                    bulletSpawner2.firePoints[currentGunCount] = point;
                }
                else
                {
                    bulletSpawner2.firePoints.Add(point); // tránh lỗi nếu chưa có
                }
            }
            else
            {
                Debug.LogWarning("Point not found in new gun prefab!");
            }

            gunCtrl.gunTransforms.Add(newGun.transform);
            currentGunCount++;
        }
        else
        {
            Debug.LogWarning("currentGunCount vượt quá giới hạn prefab hoặc spawn point!");
        }
    }
}
