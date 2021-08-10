using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Unit2
{
    public class CameraShakeUnit2 :MonoBehaviour
    {
        //Vector3 oPosition;
        Quaternion oRotation;
        //float rotateStartTime = 0.33f;
        float rotateZAngle = 1;
        public Text readyText;

        void Start()
        {
            //oPosition = transform.position;
            oRotation = transform.rotation;
        }

        void Update()
        {
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

        IEnumerator RotateAfterTime(float time, float rotation)
        {
            yield return new WaitForSeconds(time);
            //transform.Rotate()
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation);
            //transform.rotation = oRotation;
        }
    }
}