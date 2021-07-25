using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlesCol2 : MonoBehaviour
{
    float _speed = 50f;
    int i = 0;
    bool goingRight = true;

    void FixedUpdate()
    {
        if (i == 0)
        {
            goingRight = false;
        }
        else if (i == 120)
        {
            goingRight = true;
        }
        //gameObject.transform.position.x += 
        if (goingRight)
        {
            transform.Translate(new Vector3(1, 0, 0) * _speed * Time.deltaTime, Space.World);
            i--;
        }
        else if (!goingRight)
        {
            transform.Translate(new Vector3(-1, 0, 0) * _speed * Time.deltaTime, Space.World);
            
            i++;
        }
    }
}
