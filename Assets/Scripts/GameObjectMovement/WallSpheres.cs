using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectMovement
{
    public class WallSpheres :MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
    }
}