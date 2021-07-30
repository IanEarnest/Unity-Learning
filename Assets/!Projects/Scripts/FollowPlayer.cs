using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unit1
{
    public class FollowPlayer :MonoBehaviour
    {
        public GameObject player;
        public GameObject playerCameraObject;
        public GameObject playerFrontCameraObject;
        Vector3 offset = new Vector3(0, 30, 50);//(4,75,-90);//(0,5,-7);
        Camera mainCamera;
        AudioListener mainAudioListener;
        Camera playerCamera;
        AudioListener playerAudioListener;
        Camera playerFrontCamera;
        AudioListener playerFrontAudioListener;
        int coolDown = 0; //50
        int coolDownMax = 50;

        // Start is called before the first frame update
        void Start()
        {
            // set only vehicle camera active
            MainCamaraSetup(); // disabled
            PlayerCamaraSetup(); // enabled
            PlayerFrontCamaraSetup(); // disabled
        }

        // Update is called once per frame
        void Update()
        {
            PlayerInput();
            CameraMove();
            if (coolDown > 0)
            {
                coolDown--;
            }
            //Debugging();
        }

        void MainCamaraSetup()
        {
            //this.gameObject.SetActive(false); //this.enabled = false;
            mainCamera = (Camera)GetComponent("Camera");
            //mainAudioListener = (AudioListener)GetComponent("AudioListener");

            mainCamera.enabled = !mainCamera.enabled;
            //mainAudioListener.enabled = !mainAudioListener.enabled;
        }
        void PlayerCamaraSetup()
        {
            playerCamera = (Camera)playerCameraObject.GetComponent("Camera");
            //playerAudioListener = (AudioListener)playerCameraObject.GetComponent("AudioListener");

            //playerCamera.enabled = !playerCamera.enabled;
            //playerAudioListener.enabled = !playerAudioListener.enabled;
        }
        void PlayerFrontCamaraSetup()
        {
            playerFrontCamera = (Camera)playerFrontCameraObject.GetComponent("Camera");
            //playerFrontAudioListener = (AudioListener)playerFrontCameraObject.GetComponent("AudioListener");

            playerFrontCamera.enabled = !playerFrontCamera.enabled;
            //playerFrontAudioListener.enabled = !playerFrontAudioListener.enabled;
        }
        int cameraSelect = 0;
        void CameraEnableFlip()
        {
            cameraSelect++;
            if (cameraSelect > 2)
            {
                cameraSelect = 0;
            }
            switch (cameraSelect)
            {
                case 0:
                    EnableCamera(mainCamera);
                    break;
                case 1:
                    EnableCamera(playerCamera);
                    break;
                case 2:
                    EnableCamera(playerFrontCamera);
                    break;
                default:
                    break;
            }
        }
        void EnableCamera(Camera camera)
        {
            //playerCamera.enabled = !playerCamera.enabled; //playerCamera.SetActive(!playerCamera.activeSelf);
            mainCamera.enabled = false;
            playerFrontCamera.enabled = false;
            playerCamera.enabled = false;
            camera.enabled = true;
        }

        private void PlayerInput()
        {
            if (Input.GetAxis("Jump") == 1 && coolDown == 0)
            {
                coolDown = coolDownMax;
                CameraEnableFlip();
            }
        }
        private void CameraMove()
        {
            // for main camera, when not using attached camera of car
            transform.position = player.transform.position + offset;
        }
        private void Debugging()
        {
            // Debug
            if (PlayerController.frame % 100 == 0) // every second or so
            {
                print($"Jump: {Input.GetAxis("Jump")}, Cooldown: {coolDown}");
            }
            //print($"Jump: {Input.GetAxis("Jump")}");
        }
    }
}