using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit2
{
    public class MoveForward : MonoBehaviour
    {
        public float speed = 40.0f;
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void Barked()
        {
            speed = 1;
            Transform myT = transform.Find("Sweat");
            if (myT != null)
            {
                myT.gameObject.SetActive(true);
            }
        }
    }
}
