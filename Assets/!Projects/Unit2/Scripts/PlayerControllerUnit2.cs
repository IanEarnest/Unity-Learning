using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class PlayerControllerUnit2 :MonoBehaviour
    {

        public float speed = 20.0f;
        public float horizontalInput;// = -1..1;
        float xRange = 10.0f;
        public GameObject food;
        int coolDown = 0;

        void Update()
        {
            PlayerInput();
            PlayerMove();
            PlayerRestriction();
            PlayerShoot();
            if (coolDown > 0)
            {
                coolDown--;
            }
        }
        private void PlayerInput()
        {
            // Player input
            horizontalInput = Input.GetAxis("Horizontal");
        }
        private void PlayerMove()
        {
            // Player movement
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        }
        private void PlayerRestriction()
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
        }
        private void PlayerShoot()
        {
            // Player movement
            if (Input.GetAxis("Jump") == 1 && coolDown == 0)
            {
                coolDown = 100;
                Instantiate(food, transform.position, food.transform.rotation);
            }
        }
    }
}
