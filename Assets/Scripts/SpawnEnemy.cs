using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
    // public GameObject enemyPrefab; // Prefab của kẻ thù
    // public float spawnInterval = 2f; // Thời gian giữa các lần spawn
    // public Vector2 spawnAreaMin; // Điểm bắt đầu của khu vực spawn
    // public Vector2 spawnAreaMax; // Điểm kết thúc của khu vực spawn
    // public int maxEnemies = 10;

    // private int currentEnemyCount = 0;

    // private void Start()
    // {
    //     InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    // }

    // private void SpawnEnemy()
    // {
    //     if (enemyPrefab == null)
    //     {
    //         return; // Dừng lại nếu prefab không tồn tại
    //     }

    //     if (currentEnemyCount >= maxEnemies)
    //     {
    //         Debug.Log("Maximum number of enemies reached. No more enemies will be spawned.");
    //         return; // Dừng lại nếu đã đạt đến số lượng tối đa
    //     }

    //     float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
    //     float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
    //     Vector2 spawnPosition = new Vector2(spawnX, spawnY);

    //     // Spawn kẻ thù tại vị trí ngẫu nhiên
    //     Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //     currentEnemyCount++;
    // }
    public Transform[] spawnPoints;
    public GameObject enemyPrefabs;
    public EnemyTypeProb[] enemyTypes;
    private List<EnemyType> probList = new List<EnemyType>();

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2, 10);

        foreach (EnemyTypeProb enem in enemyTypes)
        {
            for (int i = 0; i < enem.probability; i++)
                probList.Add(enem.type);
        }
    }

    void SpawnEnemy()
    {
        int r = Random.Range(0, spawnPoints.Length);

        GameObject myEnemy = Instantiate(enemyPrefabs, spawnPoints[r].position, Quaternion.identity);

        myEnemy.GetComponent<EnemyController>().type = probList[Random.Range(0, probList.Count)];
    }
}

[System.Serializable]
public class EnemyTypeProb
{
    public EnemyType type;
    public int probability;
}