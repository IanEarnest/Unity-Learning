using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBomb : MonoBehaviour
{
    public GameObject bomb;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBomb = Instantiate(bomb, spawn.transform.position, Quaternion.identity);
            Destroy(newBomb, 10);
        }
    }
}
