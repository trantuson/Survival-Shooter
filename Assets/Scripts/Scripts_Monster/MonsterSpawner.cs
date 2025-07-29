using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable] 
    public class MonsterWave
    {
        public string poolTag; // loại monster
        public int spawnCount; // số lượng monster trong wave
        public float spawnInterval; // Interval (Khoảng thời gian) spawn mỗi monster
    }

    [SerializeField] private Transform[] spawnPoint; // các điểm spawn ra monster (Random)
    [SerializeField] private List<MonsterWave> Listwaves; // danh sách các wave monster để spawn
    [SerializeField] private float timeBetweenWaves; // thời gian giữa các wave để spawn

    [SerializeField] private Transform enemiesParent;

    private int currentWaveIndex = 0; // biến lưu số wave hiện tại đang được spawn 

    // -------------- boss ----------------------

    [SerializeField] private List<int> bossWaveIndexes;
    [SerializeField] private List<GameObject> bossPrefabs;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }
    protected IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < Listwaves.Count) // nếu số wave hiện tại nhỏ hơn danh sách chứa wave thì tiếp tục spawn 
        {
            MonsterWave wave = Listwaves[currentWaveIndex]; // truy cập vào phần tử tại vị trí currentWaveIndex trong ds

            for(int i = 0; i < wave.spawnCount; i++) // lặp qua số lượng cần spawn của wave hiện tại 
            {
                // chọn vị trí ngẫu nhiên trong mảng spawnpoint
                int randomIndex = UnityEngine.Random.Range(0, spawnPoint.Length);
                Transform spawnpoint = spawnPoint[randomIndex];

                // spawn
                //Instantiate(wave.monsterPrefabs, spawnpoint.position, Quaternion.identity);

                // Spawn đúng loại monster theo tag của wave
                GameObject monster = ObjectPooler.Instance.SpawnFromPool(wave.poolTag, spawnpoint.position, Quaternion.identity);
                monster.transform.parent = enemiesParent; // Gán parent

                // thời gian chờ để spawn monster trong wave hiện tại
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // kiểm tra wave hiện tại có boss không
            int bossIndex = bossWaveIndexes.IndexOf(currentWaveIndex);
            if(bossIndex != -1 && bossIndex < bossPrefabs.Count)
            {
                SpawnBoss(bossPrefabs[bossIndex]);
            }

            // Đợi cho đến khi toàn bộ enemy đều bị tiêu diệt (hoặc ẩn khỏi màn hình nếu dùng pooling)
            yield return new WaitUntil(() => AreAllEnemiesDead());

            // thời gian chờ trước khi bắt đầu spawn wave mới khi đã spawn hết số lượng trong wave trước đó
            yield return new WaitForSeconds(timeBetweenWaves);

            // tăng số lượng wave (nếu hết wave trong danh sách chứa wave thì dừng hoặc spawn ra boss ...)
            currentWaveIndex++;
        }
    }
    protected void SpawnBoss(GameObject bossPrefab)
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoint.Length);
        Transform spawnpoint = spawnPoint[randomIndex];

        Instantiate(bossPrefab, spawnpoint.position, Quaternion.identity);
    }
    private bool AreAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // tất cả enemy dùng chung tag "Enemy"

        // duyệt qua tất cả game object có tag enemy
        // nếu trong hierarchy đang hoạt đồng thì trả về false
        // không hoạt động thì trả về true
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy) return false;
        }
        return true;
    }
}
