using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit4
{
    public class PlayerController : MonoBehaviour
    {
        /*
        PlayerController
           attach to player
           playerRb, speed, getRigidbody
           float forwardInput
           rb.addforce
        PlayerController
           focalPoint - find
        */
        private Rigidbody playerRb;
        private GameObject focalPoint;
        public float speed = 5.0f;
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
        }
        void Update()
        {
            float forwardInput = Input.GetAxis("Vertical");
            //playerRb.AddForce(Vector3.forward * speed * forwardInput);
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }
}
}
