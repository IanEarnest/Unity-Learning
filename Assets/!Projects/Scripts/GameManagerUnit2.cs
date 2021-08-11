using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unit2
{

    public class GameManagerUnit2 :MonoBehaviour
    {
        // using spawnManager, main camera

        // check for game over
        // update ui/ health slider
        // barking - slow chickens, shake screen
        // restart, add/ remove life, add score

        public GameObject mainCamera;
        public static int lives = 10;
        int maxLives = 10;
        public static int score = 0;
        public Slider healthSlider;
        public TextMeshProUGUI healthText;
        public Text scoreText;
        public Text gameOverText;
        public GameObject restartButton;
        string scoreTextDefault = "Score: ";
        public Image heartGOImage;
        public Sprite heartFull;
        public Sprite heart3of4;
        public Sprite heart2of4;
        public Sprite heart1of4;
        public Sprite heartEmpty;
        public Image fillGOImage;
        public Text barkText;
        public bool gameOver;
        public GameObject miniBeagle;
        public GameObject miniBulldog;
        public GameObject miniChicken;
        public GameObject chickenBoxSpawn;
        public GameObject dogBoxSpawn;
        float scoreDifficultyIncrease = 30;
        SpawnManagerUnit2 spawnManager;
        List<GameObject> dogsList = new List<GameObject>();
        List<GameObject> chickens = new List<GameObject>();
        public static void SetIsBarking(bool _isBarking)
        {
            isBarking = _isBarking;
        }
        public static bool isBarking = false;
        float barkingDuration = 3;

        void Start()
        {
            healthSlider.maxValue = lives;
            Debug.Log($"Score: {score}");
            Debug.Log($"Lives: {lives}");
        }

        void Update()
        {
            GameOverCheck(); // lives = 0, can restart, pause time
            UIUpdate(); // player health slider, score, health, barking
            CheckIncreaseDifficulty(); // based on score
        }
        #region update
        public void GameOverCheck()
        {
            if (lives <= 0)
            {
                gameOver = true;
                gameOverText.enabled = true;
                restartButton.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        public void UIUpdate()
        {
            //Healthslider - slider - background - image - color
            healthSlider.value = (maxLives - lives); //lives-maxlives?
            scoreText.text = scoreTextDefault + score;
            healthText.text = $"{lives}/{maxLives}";
            if (dogsList.Count > 0 && !isBarking)
            {
                barkText.enabled = true;
            }
            else
            {
                barkText.enabled = false;
            }
            HealthSliderUIUpdate();
        }
        // UI for heart graphic
        void HealthSliderUIUpdate()
        {
            // Full HP
            // Less than full HP
            // Half HP
            // Quarter HP
            // Less than quarter HP

            // Full HP
            if (lives >= maxLives)
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.green;
                heartGOImage.sprite = heartFull;
            }
            // Less than full HP
            else if (lives >= ((maxLives / 4) + (maxLives / 2)))
            {
                //print((maxLives / 4) + (maxLives / 2)); // 3 or 7 if 5 or 10 max
                fillGOImage.color = Color.black; // set fill to red after first damage

                healthSlider.GetComponentInChildren<Image>().color = Color.green;
                heartGOImage.sprite = heart3of4;
            }
            // Half HP
            else if (lives >= (maxLives / 2))
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.yellow;
                heartGOImage.sprite = heart2of4;
            }
            // Quarter HP
            else if (lives >= (maxLives / 4))
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.red;
                heartGOImage.sprite = heart1of4;
            }
            // Less than quarter HP
            else
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.red;
                heartGOImage.sprite = heartEmpty;
            }
        }
        void CheckIncreaseDifficulty()
        {
            if (score >= scoreDifficultyIncrease)
            {
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerUnit2>();
                spawnManager.DifficultyIncrease();
                scoreDifficultyIncrease += scoreDifficultyIncrease;
            }
        }
        #endregion

        #region barking
        // barking - slow chickens, shake screen
        public void Bark()
        {
            //check if dogs
            //slow down just chickens for 5s, chick anim
            //delete last dog
            //shake screen
            if (dogsList.Count > 0 && !isBarking)
            {
                isBarking = true;
                MakeChickensSlow(barkingDuration);
                DestroyDog();
                ShakeCamera(barkingDuration);
            }
            else
            {
                //AddAnimal(SpawnManagerUnit2.beagleName); // testing
            }
        }
        // Spawn dog/ chicken in areas
        public void AddAnimal(string name)
        {
            float dogXIncrement = 0.8f;
            Vector3 dogPos = new Vector3(dogBoxSpawn.transform.position.x-(dogXIncrement*dogsList.Count), dogBoxSpawn.transform.position.y, dogBoxSpawn.transform.position.z);
            // Spawn dog
            if (dogsList.Count < 6)
            {
                if (name.Contains(SpawnManagerUnit2.beagleName))
                {
                    AddDogToListAndGame(miniBeagle, dogPos);
                }
                else if (name.Contains(SpawnManagerUnit2.bulldogName))
                {
                    AddDogToListAndGame(miniBulldog, dogPos);
                }
            }
            else
            {
                print("too many dogs");
            }
            // Spawn mini-chicken
            if (name.Contains(SpawnManagerUnit2.chickenName))
            {
                Instantiate(miniChicken, chickenBoxSpawn.transform.position, miniChicken.transform.rotation);
            }
        }
        public void AddDogToListAndGame(GameObject dog, Vector3 dogPos)
        {
            GameObject newDog = Instantiate(dog, dogPos, dog.transform.rotation);
            dogsList.Add(newDog);
        }
        void FindAllChickens()
        {
            //get chickens
            GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
            foreach (GameObject chicken in animals)
            {
                if (chicken.name.Contains("Chicken"))
                {
                    chickens.Add(chicken);
                }
            }
        }
        // Repeat find chickens and apply bark effects every 0.5 seconds
        void MakeChickensSlow(float barkingDuration)
        {
            InvokeRepeating("FindAndBarkChickens", 0, 0.5f);
            StartCoroutine(WaitForSeconds(barkingDuration)); // cancel invokes/ end barking
        }
        void FindAndBarkChickens()
        {
            FindAllChickens();
            foreach (GameObject chicken in chickens)
            {
                if (chicken != null)
                {
                    chicken.GetComponent<MoveForwardUnit2>().Barked();
                }
            }
        }
        // Wait and cancel repeat invoke
        IEnumerator WaitForSeconds(float time)
        {
            yield return new WaitForSeconds(time);
            CancelInvoke("FindAndBarkChickens");
        }
        void DestroyDog()
        {
            GameObject dogToDelete = dogsList[dogsList.Count - 1].gameObject;
            dogsList.RemoveAt(dogsList.Count - 1);
            Destroy(dogToDelete);
        }
        void ShakeCamera(float barkingDuration)
        {
            mainCamera.GetComponent<CameraShakeUnit2>().CameraShaking(barkingDuration); // 3 seconds
        }
        #endregion


        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            lives = maxLives;
            score = 0;
            gameOver = false;
            gameOverText.enabled = false;
            restartButton.gameObject.SetActive(false);
            Time.timeScale = 1;
            print("Restarted");
        }
        public void AddLives(int value)
        {
            lives += value;
            //Debug.Log($"Lives added - Lives: {lives}");
        }
        public void AddScore(int value)
        {
            score += value;
            //Debug.Log($"Score added - Score: {score}");
        }
        public void LoseALife()
        {
            if (lives > 0)
            {
                lives--;
            }
            else
            {
                //Debug.Log("GameOver");
            }
            //Debug.Log($"Lives: {lives}");
        }
    }
}