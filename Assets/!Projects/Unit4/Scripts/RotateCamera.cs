using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class RotateCamera :MonoBehaviour
    {
        /*
        RotateCamera
	        rotate around level (center)
        */
        public float rotationSpeed;
        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}
