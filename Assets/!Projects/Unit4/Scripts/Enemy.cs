using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRB;
    private GameObject player;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed); // normalize to stop speed from multiplying
    }
}
