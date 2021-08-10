using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DestroyOutOfBoundsUnit2 : MonoBehaviour
{
    float leftLimit = 60;
    float bottomLimit = -5;
    //float topLimit = 70;
    GameManagerUnit2 gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
    }
    void Update()
    {
        // Destroy dogs if x position less than left limit
        // -left or right of screen
        if (transform.position.x > leftLimit || transform.position.x < -leftLimit)
        {
            //gameManager.LoseALife();
            Destroy(gameObject);
        }
        // Destroy dogs if y position is less than bottomLimit
        // -under player
        else if (transform.position.z < bottomLimit) //transform.position.z > topLimit ||
        {
            gameManager.LoseALife();
            Destroy(gameObject);
        }
    }

}
