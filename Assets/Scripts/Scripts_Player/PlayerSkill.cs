using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //[SerializeField] private GameObject skillPrefab;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float offset = 0.3f; // khoảng cách spawn skill quanh player
    [SerializeField] private int baseDamage = 5;

    private bool isSpawning = false;

    private int skillLevel = 0; // lưu cấp độ hiện tại của skill để tăng dần
    public bool IsActive => skillLevel > 0; // true nếu đã có cấp

    public void ActivateAndUpgradeSkill()
    {
        skillLevel = Mathf.Clamp(skillLevel + 1, 1, 3);

        if (!isSpawning)
        {
            StartCoroutine(SpawnLoop());
            isSpawning = true;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnSkill();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnSkill()
    {
        Vector2[] directions = GetDirectionsForLevel(skillLevel);

        //Vector2[] directions = new Vector2[]
        //{
        //    new Vector2(1, 1).normalized,
        //    new Vector2(-1, 1).normalized,
        //    new Vector2(1, -1).normalized,
        //    new Vector2(-1, -1).normalized,
        //};

        foreach (var dir in directions)
        {
            Vector3 spawnPos = transform.position + (Vector3)(dir * offset);

            // Tính góc xoay dựa vào hướng
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            string skillTag = "Skill";
            GameObject instance = ObjectPooler.Instance.SpawnFromPool(skillTag, spawnPos, rotation);
            instance.GetComponent<Rigidbody2D>().velocity = dir * speed;

            SkillDame skillDame = instance.GetComponent<SkillDame>();
            if (skillDame != null)
            {
                skillDame.SetDamage(baseDamage + (skillLevel - 1) * 3);
            }
            Debug.Log("Current Skill Level: " + skillLevel);
        }
    }
    private Vector2[] GetDirectionsForLevel(int level)
    {
        if (level == 1)
            return new[] { new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized, new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized };
        else if (level == 2)
            return new[] { new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized, new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized, Vector2.up.normalized, Vector2.down.normalized };
        else
            return new[] { new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized, new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized, Vector2.up.normalized, Vector2.down.normalized, Vector2.left.normalized, Vector2.right.normalized };
    }
}
