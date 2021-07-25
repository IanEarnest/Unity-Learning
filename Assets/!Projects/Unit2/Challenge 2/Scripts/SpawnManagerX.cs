using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;
    //private float spawnInterval = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //TODO  random - spawnInterval
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }
    public float spawnRate = 0.5f;
    private float nextSpawn = 0.0f;
    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            spawnRate = (float)Random.Range(3, 6);
            nextSpawn = Time.time + spawnRate;
            Invoke("SpawnRandomBall", startDelay);//, spawnInterval);
        }
    }

    //float timeOut = 0;
    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        //spawnInterval = (float)Random.Range(3, 6);
        //startDelay = (float)Random.Range(0, 2);

        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        int ballRandom = Random.Range(0, 3);
        //TODO Random r = new Random(); //int index variable
        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballRandom], spawnPos, ballPrefabs[ballRandom].transform.rotation);
    }

}
