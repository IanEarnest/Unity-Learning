using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit2;

public class DetectCollisionsUnit2 : MonoBehaviour
{
    // triggers for Food, Animals, FoodSpawners and Player
    // uses gameManager and playerController
    GameManagerUnit2 gameManager;
    PlayerControllerUnit2 playerController;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerUnit2>();
    }
    // Triggers for:
    //  Player/ Animal  - loose life
    //  Food/ Animal    - feed animal   - Beagle+Bone, BullDog+Steak, Chicken+Sandwich
    //  Player/ FoodSpawner - Set player food to shoot
    void OnTriggerEnter(Collider other)
    {
        //print($"Gameobject {gameObject.name} hit: {other.name}"); // debug
        if (gameObject.CompareTag("Player") && other.CompareTag("Animal"))
        {
            gameManager.LoseALife();
            Destroy(other.gameObject);
        }
        else if (gameObject.CompareTag("Food") && other.CompareTag("Animal"))//"Projectile"))
        {
            Destroy(gameObject);
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

            // If collided with any food, set inactive, change food object for player
            if (foodSpawnerCollide)
            {
                other.gameObject.SetActive(false);
                playerController.food = other.transform.GetChild(0).gameObject;
            }
        }
    }
}
