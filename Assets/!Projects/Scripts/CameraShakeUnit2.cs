using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Unit2
{
    public class CameraShakeUnit2 :MonoBehaviour
    {
        // Camera Shake - called from GameManager - bark
        // rotates camera on z, two ways then stops, displays time on UI

        Quaternion oRotation;
        float rotateZAngle = 1;
        public Text readyText;

        void Start()
        {
            oRotation = transform.rotation;
        }

        public void CameraShaking(float duration)
        {
            InvokeRepeating("RotateCamera", 0, 0.5f);
            InvokeRepeating("RotateCamera2", 0.33f, 0.5f);

            StartCoroutine(WaitForSeconds(duration)); // cancel invokes/ end barking
        }
        IEnumerator WaitForSeconds(float time)
        {
            StartCoroutine(PrintWaitTime(time));
            yield return new WaitForSeconds(time);
            CancelInvoke("RotateCamera");
            CancelInvoke("RotateCamera2");
            transform.rotation = oRotation;
            GameManagerUnit2.SetIsBarking(false);
        }
        IEnumerator PrintWaitTime(float waitingTime)
        {
            PrintToReady(waitingTime);
            yield return new WaitForSeconds(1);
            if (waitingTime > 1)
            {
                StartCoroutine(PrintWaitTime(--waitingTime));
            }
            PrintToReady(0);
        }


        void RotateCamera()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotateZAngle);
        }
        void RotateCamera2()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -rotateZAngle);
        }

        void PrintToReady(float time)
        {
            if (time > 0)
            {
                readyText.text = $"Ready in {time} seconds";
            }
            else
            {
                readyText.text = "";
            }
        }
    }
}