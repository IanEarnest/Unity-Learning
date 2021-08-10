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

        void Start()
        {
            //lives = maxLives;
            healthSlider.maxValue = lives;
            Debug.Log($"Score: {score}");
            Debug.Log($"Lives: {lives}");
        }

        void Update()
        {
            GameOverCheck();
            UIUpdate();
            if (score >= scoreDifficultyIncrease)
            {
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerUnit2>();
                spawnManager.DifficultyIncrease();
                scoreDifficultyIncrease += scoreDifficultyIncrease;
            }
        }
        List<GameObject> dogsList = new List<GameObject>();
        public void AddAnimal(string name)
        {
            float dogXIncrement = 0.8f;
            Vector3 dogPos = new Vector3(dogBoxSpawn.transform.position.x-(dogXIncrement*dogsList.Count), dogBoxSpawn.transform.position.y, dogBoxSpawn.transform.position.z);
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
        List<GameObject> chickens = new List<GameObject>();
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
                    chicken.GetComponent<MoveForward>().Barked();
                }
            }
        }
        IEnumerator WaitForSeconds(float time)
        {
            //StartCoroutine(PrintWaitTime(time));
            yield return new WaitForSeconds(time);
            CancelInvoke("FindAndBarkChickens");
            //SetIsBarking(false);
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
        public static void SetIsBarking(bool _isBarking)
        {
            isBarking = _isBarking;
        }
        public static bool isBarking = false;
        float barkingDuration = 3;
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

        //gameOverText
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
        void HealthSliderUIUpdate()
        {
            if (lives >= maxLives)
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.green;
                heartGOImage.sprite = heartFull;
            }
            else if (lives >= ((maxLives / 4) + (maxLives / 2)))
            {
                //print((maxLives / 4) + (maxLives / 2)); // 3 or 7 if 5 or 10 max
                fillGOImage.color = Color.black; // set fill to red after first damage

                healthSlider.GetComponentInChildren<Image>().color = Color.green;
                heartGOImage.sprite = heart3of4;
            }
            else if (lives >= (maxLives / 2))
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.yellow;
                heartGOImage.sprite = heart2of4;
            }
            else if (lives >= (maxLives / 4))
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.red;
                heartGOImage.sprite = heart1of4;
            }
            else
            {
                healthSlider.GetComponentInChildren<Image>().color = Color.red;
                heartGOImage.sprite = heartEmpty;
            }
        }
        public void UIUpdate()
        {
            //Healthslider
            //slider
            //Background
            //    image
            //        color
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