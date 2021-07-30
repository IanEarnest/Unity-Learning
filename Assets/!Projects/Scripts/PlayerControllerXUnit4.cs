using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit4
{
    public class PlayerControllerXUnit4 : MonoBehaviour
    {
        private Rigidbody playerRb;
        private float speed = 500;
        float powerSpeed = 2;
        private GameObject focalPoint;
        private GameObject smokeParticles;

        public bool hasPowerup;
        public GameObject powerupIndicator;
        public int powerUpDuration = 5;

        private float normalStrength = 10; // how hard to hit enemy without powerup
        private float powerupStrength = 25; // how hard to hit enemy with powerup
        private float coolDown = 0;
        private float coolDownMax = 400;

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
            smokeParticles = GameObject.Find("Smoke_Particle");
        }

        void Update()
        {
            // Add force to player in direction of the focal point (and camera)
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

            // Set powerup indicator position to beneath player
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);


            if (coolDown == 1) // just before usable
            {
                print("Power ready");
            }
            if (Input.GetAxis("Jump") == 1 && coolDown == 0 || Input.GetKeyDown(KeyCode.Space) && hasPowerup)
            {
                coolDown = coolDownMax;
                playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * powerSpeed * Time.deltaTime, ForceMode.Impulse);
                //particle effect
                smokeParticles.GetComponent<ParticleSystem>().Play();
            }
            if (coolDown > 0)
            {
                coolDown--;
            }
        }

        // If Player collides with powerup, activate powerup
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
                Destroy(other.gameObject);
                hasPowerup = true;
                powerupIndicator.SetActive(true);
                StartCoroutine(PowerupCooldown());
            }
        }

        // Coroutine to count down powerup duration
        IEnumerator PowerupCooldown()
        {
            yield return new WaitForSeconds(powerUpDuration);
            hasPowerup = false;
            powerupIndicator.SetActive(false);
        }

        // If Player collides with enemy
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position;

                if (hasPowerup) // if have powerup hit enemy with powerup force
                {
                    enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
                }
                else // if no powerup, hit enemy with normal strength
                {
                    enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);//, ForceMode.Impulse);
                }


            }
        }
    }
}