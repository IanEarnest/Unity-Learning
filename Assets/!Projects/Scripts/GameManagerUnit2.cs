using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUnit2 : MonoBehaviour
{
    public static int lives = 5;
    public static int score = 0;

    void Start()
    {
        Debug.Log($"Score: {score}");
        Debug.Log($"Lives: {lives}");
    }

    void Update()
    {
    }

    public void AddLives(int value)
    {
        lives += value;
        Debug.Log($"Lives added - Lives: {lives}");
    }
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score added - Score: {score}");
    }

    public void LoseALife()
    {
        if (lives > 0)
        {
            lives--;
        }
        else
        {
            Debug.Log("GameOver");
        }
        Debug.Log($"Lives: {lives}");
    }
}
