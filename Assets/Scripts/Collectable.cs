using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{

    public class Collectable :MonoBehaviour
    {
        //OnCollisionEnter when using collider not trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                // Add points
                // Add power up ability
                Destroy(this.gameObject);
            }
        }
    }
}