using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class WaveUnit4 :MonoBehaviour
    {
        public int waveNumber = 0;//1
        public int bossWaveNum = 5;
        SpawnManagerUnit4 _SpawnManagerUnit4;

        void Start()
        {
            _SpawnManagerUnit4 = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerUnit4>();
        }

        void Update()
        {
            // first wave
            if (waveNumber == 0)
            {
                SpawnEnemyWave(++waveNumber);
                return;
            }
            _SpawnManagerUnit4.enemyCount = FindObjectsOfType<EnemyUnit4>().Length;
            if (_SpawnManagerUnit4.enemyCount == 0)
            {
                if ((waveNumber + 1) % bossWaveNum == 0) // every 5th wave spawn boss
                {
                    SpawnEnemyBossWave(++waveNumber); // 10th wave spawn 2 bosses?
                }
                else
                {
                    SpawnEnemyWave(++waveNumber);
                }
            }
        }

        // Spawn one enemy*, and powerup per wave
        public void SpawnEnemyWave(int waveNumber, int powerUps = 1)
        {
            Debug.Log($"Wave: {waveNumber}");
            _SpawnManagerUnit4.SpawnObjects(_SpawnManagerUnit4.enemyPrefabs, waveNumber); // enemies to spawn
            _SpawnManagerUnit4.SpawnObjects(_SpawnManagerUnit4.powerupPrefabs, powerUps); // powerups to spawn
        }

        public void SpawnEnemyBossWave(int waveNumber, int bosses = 1)
        {
            Debug.Log($"Wave: {waveNumber} (BOSS) x {bosses}");
            _SpawnManagerUnit4.SpawnObjects(_SpawnManagerUnit4.enemyBossesPrefabs, bosses); // bosses to spawn
            //SpawnObjects(new GameObject[] { enemyBossPrefab }, bosses); // bosses to spawn
            //for (int i = 0; i < bosses; i++) // enemies to spawn
            //{
            //    Instantiate(enemyBossPrefab, GenerateSpawnPos(), enemyBossPrefab.transform.rotation);
            //}
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