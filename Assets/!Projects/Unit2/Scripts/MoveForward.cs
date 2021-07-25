using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class MoveForward : MonoBehaviour
    {
        public float speed = 40.0f;
        float bound = 30f;

        void Update()
        {
            if (transform.position.z > bound || transform.position.z < -bound)
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
