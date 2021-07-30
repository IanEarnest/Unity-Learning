using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DestroyOutOfBoundsX1 : MonoBehaviour
{
    private float leftLimit = 20;
    private float bottomLimit = -5;
    private float topLimit = 20;
    private GameManagerUnit2 gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
    }
    void Update()
    {
        // Destroy dogs if x position less than left limit
        if (transform.position.x > leftLimit || transform.position.x < -leftLimit)
        {
            gameManager.LoseALife();
            Destroy(gameObject);
        }
        // Destroy balls if y position is less than bottomLimit
        else if (transform.position.z > topLimit|| transform.position.z < bottomLimit)
        {
            gameManager.LoseALife();
            Destroy(gameObject);
        }
    }

}
