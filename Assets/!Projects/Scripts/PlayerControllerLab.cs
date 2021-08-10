using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLab : MonoBehaviour
{
    float speed = 50;
    float bound = 10;
    Rigidbody playerRb;
    string enemyTag = "Enemy";
    string powerupTag = "Powerup";


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }


    // Moves player based on input
    private void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //if keycode - translate addforce forward, speed, vert
        //jump / landing
        playerRb.AddForce(Vector3.forward * verticalInput * speed);
        playerRb.AddForce(Vector3.right * horizontalInput * speed);
    }

    // Prevents player from leaving screen
    private void ConstrainPlayerPosition()
    {
        if (transform.position.z > bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bound);
        }
        if (transform.position.z < -bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            print("Enemy hit!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(powerupTag))
        {
            print("POWERUP!");
            Destroy(other.gameObject);
        }
    }
}
