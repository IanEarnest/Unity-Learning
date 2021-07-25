using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit2
{
    public class SpawnManagerUnit2 :MonoBehaviour
    {
        public GameObject[] animalPrefabs; // Chicken, Dog Beagle, Dog Bulldog
        float spawnRangeX = 10;
        float spawnPosZ = 20;
        //int animalIndex = 1;
        //Vector3 animalSpawnLocation = new Vector3(0, 0, 20);
        public float startDelay = 2;
        public float spawnInterval = 1.5f;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnRandomAnimal();
            }
        }

        private void SpawnRandomAnimal()
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length); // 0-3
            float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 animalSpawnLocation = new Vector3(spawnPosX, 0, spawnPosZ);

            Instantiate(animalPrefabs[animalIndex], animalSpawnLocation, animalPrefabs[animalIndex].transform.rotation);
        }
    }
}
