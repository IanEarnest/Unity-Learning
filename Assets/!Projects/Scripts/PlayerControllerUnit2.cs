using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class PlayerControllerUnit2 :MonoBehaviour
    {
        // Player input keys, WASD, R, V
        // moving player + restriction
        // shooting
        // uses GameManager
        //R - Restart
        //V - bark
        //Q - spawn //debug
        //C - lose life //debug

        public float speed = 20.0f;
        public float horizontalInput;// = -1..1;
        public float verticalInput;// = -1..1;
        float xRange = 10.0f;
        float zRange = 3.0f;
        public GameObject food;
        Vector3 foodSpawnPosition;
        GameObject foodSpawn;
        int coolDown = 0;
        int coolDownMax = 50;
        GameManagerUnit2 gameManager;

        // Setup gameobjects and scripts
        void Start()
        {
            foodSpawn = GameObject.Find("FoodSpawnPosition"); // for player to shoot food
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
        }
        // Player input keys, WASD, R, V,
        // moving player + restriction
        // shooting
        void Update()
        {
            PlayerKeysInput(); // R - restart, V - bark
            PlayerInput(); // WASD
            PlayerMove(); // transform.translate (moving game object)
            PlayerRestriction(); // restrict movement
            PlayerShoot(); // Space to shoot
        }

        // R - restart, V - bark
        void PlayerKeysInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gameManager.Restart();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                gameManager.Bark();
            }

            //Debugs
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerUnit2>();
                //spawnManager.QuickSpawn();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                //gameManager.LoseALife();
            }
        }
        // Player input - horizontal/ vertical
        void PlayerInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        // Player movement
        void PlayerMove()
        {
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        }
        // Player restriction - keep the player in bounds
        void PlayerRestriction()
        {
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            if (transform.position.z < -zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
            }
            else if (transform.position.z > zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            }
        }
        // Shoot food from player
        void PlayerShoot()
        {
            // Player movement
            if (Input.GetAxis("Jump") == 1 && coolDown == 0)
            {
                coolDown = coolDownMax;
                GameObject newFood = Instantiate(food, foodSpawnPosition, food.transform.rotation);//transform.position, food.transform.rotation);
                newFood.GetComponent<MoveForwardUnit2>().speed = 40;
                newFood.GetComponent<MoveForwardUnit2>().enabled = true;
                newFood.GetComponent<BoxCollider>().enabled = true;
                newFood.SetActive(true);
                Destroy(newFood, 5);
            }
            foodSpawnPosition = foodSpawn.transform.position;
            if (coolDown > 0)
            {
                coolDown--;
            }
        }
    }
}
