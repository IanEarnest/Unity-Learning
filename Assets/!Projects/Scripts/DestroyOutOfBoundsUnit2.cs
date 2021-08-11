using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DestroyOutOfBoundsUnit2 : MonoBehaviour
{
    // restrict movement, destroy/ lose life
    // uses GameManager

    float leftLimit = 60;
    float bottomLimit = -5;
    GameManagerUnit2 gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
    }
    // Destroy of past left/ right or under bottom limits
    void Update()
    {
        // -left or right of screen
        if (transform.position.x > leftLimit || transform.position.x < -leftLimit)
        {
            Destroy(gameObject);
        }
        // -under player
        else if (transform.position.z < bottomLimit) //transform.position.z > topLimit ||
        {
            gameManager.LoseALife();
            Destroy(gameObject);
        }
    }

}
