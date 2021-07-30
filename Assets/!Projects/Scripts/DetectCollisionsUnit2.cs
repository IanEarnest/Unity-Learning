using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DetectCollisionsUnit2 : MonoBehaviour
{
    private GameManagerUnit2 gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player") && other.CompareTag("Animal"))
        {
            gameManager.LoseALife();
            Destroy(other.gameObject);
        }
        if (gameObject.CompareTag("Projectile") && other.CompareTag("Animal"))//"Projectile"))
        {
            Destroy(gameObject);
            //gameManager.score++;
            other.GetComponent<AnimalHungerUnit2>().FeedAnimal(1);
            Debug.Log("Score: " + GameManagerUnit2.score);
        }
    }
}
