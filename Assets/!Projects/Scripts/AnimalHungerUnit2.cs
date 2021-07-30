using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHungerUnit2 : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToBeFed;
    private int currentFedAmount = 0;
    private GameManagerUnit2 gameManager;
    void Start()
    {
        hungerSlider.maxValue = amountToBeFed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerUnit2>();
    }
    public void FeedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;
        if (currentFedAmount >= amountToBeFed)
        {
            gameManager.AddScore(amountToBeFed);
            Destroy(gameObject);
        }
    }
}
