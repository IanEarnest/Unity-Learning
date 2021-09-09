using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class RocketUnit4 :MonoBehaviour
    {
        public bool isEnemyRocket;
        void Start()
        {
        }

        void Update()
        {
        }

        public void LaunchRocket(GameObject target)
        {
            //print($"Rocket launched at {target.name}");
            //GameObject newRocket = Instantiate(rocket, transform.position, Quaternion.identity); // look at an enemy
            //newRocket.transform.LookAt(target.transform);
            //newRocket.GetComponent<Rigidbody>().move
            StartCoroutine(MoveRocket(gameObject, target, 5)); //InvokeRepeating(Grow);

            //newRocket.GetComponent<Rigidbody>().AddForce(lookDirection * speed); // normalize to stop speed from multiplying
        }
        float rocketSpeed = 50;
        IEnumerator MoveRocket(GameObject rocket, GameObject target, float timer)
        {
            if (rocket == null || target == null)
            {
                Destroy(rocket);
                yield break;
            }
            timer -= 0.01f;
            //direction? speed?
            //Vector3 lookDirection = //(go.transform.position - target.transform.position).normalized;

            //rocket.GetComponent<Rigidbody>().AddForce(target.transform.position, ForceMode.Impulse); // normalize to stop speed from multiplying
            Vector3 lookDirection = (target.transform.position - rocket.transform.position).normalized;

            rocket.transform.position += lookDirection * rocketSpeed * Time.deltaTime;
            rocket.transform.LookAt(target.transform);
            yield return new WaitForSeconds(0.01f);
            if (timer > 0)
            {
                StartCoroutine(MoveRocket(rocket, target, timer));
            }
            else
            {
                Destroy(rocket);
            }
        }

        //float powerupStrength = 20;
        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Enemy"))
        //    {
        //        print("enemy hit wave");
        //        Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
        //        Vector3 awayFromWave = (other.gameObject.transform.position - transform.position);
        //        enemyRigidbody.AddForce(awayFromWave * powerupStrength, ForceMode.Impulse);
        //    }
        //}
    }
}