using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLab : MonoBehaviour
{

	public GameObject[] enemies; //assign in editor
	public GameObject powerup;
	float zEnemySpawn = 12;
	float xSpawnRange = 16;
	float zPowerupRange = 5;
	float ySpawn = 0.75f;

	float powerupSpawnTime = 5;
	float enemySpawnTime = 1;
	float startDelay = 1;



	// Start is called before the first frame update
	void Start()
    {
		InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
		InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
	{


		if (Input.GetKeyUp(KeyCode.Space))
        {
			SpawnRandomEnemy();
		}
    }

	void SpawnRandomEnemy()
	{
		float randomX = Random.Range(-xSpawnRange, xSpawnRange);
		int randomIndex = Random.Range(0, enemies.Length);
		Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

		Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
	}
	void SpawnPowerup()
	{
		float randomX = Random.Range(-xSpawnRange, xSpawnRange);
		float randomZ = Random.Range(-zPowerupRange, zPowerupRange);
		Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

		Instantiate(powerup, spawnPos, powerup.gameObject.transform.rotation);
	}
	//Input.keydown space
	//SpawnRandomEnemy
	//	floatX = Random.Range(-xSpawnRange...)
	//	int randomIndex = Random.Range(0, enemies.Length);
	//vector3 spawnPos = new... randomX, ySpawn, zEnemySpawn
	//Instatiate(enemies[randomIndex], spawnPos, enemies[randomin].gameobject.trans.rotation)


	//InvokeRepeating(SpawnRandomEnemy, startDelay, enemySpawnTime)
	//...powerup
}
