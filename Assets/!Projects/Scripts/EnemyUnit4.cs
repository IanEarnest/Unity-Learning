using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class EnemyUnit4 :MonoBehaviour
    {
        public float speed;
        Rigidbody enemyRB;
        GameObject player;
        SpawnManagerUnit4 _spawnManager;
        float powerupStrength = 20;
        float rocketStrength = 50;
        Coroutine _coroutine;
        float spawnTimer = 2f;
        RocketUnit4 _RocketUnit4;

        void Start()
        {
            enemyRB = GetComponent<Rigidbody>();
            player = GameObject.Find("Player");
            _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerUnit4>();
        }

        void Update()
        {
            MoveEnemy();
            CheckBossTypeSpawning();
        }

        // Boss1 - fast
        // Boss2 - spawner
        // Boss3 - rockets?
        void CheckBossTypeSpawning()
        {
            if (name.Contains("Boss2") && _coroutine == null) //Boss2
            {
                print($"enemy boss: mini-enemies spawning");
                _coroutine = StartCoroutine(SpawningEnemies());
            }
            else if (name.Contains("Boss3") && _coroutine == null) //Boss3
            {
                print($"enemy boss: rockets spawning");
                _coroutine = StartCoroutine(SpawningRockets());
            }
        }
        void MoveEnemy()
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRB.AddForce(lookDirection * speed); // normalize to stop speed from multiplying
        }
        IEnumerator SpawningEnemies()
        {
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(_spawnManager.enemyPrefab, transform.position, transform.rotation);
            StartCoroutine(SpawningEnemies());
            //print($"Spawned mini-enemy");
        }
        IEnumerator SpawningRockets()
        {
            yield return new WaitForSeconds(spawnTimer);
            GameObject rocket = Instantiate(_spawnManager.rocketPrefab, transform.position, transform.rotation);
            _RocketUnit4 = rocket.GetComponent<RocketUnit4>();
            _RocketUnit4.isEnemyRocket = true;
            _RocketUnit4.LaunchRocket(GameObject.Find("Player"));
            StartCoroutine(SpawningRockets());
            //print($"Spawned enemy rocket");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wave"))//"Enemy"))
            {
                //print("enemy hit wave");
                Rigidbody enemyRigidbody = gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromWave = (gameObject.transform.position - other.transform.position);
                enemyRigidbody.AddForce(awayFromWave * powerupStrength, ForceMode.Impulse);
            }
            if (other.CompareTag("Rocket"))
            {
                if (other.GetComponent<RocketUnit4>().isEnemyRocket == true)
                {
                    //print($"Enemy hit by own rocket");
                    return;
                }
                //print("enemy hit rocket");
                Rigidbody enemyRigidbody = gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromWave = (gameObject.transform.position - other.transform.position);
                enemyRigidbody.AddForce(awayFromWave * rocketStrength, ForceMode.Impulse);
                Destroy(other.gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}