using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX2 : MonoBehaviour
{
    public GameObject dogPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            // stop space spam
            nextFire = Time.time + fireRate;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
