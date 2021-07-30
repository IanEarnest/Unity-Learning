using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float spawnRange = 9;
    public int waveNumber = 1;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // find object by script applied to object
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(++waveNumber);
        }
    }
    // Spawn one enemy and powerup per wave
    void SpawnEnemyWave(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++) // enemies to spawn
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
            Debug.Log($"Wave: {waveNumber}");
        }
        Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation); // only spawn 1 every wave
    }
    Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        Vector3 newPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return newPos;
    }
}
