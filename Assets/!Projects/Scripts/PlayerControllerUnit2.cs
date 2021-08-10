using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class PlayerControllerUnit2 :MonoBehaviour
    {

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
        private GameManagerUnit2 gameManager;
        private SpawnManagerUnit2 spawnManager;

        private void Start()
        {
            foodSpawn = GameObject.Find("FoodSpawnPosition");
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerUnit2>();
        }
        void Update()
        {
            //R - Restart
            //V - bark
            //Q - spawn
            //C - lose life
            PlayerDebugInput();

            PlayerInput(); // WASD
            PlayerMove(); // transform.translate (moving game object)
            PlayerRestriction();
            PlayerShoot(); // Space to shoot
            foodSpawnPosition = foodSpawn.transform.position;
            if (coolDown > 0)
            {
                coolDown--;
            }
        }
        void PlayerDebugInput()
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
                //spawnManager.QuickSpawn();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                //gameManager.LoseALife();
            }
        }
        void PlayerInput()
        {
            // Player input
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        void PlayerMove()
        {
            // Player movement
            //transform.Translate(horizontalInput * speed * Time.deltaTime, 0, verticalInput * speed * Time.deltaTime);//Vector3.right * speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        }
        void PlayerRestriction()
        {
            // Player restriction - keep the player in bounds
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
        void PlayerShoot()
        {
            // Player movement
            if (Input.GetAxis("Jump") == 1 && coolDown == 0)
            {
                coolDown = coolDownMax;
                GameObject newFood = Instantiate(food, foodSpawnPosition, food.transform.rotation);//transform.position, food.transform.rotation);
                newFood.GetComponent<MoveForward>().speed = 40;
                newFood.GetComponent<MoveForward>().enabled = true;
                newFood.GetComponent<BoxCollider>().enabled = true;
                newFood.SetActive(true);
                Destroy(newFood, 5);
            }
        }

        //void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag("Animal"))
        //    {

        //    }
        //}
    }
}
