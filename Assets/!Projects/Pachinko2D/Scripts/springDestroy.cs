using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springDestroy : MonoBehaviour
{
    int health = 2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bomb"))
        {
            print("bomb hit");
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
