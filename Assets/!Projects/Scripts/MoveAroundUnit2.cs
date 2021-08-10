﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class MoveAroundUnit2 :MonoBehaviour
    {
        public float speed = 2;

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //print("rotating...");
            transform.Rotate(new Vector3(transform.rotation.eulerAngles.x, Random.Range(0,360), transform.rotation.eulerAngles.z));
        }
    }
}
