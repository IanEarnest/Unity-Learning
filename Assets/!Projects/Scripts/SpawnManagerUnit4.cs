using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class SpawnManagerUnit4 :MonoBehaviour
    {
        public GameObject[] enemyPrefabs;
        public GameObject enemyPrefab;
        public GameObject[] enemyBossesPrefabs;
        public GameObject enemyBossPrefab;
        public GameObject[] powerupPrefabs; // lightning, star, gem
        public GameObject powerupPrefab;
        public GameObject rocketPrefab;
        public float spawnRange = 9;
        public int enemyCount;

        // Start is called before the first frame update
        void Start()
        {
            //SpawnEnemyWave(waveNumber);
        }

        // find object by script applied to object
        // when no enemies are left - spawn boss or normal wave
        void Update()
        {

        }
        public void SpawnTest()
        {
            Debug.Log($"SPAWNTEST");
            SpawnObject(enemyBossesPrefabs[2]);
        }

        public void SpawnObject(GameObject prefab, int iteration = 1)
        {
            SpawnObjects(new GameObject[] { prefab}, iteration);
        }
        public void SpawnObjects(GameObject[] gameObjects, int iteration = 1)
        {
            for (int i = 0; i < iteration; i++)
            {
                int rand = Random.Range(0, gameObjects.Length);
                Instantiate(gameObjects[rand], GenerateSpawnPos(), gameObjects[rand].transform.rotation);
            }
            //for (int i = 0; i < powerUps; i++)
            //{
            //    int rPowerUps = Random.Range(0, powerupPrefabs.Length);
            //    Instantiate(powerupPrefabs[rPowerUps], GenerateSpawnPos(), powerupPrefabs[rPowerUps].transform.rotation); // only spawn 1 every wave
            //}
        }

        Vector3 GenerateSpawnPos()
        {
            float spawnPosX = Random.Range(spawnRange, -spawnRange);
            float spawnPosZ = Random.Range(spawnRange, -spawnRange);
            Vector3 newPos = new Vector3(spawnPosX, 0, spawnPosZ);
            return newPos;
        }
    }
}