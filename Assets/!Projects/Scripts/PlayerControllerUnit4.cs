using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class PlayerControllerUnit4 : MonoBehaviour
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
        public GameObject[] powerupIndicators; // balloon, ring with edge, ring, target
                            //powerupPrefabs; // lightning, star, gem
        public GameObject wave;
        public GameObject rocket;
        public GameObject line;


        public float speed = 5.0f;
        public bool hasPowerup;
        public int activePowerup = 0;
        Vector3 offset = new Vector3(0, 1.6f, 0);
        SpawnManagerUnit4 _SpawnManagerUnit4;
        PowerUpsUnit4 _PowerUpsUnit4;
        RocketUnit4 _RocketUnit4;
        public bool goneUp = false;
        public bool allowJump = true;


        ////Smash     = Lightning = Balloon   = 1
        ////Rocket    = Star      = Edge      = 2
        ////Push      = Gem       = Ring      = 3
        //public enum powerupType
        //{
        //    none,
        //    smash,
        //    rockets,
        //    push
        //}
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
            _SpawnManagerUnit4 = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerUnit4>();
            _PowerUpsUnit4 = GameObject.Find("Game Manager").GetComponent<PowerUpsUnit4>();
        }
        void Update()
        {
            CheatKeys(); //1-4 for testing powerups/ bosses
            PlayerMove();
            PowerUpIndicatorFollow();
            LineFollow();
        }
        void CheatKeys()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                //_PowerUpsUnit4.powerUpCountSmash += 1;
                //adds multiple??
                _PowerUpsUnit4.CountPowerUps(1, (int)PowerUpsUnit4.powerupType.smash);
                _PowerUpsUnit4.CountPowerUps(1, (int)PowerUpsUnit4.powerupType.rockets);
                _PowerUpsUnit4.CountPowerUps(1, (int)PowerUpsUnit4.powerupType.push);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (_PowerUpsUnit4.powerUpCountSmash > 0)
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.smash, true);
                    _PowerUpsUnit4.CountPowerUps(-1, (int)PowerUpsUnit4.powerupType.smash);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_PowerUpsUnit4.powerUpCountRockets > 0)
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.rockets, true);
                    _PowerUpsUnit4.CountPowerUps(-1, (int)PowerUpsUnit4.powerupType.rockets);
                    //_PowerUpsUnit4.powerUpCountRockets--;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (_PowerUpsUnit4.powerUpCountPush > 0)
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.push, true);
                    _PowerUpsUnit4.CountPowerUps(-1, (int)PowerUpsUnit4.powerupType.push);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _SpawnManagerUnit4.SpawnTest();// boss test
            }
        }
        void PlayerMove()
        {
            float forwardInput = Input.GetAxis("Vertical");
            //playerRb.AddForce(Vector3.forward * speed * forwardInput);
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }
        void PowerUpIndicatorFollow()
        {
            powerupIndicator.transform.position = transform.position;// + offset; //offset only for balloon
        }
        void LineFollow()
        {
            //line.transform.position = transform.position;
            //Vector3 tmp = transform.position;
            //tmp.y -= 0.345f;
            //line.transform.position = tmp;
            line.transform.position = transform.position + new Vector3(0, 0.345f, 0);

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Powerup"))
            {
                if (other.name.Contains("1"))
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.smash, false, other);
                }
                else if (other.name.Contains("2"))
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.rockets, false, other);
                }
                else if (other.name.Contains("3"))
                {
                    _PowerUpsUnit4.ActivatePowerUp((int)PowerUpsUnit4.powerupType.push, false, other);
                }
            }
            else if (other.CompareTag("Rocket")) // hit by enemy rocket
            {
                if (other.GetComponent<RocketUnit4>().isEnemyRocket == true)
                {
                    //print($"Enemy hit by player with rocket");
                    PushPlayerAway(other.gameObject);
                }
            }
        }
        ////ienumerator = interface, Coroutines
        void PushPlayerAway(GameObject other)
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (transform.position - other.gameObject.transform.position);
            playerRb.AddForce(awayFromPlayer * _PowerUpsUnit4.powerupStrength, ForceMode.Impulse);
        }
        void OnCollisionEnter(Collision collision)//physics
        {
            // power up 3 = tough/ diamond/ ring
            if (collision.gameObject.CompareTag("Enemy") && hasPowerup && activePowerup == (int)PowerUpsUnit4.powerupType.push)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                enemyRigidbody.AddForce(awayFromPlayer * _PowerUpsUnit4.powerupStrength, ForceMode.Impulse);

                //Debug.Log($"Player hit: {collision.gameObject}, with powerup = {hasPowerup}");
            }
        }
    }
}
