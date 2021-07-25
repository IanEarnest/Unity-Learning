using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlesCol4 : MonoBehaviour
{
    float _speed = 25f;
    int i = 0;
    bool goingRight = true;

    void FixedUpdate()
    {
        if (i == 0)
        {
            goingRight = true;
        }
        else if (i == 120)
        {
            goingRight = false;
        }
        //gameObject.transform.position.x += 
        if (goingRight)
        {
            transform.Translate(new Vector3(1, 0, 0) * _speed * Time.deltaTime, Space.World);
            i++;
        }
        else if (!goingRight)
        {
            transform.Translate(new Vector3(-1, 0, 0) * _speed * Time.deltaTime, Space.World);
            i--;
        }
    }
}
