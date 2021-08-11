using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit2
{
    public class AnimalHungerUnit2 :MonoBehaviour
    {
        // Feed animal/ do something with fed animal and set hunger sliders
        public Slider hungerSlider;
        public int amountToBeFed;
        int currentFedAmount = 0;
        GameManagerUnit2 gameManager;
        float xMultiplier= 10;

        void Start()
        {
            SetupHungerSlider();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
        }
        void SetupHungerSlider()
        {
            // Set slider size to amountToBeFed value - higher amount = larger bar
            hungerSlider.maxValue = amountToBeFed;
            hungerSlider.value = hungerSlider.maxValue;
            Vector2 hungerSliderSizeDelta = hungerSlider.gameObject.GetComponent<RectTransform>().sizeDelta;
            float xSliderSize = hungerSliderSizeDelta.x+(amountToBeFed*xMultiplier);
            hungerSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(xSliderSize, hungerSliderSizeDelta.y);
        }
        // Adding animal to list, setting hunger slider
        public void FeedAnimal(int amount)
        {
            currentFedAmount += amount;
            hungerSlider.value = (amountToBeFed - currentFedAmount); // going downwards from high number
            if (currentFedAmount >= amountToBeFed) // if animal fed
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