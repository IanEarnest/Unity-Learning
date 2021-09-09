using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unit4
{

    public class GameManagerUnit4 :MonoBehaviour
    {
        // check for game over

        public GameObject player;

        void Start()
        {
        }

        void Update()
        {
            CheckPlayerAlive();
            CheckEnemiesAlive();
            //GameOverCheck(); // lives = 0, can restart, pause time
            //UIUpdate(); // player health slider, score, health, barking
        }
        int areaLimit = 15;
        #region update
        public bool GOWithinArea(GameObject go)
        {
            if (go.transform.position.y < -1 ||
                go.transform.position.x > areaLimit ||
                go.transform.position.x < -areaLimit ||
                go.transform.position.z > areaLimit ||
                go.transform.position.z < -areaLimit)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CheckPlayerAlive()
        {
            if (!GOWithinArea(player))
            {
                //player below level, restart level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                print("Restarted");
            }
        }

        public void CheckEnemiesAlive()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (!GOWithinArea(enemy))
                {
                    Destroy(enemy);
                }
            }
        }
        public void UIUpdate()
        {
        }
        #endregion
    }
}