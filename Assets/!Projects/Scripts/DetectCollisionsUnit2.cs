using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DetectCollisionsUnit2 : MonoBehaviour
{
    private GameManagerUnit2 gameManager;
    private PlayerControllerUnit2 playerController;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerUnit2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //print($"Gameobject {gameObject.name} hit: {other.name}");
        if (gameObject.CompareTag("Player") && other.CompareTag("Animal"))
        {
            gameManager.LoseALife();
            Destroy(other.gameObject);
        }
        else if (gameObject.CompareTag("Food") && other.CompareTag("Animal"))//"Projectile"))
        {
            Destroy(gameObject);
            //gameManager.score++;
            //print($"Gameobject {gameObject.name} hit: {other.name}");
            if (gameObject.name.Contains("Bone") && other.name.Contains("Beagle"))
            {
                other.GetComponent<AnimalHungerUnit2>().FeedAnimal(1);
            }
            else if (gameObject.name.Contains("Steak") && other.name.Contains("BullDog"))
            {
                other.GetComponent<AnimalHungerUnit2>().FeedAnimal(1);
            }
            else if (gameObject.name.Contains("Sandwich") && other.name.Contains("Chicken"))
            {
                other.GetComponent<AnimalHungerUnit2>().FeedAnimal(1);
            }
            else
            {
                //print($"Wrong food on animal");
            }
        }
        else if (gameObject.CompareTag("Player") && other.CompareTag("FoodSpawner"))
        {
            bool foodSpawnerCollide = false;
            if (other.name.Contains("SandwichSpawn"))
            {
                foodSpawnerCollide = true;
            }
            else if (other.name.Contains("BoneSpawn"))
            {
                foodSpawnerCollide = true;
            }
            else if (other.name.Contains("SteakSpawn"))
            {
                foodSpawnerCollide = true;
            }

            // If collided with any food, set inactive, change food object for player, print hit
            if (foodSpawnerCollide)
            {
                other.gameObject.SetActive(false);
                playerController.food = other.transform.GetChild(0).gameObject;
                //print($"{other.name} hit");
            }
            //gameManager.LoseALife();
            //Destroy(other.gameObject);
        }
    }
}
