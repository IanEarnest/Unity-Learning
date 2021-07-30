using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class PlayerController : MonoBehaviour
    {
        /*
        PlayerController
           attach to player
           playerRb, speed, getRigidbody
           float forwardInput
           rb.addforce
        PlayerController
           focalPoint - find
        */
        private Rigidbody playerRb;
        private GameObject focalPoint;
        public GameObject powerupIndicator;

        public float speed = 5.0f;
        public bool hasPowerup;
        float powerupStrength = 15.0f;
        int powerupTimer = 7;
        Vector3 offset = new Vector3(0, 1.6f, 0);

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
        }
        void Update()
        {
            float forwardInput = Input.GetAxis("Vertical");
            //playerRb.AddForce(Vector3.forward * speed * forwardInput);
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
            powerupIndicator.transform.position = transform.position + offset;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Powerup"))
            {
                hasPowerup = true;
                powerupIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine());
            }
        }
        //ienumerator = interface, Coroutines
        IEnumerator PowerupCountdownRoutine()
        {
            yield return new WaitForSeconds(powerupTimer);
            hasPowerup = false;
            powerupIndicator.SetActive(false);
            Debug.Log($"powerup = {hasPowerup}");
        }
        private void OnCollisionEnter(Collision collision)//physics
        {
            if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

                Debug.Log($"Player hit: {collision.gameObject}, with powerup = {hasPowerup}");
            }
        }
    }
}
