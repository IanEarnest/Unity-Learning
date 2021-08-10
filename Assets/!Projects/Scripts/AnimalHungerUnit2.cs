using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit2
{
    public class AnimalHungerUnit2 :MonoBehaviour
    {
        public Slider hungerSlider;
        public int amountToBeFed;
        int currentFedAmount = 0;
        GameManagerUnit2 gameManager;
        float xMultiplier= 10;

        void Start()
        {
            hungerSlider.maxValue = amountToBeFed;
            hungerSlider.value = hungerSlider.maxValue;
            // Set slider size to amountToBeFed value - larger = larger
            Vector2 hungerSliderSizeDelta = hungerSlider.gameObject.GetComponent<RectTransform>().sizeDelta;
            float xSliderSize = hungerSliderSizeDelta.x+(amountToBeFed*xMultiplier);
            hungerSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(xSliderSize, hungerSliderSizeDelta.y);
            //hungerSlider.fillRect.gameObject.SetActive(false); //important?

            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
        }
        public void FeedAnimal(int amount)
        {
            currentFedAmount += amount;
            //hungerSlider.fillRect.gameObject.SetActive(true);
            hungerSlider.value = (amountToBeFed - currentFedAmount);
            if (currentFedAmount >= amountToBeFed)
            {
                if (gameObject.name.Contains(SpawnManagerUnit2.beagleName))
                {
                    gameManager.AddAnimal(SpawnManagerUnit2.beagleName);
                    Destroy(gameObject);
                }
                else if (gameObject.name.Contains(SpawnManagerUnit2.bulldogName))
                {
                    gameManager.AddAnimal(SpawnManagerUnit2.bulldogName);
                    Destroy(gameObject);
                }
                else if (gameObject.name.Contains(SpawnManagerUnit2.chickenName))
                {
                    gameManager.AddAnimal(SpawnManagerUnit2.chickenName);
                    gameManager.AddScore(amountToBeFed);
                    Destroy(gameObject);
                }
            }
        }
    }
}