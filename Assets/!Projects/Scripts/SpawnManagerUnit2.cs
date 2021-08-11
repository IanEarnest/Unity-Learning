using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit2
{
    public class SpawnManagerUnit2 :MonoBehaviour
    {
        // Spawn dogs, chickens
        // Food spawners
        // Difficulty

        public GameObject[] animalPrefabs; // Chicken, Dog Beagle, Dog Bulldog
        public GameObject[] foodSpawners; // Sandwich, Bone, Steak
        public static string chickenName = "Chicken";
        public static string beagleName = "Beagle";
        public static string bulldogName = "BullDog";
        public static string sandwichName = "Sandwich";
        public static string boneName = "Bone";
        public static string steakName = "Steak";
        float spawnRangeX = 10;
        float spawnPosZ = 60; // 110 limit
        float sideSpawnMinZ = 5;
        float sideSpawnMaxZ = 30;
        float sideSpawnX = 50; // 60 limit
        float startDelay = 0;
        float spawnInterval = 3f; //1.5
        float spawnIntervalChicken = 5f;
        int lastActiveFood = 0;
        bool spawnFood = true;
        int activeFood = 0;

        // Keep spawning dogs from sides
        // and chickens from top
        void Start()
        {
            InvokeRepeating("SpawnRandomDog", startDelay, spawnInterval);
            InvokeRepeating("SpawnChicken", startDelay, spawnIntervalChicken);
        }

        // Keep checking food spawners/ selected food from player
        void Update()
        {
            CheckFoodSpawners();
        }
        #region foodSpawning
        // if any foodSpawner active, don't spawn next food
        // Spawn rotation for foods in food spawners for player to choose
        void CheckFoodSpawners()
        {
            spawnFood = true;
            CheckFoodSpawnersHaveFood();
            if (spawnFood)
            {
                SpawnFood();
            }
        }
        void  CheckFoodSpawnersHaveFood()
        {
            activeFood = 0;
            foreach (GameObject food in foodSpawners)
            {
                if (food.activeSelf)
                {
                    spawnFood = false;
                    lastActiveFood = activeFood;
                }
                activeFood++;
            }
        }
        // spawn food on foodSpawner, set active
        void SpawnFood()
        {
            GameObject foodGO;
            GameObject foodChildGO;
            //first on list
            if ((lastActiveFood + 2) > foodSpawners.Length) //+1 for next, +1 for array 0 index
            {
                foodGO = foodSpawners[0];
                foodChildGO = foodSpawners[0].transform.GetChild(0).gameObject;
            }
            else
            {
                foodGO = foodSpawners[lastActiveFood + 1];
                foodChildGO = foodSpawners[0].transform.GetChild(0).gameObject;
            }
            foodGO.SetActive(true);
            foodChildGO.SetActive(true);
        }
        #endregion

        #region dog and chicken spawning
        // Chickens only from top
        void SpawnChicken()
        {
            float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 animalSpawnLocation = new Vector3(spawnPosX, 0, spawnPosZ);

            Instantiate(animalPrefabs[0], animalSpawnLocation, animalPrefabs[0].transform.rotation);
        }

        // Dogs only from left/ right
        void SpawnRandomDog(bool isLeft)
        {
            int animalIndex = Random.Range(1, animalPrefabs.Length); // 0-3, 1 = chicken, not included
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

            Instantiate(animalPrefabs[animalIndex], animalSpawnLocation, Quaternion.Euler(rotation));
        }
        void SpawnRandomAnimalLeft()
        {
            SpawnRandomDog(true);
        }
        void SpawnRandomDogRight()
        {
            SpawnRandomDog(false);
        }
        void SpawnRandomDog()
        {
            int randomSpawn = Random.Range(0, 3); // 0-2 = 3
            switch (randomSpawn)
            {
                case 0:
                    //SpawnRandomAnimalTop();
                    break;
                case 1:
                    SpawnRandomAnimalLeft();
                    break;
                case 2:
                    SpawnRandomDogRight();
                    break;
                default:
                    break;
            }
        }
        #endregion

        // Used by GameManager to increase difficulty based on score
        public void DifficultyIncrease()
        {
            InvokeRepeating("SpawnChicken", startDelay, spawnIntervalChicken);
        }

        // Debug
        public void QuickSpawn()
        {
            SpawnRandomDog();
            SpawnChicken();
        }
    }
}