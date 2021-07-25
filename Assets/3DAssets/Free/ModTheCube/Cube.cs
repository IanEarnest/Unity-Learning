using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ModTheCube
{

    public class Cube :MonoBehaviour
    {
        public MeshRenderer Renderer;
        // 5 x 5 x 5 position
        // 360... rotation
        // 5... scale
        // 0-1... colour //255... colour
        float _posX, _posY, _posZ;
        float _rotX, _rotY, _rotZ;
        float _scaleX, _scaleY, _scaleZ;
        float _colourR, _colourG, _colourB, _colourA;

        float _posMin = -5, _posMax = 5;
        float _rotMin = 1, _rotMax = 360;
        float _scaleMin = 1, _scaleMax = 10;
        float _colourMin = 0, _colourMax = 1;
        float _rotSpeed = 0.001f;
        float _rotSpeedMin = 0f, _rotSpeedMax = 0.01f;
        int _timeSinceLastChange = 0;
        int _halfASecond = 30;

        void Start()
        {
            //transform.position = new Vector3(3, 4, 1);
            //transform.localScale = Vector3.one * 1.3f;
            //material = Renderer.material;
            //material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);

            SetRandomValues();
        }


        void Update()
        {
            _timeSinceLastChange++;
            // location, rotation, scale, material colour, opacity, random for each scene, space to change?
            transform.position = new Vector3(_posX, _posY, _posZ);
            //transform.Translate();
            transform.Rotate(_rotSpeed * _rotX, _rotSpeed * _rotY, _rotSpeed * _rotZ); //* Time.deltaTime
            transform.localScale = new Vector3(_scaleX, _scaleY, _scaleZ);
            Renderer.material.color = new Color(_colourR, _colourG, _colourB, _colourA);

            //Input.GetKeyDown(KeyCode.Space) ||
            if (Input.GetKey(KeyCode.Space) && _timeSinceLastChange > _halfASecond) // half second
            {
                SetRandomValues();
            }
        }

        private void SetRandomValues()
        {
            _timeSinceLastChange = 0;
            _rotSpeed = Random.Range(_rotSpeedMin, _rotSpeedMax);

            _posX = Random.Range(_posMin, _posMax);
            _posY = Random.Range(_posMin, _posMax);
            _posZ = Random.Range(_posMin, _posMax);

            _rotX = Random.Range(_rotMin, _rotMax);
            _rotY = Random.Range(_rotMin, _rotMax);
            _rotZ = Random.Range(_rotMin, _rotMax);

            _scaleX = Random.Range(_scaleMin, _scaleMax);
            _scaleY = Random.Range(_scaleMin, _scaleMax);
            _scaleZ = Random.Range(_scaleMin, _scaleMax);

            _colourR = Random.Range(_colourMin, _colourMax);
            _colourG = Random.Range(_colourMin, _colourMax);
            _colourB = Random.Range(_colourMin, _colourMax);
            _colourA = Random.Range(_colourMin, _colourMax);
            //print($"{_colourR}, {_colourG}, {_colourB}, {_colourA}");
        }
    }
}
