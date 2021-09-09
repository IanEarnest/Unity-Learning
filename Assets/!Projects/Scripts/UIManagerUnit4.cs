using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unit4
{
    public class UIManagerUnit4 : MonoBehaviour
    {

        public TextMeshProUGUI powerUpText;
        public TextMeshProUGUI WaveText;
        string powerUpTextPre = "Power Up: ";
        string waveTextPre = "Wave: ";
        WaveUnit4 _waveUnit4;
        PowerUpsUnit4 _powerUpsUnit4;
        PlayerControllerUnit4 _playerControllerUnit4;

        private void Start()
        {
            _waveUnit4 = GameObject.Find("Game Manager").GetComponent<WaveUnit4>();
            _powerUpsUnit4 = GameObject.Find("Game Manager").GetComponent<PowerUpsUnit4>();
            _playerControllerUnit4 = GameObject.Find("Player").GetComponent<PlayerControllerUnit4>();
        }
        void Update()
        {
            if (_playerControllerUnit4.activePowerup != 0)
            {
                powerUpText.text = $"{powerUpTextPre} {_playerControllerUnit4.activePowerup}, {Enum.GetName(typeof(PowerUpsUnit4.powerupType), _playerControllerUnit4.activePowerup)}, {_powerUpsUnit4.powerupTimerTMP}";
            }
            else
            {
                powerUpText.text = powerUpTextPre;
            }
            //Enum.GetName(typeof(PowerUpsUnit4.powerupType), _playerControllerUnit4.activePowerup)
            //or
            //(PowerUpsUnit4.powerupType)_playerControllerUnit4.activePowerup

            //print($"Power up {powerUp} {Enum.GetName(typeof(powerupType), powerUp)}, {(powerupType)powerUp}"); //powerupType enum?
            //public int activePowerup = 0;

            WaveText.text = $"{waveTextPre} {_waveUnit4.waveNumber}";
        }
    }
}