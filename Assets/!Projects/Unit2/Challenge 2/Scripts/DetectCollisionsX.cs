using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("Ball") && other.name.Contains("Dog"))
        {
            Destroy(gameObject);
        }
        if (gameObject.name.Contains("Ball") && other.name == "Ground")
        {
            Debug.Log("GameOver");
            Destroy(gameObject);
        }
    }
}
