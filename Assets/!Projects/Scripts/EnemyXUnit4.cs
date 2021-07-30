using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class EnemyXUnit4 :MonoBehaviour
    {
        private Rigidbody enemyRb;
        private GameObject playerGoal;

        // Start is called before the first frame update
        void Start()
        {
            enemyRb = GetComponent<Rigidbody>();
            playerGoal = GameObject.Find("Player Goal");
        }

        // Update is called once per frame
        void Update()
        {
            // Set enemy direction towards player goal and move there
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized; //playerGoal.transform.position?
            enemyRb.AddForce(lookDirection * SpawnManagerXUnit4.speed * Time.deltaTime);
            print(SpawnManagerXUnit4.speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            // If enemy collides with either goal, destroy it
            if (other.gameObject.name == "Enemy Goal")
            {
                print("Player scored");
                Destroy(gameObject);
            }
            else if (other.gameObject.name == "Player Goal")
            {
                print("Enemy scored");
                Destroy(gameObject);
            }
        }
    }
}