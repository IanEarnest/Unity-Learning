using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Powerup :MonoBehaviour
    {
        [SerializeField] // show in inspector
        private float _moveSpeed = 100;
        [SerializeField] private float _rotSpeed  = 100;
        [SerializeField] private float _scaleSpeed = 100;
        [SerializeField] private float _horizontalInput;
        public Vector3 positionChange;
        float posCX = -0.002f;
        float posCXPositive = 0.002f;
        float posCXNegative = -0.002f;

        float posCY = 0;
        float posCZ = 0;
        public Vector3 rotationChange;
        float rotCX = 1;
        float rotCY = 0;
        float rotCZ = 0;
        public Vector3 scaleChange;
        float scaCX = 0.001f;
        float scaCY = 0.001f;
        float scaCZ = 0.001f;
        float scaCPositive = 0.001f;
        float scaCNegative = -0.001f;

        float targetRight = 3;
        float targetLeft = 3;


        //private string _horizontal = "Horizontal";

        // Start is called before the first frame update
        void Start()
        {
            targetLeft = transform.position.x - targetLeft; // based on original position
            targetRight = transform.position.x + targetRight;

            UpdateVectors();
        }

        // Update is called once per frame
        void Update()
        {
            //_speed *= Time.deltaTime;
            //transform.Translate(Vector3.right * _speed * Time.deltaTime); // meters per second
            //_horizontalInput = Input.GetAxis(_horizontal);
            //transform.Translate(new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime); // meters per second
            //transform.Rotate(Vector3.left * _speed * Time.deltaTime);
            transform.Rotate(rotationChange);
            transform.position += positionChange;
            transform.localScale += scaleChange;

            CheckValues();
        }

        void UpdateVectors()
        {
            positionChange = (new Vector3(posCX, posCY, posCZ) * _moveSpeed * Time.deltaTime);
            rotationChange = (new Vector3(rotCX, rotCY, rotCZ) * _rotSpeed * Time.deltaTime);
            scaleChange = (new Vector3(scaCX, scaCY, scaCZ) * _scaleSpeed * Time.deltaTime);
        }

        void CheckValues()
        {
            // Position
            if (transform.position.x > targetRight)
            {
                //print("changing position");
                posCX = posCXNegative;
            }
            else if (transform.position.x < targetLeft)
            {
                posCX = posCXPositive;
            }

            // Scale
            if (transform.localScale.x > 2)
            {
                //print("changing scale");
                scaCX = scaCNegative;
                scaCY = scaCNegative;
                scaCZ = scaCNegative;
            }
            else if (transform.localScale.x < 1)
            {
                scaCX = scaCPositive;
                scaCY = scaCPositive;
                scaCZ = scaCPositive;
            }

            UpdateVectors();
        }
    }
}