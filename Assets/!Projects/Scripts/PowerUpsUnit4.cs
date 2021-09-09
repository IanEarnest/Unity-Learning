using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class PowerUpsUnit4 :MonoBehaviour
    {
        public Rigidbody playerRb;
        PlayerControllerUnit4 _PlayerControllerUnit4;

        public GameObject wave;
        public GameObject rocket;
        public float powerupStrength = 15.0f;
        public int powerupTimer = 7;
        RocketUnit4 _RocketUnit4;
        Coroutine _coroutine;
        List<Coroutine> _coroutines = new List<Coroutine>();


        //Smash     = Lightning = Balloon   = 1
        //Rocket    = Star      = Edge      = 2
        //Push      = Gem       = Ring      = 3
        public enum powerupType
        {
            none,
            smash,
            rockets,
            push
        }

        void Start()
        {
            _PlayerControllerUnit4 = GameObject.Find("Player").GetComponent<PlayerControllerUnit4>();
        }
        void Update()
        {
            CheckPowerUp1();    //Smash     = Lightning = Balloon   = 1
            CheckPowerUp2();    //Rocket    = Star      = Edge      = 2
            //CheckPowerUp3();  //Push      = Gem       = Ring      = 3
            CheckPlayerPosition();
        }

        void CheckPlayerPosition()
        {
            // on ground, check with trigger?
            if (playerRb.position.y < 0.2f)
            {
                if (_PlayerControllerUnit4.goneUp) // powerup already checked in "above ground"
                {
                    MakeShockwave();
                    _PlayerControllerUnit4.allowJump = true;
                }
                _PlayerControllerUnit4.goneUp = false;
            }
            // above ground
            if (playerRb.position.y > 1 && _PlayerControllerUnit4.hasPowerup && _PlayerControllerUnit4.activePowerup == (int)powerupType.smash)
            {
                _PlayerControllerUnit4.allowJump = false;
                _PlayerControllerUnit4.goneUp = true;
            }

            if (_PlayerControllerUnit4.hasPowerup && _PlayerControllerUnit4.activePowerup == (int)powerupType.smash)
            {
            }
        }

        void CheckPowerUp1()
        {
            if (_PlayerControllerUnit4.hasPowerup && _PlayerControllerUnit4.activePowerup == (int)powerupType.smash)
            {
                if (Input.GetKeyDown(KeyCode.Space) && _PlayerControllerUnit4.allowJump)//(Input.GetKey(KeyCode.Space))
                {
                    float jumpForce = 5;
                    playerRb.AddForce(new Vector3(0, 2, 0) * jumpForce, ForceMode.Impulse); // velocity?
                    _PlayerControllerUnit4.allowJump = false;
                }
            }
        }
        void CheckPowerUp2()
        {
            if (_PlayerControllerUnit4.hasPowerup && _PlayerControllerUnit4.activePowerup == (int)powerupType.rockets)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //get list of enemies
                    //launch one rocket for each enemy
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                    //print($"Enemies {enemies.Length}");
                    foreach (GameObject enemy in enemies)
                    {
                        GameObject newRocket = Instantiate(rocket, playerRb.transform.position, Quaternion.identity); // look at an enemy
                        _RocketUnit4 = newRocket.GetComponent<RocketUnit4>();
                        _RocketUnit4.LaunchRocket(enemy);
                    }
                }
                else
                {
                }
            }
        }

        void MakeShockwave()
        {
            GameObject newWave = Instantiate(wave, playerRb.transform.position, Quaternion.identity);
            StartCoroutine(Grow(newWave, 1, 20)); //InvokeRepeating(Grow);
        }
        IEnumerator Grow(GameObject go, int multiplier, int limit)
        {
            Vector3 newScale = go.transform.localScale;
            newScale.x += multiplier;
            newScale.y += 0;//multiplier;
            newScale.z += multiplier;
            go.transform.localScale = newScale;
            //print("newScale:" + newScale);
            yield return new WaitForSeconds(0.01f);
            if (newScale.x < limit)//(++multiplier < limit)
            {
                StartCoroutine(Grow(go, multiplier, limit));
            }
            else
            {
                Destroy(go);
            }
        }
        public void ActivatePowerUp(int powerUp, Collider other = null)
        {
            // destroy pickup
            if (other != null)
            {
                Destroy(other.gameObject);
            }
            //existing powerup
            PowerUpDisable();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine); //StopCoroutine(PowerupCountdownRoutine()); //[StopAllCoroutines][1]()
            }
            if (_coroutines.Count != 0)
            {
                foreach (Coroutine co in _coroutines)
                {
                    StopCoroutine(co);
                }
                _coroutines.Clear();
            }
            //print($"Power up {powerUp} {Enum.GetName(typeof(powerupType), powerUp)}, {(powerupType)powerUp}"); //powerupType enum?
            _PlayerControllerUnit4.activePowerup = powerUp;
            _PlayerControllerUnit4.hasPowerup = true;

            _PlayerControllerUnit4.powerupIndicator = _PlayerControllerUnit4.powerupIndicators[powerUp - 1];
            _PlayerControllerUnit4.powerupIndicator.SetActive(true);

            powerupTimerTMP = powerupTimer;
            _coroutine = StartCoroutine(PowerupCountdownRoutine());
        }
        // power up counts down mutiple seconds
        // cause = anonamous coroutine still running in self calling code
        // list of coroutines
        // end all of them

        public int powerupTimerTMP;
        //ienumerator = interface, Coroutines
        IEnumerator PowerupCountdownRoutine()
        {
            //yield return new WaitForSeconds(powerupTimer);
            yield return new WaitForSeconds(1);
            powerupTimerTMP--;
            if (powerupTimerTMP > 0)
            {
                Coroutine newCo = StartCoroutine(PowerupCountdownRoutine());
                _coroutines.Add(newCo);
            }
            else
            {
                PowerUpDisable();
            }
        }
        public void PowerUpDisable()
        {
            _PlayerControllerUnit4.hasPowerup = false;
            _PlayerControllerUnit4.powerupIndicator.SetActive(false);
            _PlayerControllerUnit4.activePowerup = (int)powerupType.none;
        }
    }
}