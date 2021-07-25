using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostProcessing
{
public class Crate : MonoBehaviour
{
    public GameObject fracturedCrate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            // instantiate explosion effect too

            // Destroy old crate, put in new fractured crate
            // take all parts of crate and explode
            GameObject fracturedCrateObj = Instantiate(fracturedCrate, transform.position, Quaternion.identity); // as GameObject // not needed anymore
            Rigidbody[] allRigidBodies = fracturedCrateObj.GetComponentsInChildren<Rigidbody>();
            if (allRigidBodies.Length > 0)
            {
                foreach (var body in allRigidBodies)
                {
                    body.AddExplosionForce(500, transform.position, 1);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
}