using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownLab : MonoBehaviour
{
    public float speed = 20;
    float bound = -10;
    Rigidbody objectRb;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.back * speed);
        if (transform.position.z < bound)
        {
            Destroy(gameObject);
        }
    }
}
