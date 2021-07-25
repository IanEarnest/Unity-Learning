using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    float speed = 20f;
    float rotationSpeed = 200;
    float propellerSpeed = 100;
    public GameObject propeller;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.left * verticalInput * rotationSpeed * Time.deltaTime);//Time.deltaTime);

        // spin faster the more you press up
        if (verticalInput > 0)
        {
            propellerSpeed = (1000 * verticalInput);
        }
        else
        {
            propellerSpeed = 500;
        }
        // propeller spin
        propeller.transform.Rotate(Vector3.forward * propellerSpeed * Time.deltaTime);
    }
}
