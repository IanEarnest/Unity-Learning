using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using TMPro;
namespace Tutorial
{

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] // show in inspector
    private float _speed = 40;
    [SerializeField]
    private float _horizontalInput;

    private string _horizontal = "Horizontal";

    [SerializeField] List<GameObject> _prefabsList;
    //TMP_Text _text;
    [SerializeField] UnityEvent OnCompleteEvent;



    // c# 7 expression body (property/method) - only difference is method = ()
    //      property into single line
    void Start()
    {
        if (ReadyToSpawn) // without brackets?
            SpawnPlayer();
    }
    //bool ReadyToSpawn()
    //{
    //    return true; // if Time.time > ...next spawn time
    //}
    //bool ReadyToSpawn
    //{
    //    get
    //    {
    //        return true;
    //    }
    //    set
    //    {
    //        ReadyToSpawn = value;
    //    }
    //}
    bool ReadyToSpawn => true; // if Time.time > ...next spawn time


    void SpawnPlayer()
    {
        print("player spawned"); // if Time.time > ...next spawn time
        OnCompleteEvent.Invoke();
    }

    // c# 7 null conditioner operator
    //      quick null check
    //_text?.SetText(...);

    // c# 7 string interpolation = print($"Spawned {playerName}");

    // Coroutine
    private Coroutine _coroutine;
    //IEnumerator Start()
    IEnumerator MyRepeats()
    {
        yield return new WaitForSeconds(1f);
        print("1 second waited");
    }
    IEnumerator WaitAFewSeconds()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(3f);
            print($"{i} seconds waited");
        }
        //
    }




    // Update is called once per frame
    void Update()
    {
        // Coroutine
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            else
            {
                _coroutine = StartCoroutine(WaitAFewSeconds());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StopCoroutine(_coroutine);
        }


        //transform.Translate(Vector3.right * _speed * Time.deltaTime); // meters per second
        _horizontalInput = Input.GetAxis(_horizontal);
        transform.Translate(new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime, Space.World); // meters per second
        transform.Rotate(new Vector3(0,0, (1 * _speed * Time.deltaTime)));

        // pause
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 0.5f;
        }
        // resume
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Time.timeScale += 1;
        }
    }
}
}