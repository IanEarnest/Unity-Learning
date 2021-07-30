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
        float sideSpawnMinZ = 5;
        float sideSpawnMaxZ = 15;
        float sideSpawnX = 15;
        //int animalIndex = 1;
        //Vector3 animalSpawnLocation = new Vector3(0, 0, 20);
        float startDelay = 2;
        float spawnInterval = 3f; //1.5

        void Start()
        {
            InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnRandomAnimal();
            }
        }
        void SpawnRandomAnimal()
        {
            int randomSpawn = Random.Range(0, 3); // 0-2 = 3
            switch (randomSpawn)
            {
                case 0:
                    SpawnRandomAnimalTop();
                    break;
                case 1:
                    SpawnRandomAnimalLeft();
                    break;
                case 2:
                    SpawnRandomAnimalRight();
                    break;
                default:
                    break;
            }
        }
        void SpawnRandomAnimalTop()
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length); // 0-3
            float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 animalSpawnLocation = new Vector3(spawnPosX, 0, spawnPosZ);

            Instantiate(animalPrefabs[animalIndex], animalSpawnLocation, animalPrefabs[animalIndex].transform.rotation);
        }
        void SpawnRandomAnimalLeft()
        {
            SpawnRandomAnimal(true);
        }
        void SpawnRandomAnimalRight()
        {
            SpawnRandomAnimal(false);
        }
        void SpawnRandomAnimal(bool isLeft)
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length); // 0-3
            float spawnPosX = sideSpawnX;//Random.Range(spawnRangeX, spawnRangeX);
            float spawnPosZ = Random.Range(sideSpawnMinZ, sideSpawnMaxZ);
            Vector3 animalSpawnLocation;
            Vector3 rotation;
            if (isLeft)
            {
                animalSpawnLocation = new Vector3(-spawnPosX, 0, spawnPosZ);
                rotation = new Vector3(0, 90, 0);
            }
            else
            {
                animalSpawnLocation = new Vector3(spawnPosX, 0, spawnPosZ);
                rotation = new Vector3(0, -90, 0);
            }

            //Instantiate(animalPrefabs[animalIndex], animalSpawnLocation, animalPrefabs[animalIndex].transform.rotation);
            Instantiate(animalPrefabs[animalIndex], animalSpawnLocation, Quaternion.Euler(rotation));
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    print("player hit: "+other.name);
        //}
    }
}