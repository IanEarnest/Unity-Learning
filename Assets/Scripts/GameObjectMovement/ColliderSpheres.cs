using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectMovement
{
    public class ColliderSpheres :MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        }
    }
}
