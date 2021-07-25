using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private variables
    public float speed = 20.0f;
    public float minSpeed = 20.0f;
    public float maxSpeed = 80.0f;
    public float turnSpeed = 45.0f;
    public float minTurnSpeed = 45.0f;
    public float maxTurnSpeed = 200.0f;
    private float horizontalInput;// = -1..1;
    private float forwardInput;// = -1..1;
    public static int frame = 0; // used in FollowPlaye.cs aswell
    string obstacleName = "Obstacle";
    Vector3 normal = Vector3.up;
    Vector3 inverse = Vector3.down;
    Vector3 turning = Vector3.up;
    Quaternion playerOldRotation;
    Vector3 playerOldPosition;
    public bool ResetRotationHeight;// = -1..1;
    public bool isSecondPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerOldRotation = transform.rotation;
        playerOldPosition = transform.position;
    }




    // Update is called once per frame
    void Update() // FixedUpdate
    {

        // added:
        // speed increase and turning speed
        // stop rotating when not moving
        // destroy obstacles
        // bigger road
        // reverse inverse movement
        // reset rotation = if x or z rotation not 0
        // reset height = y < 5  or > 10 = 3

        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetRotationHeight = !ResetRotationHeight;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = playerOldPosition;
        }

        if (ResetRotationHeight)
        {
            ResetRotation();
            ResetHeight();
        }
        if (!isSecondPlayer)
        {
            PlayerInput();
        }
        else
        {
            SecondPlayerInput();
        }
        PlayerMove();

        Frame();
        Debugging();
    }

    private void ResetRotation()
    {
        // flip = if x or z rotation not 0
        if (//transform.rotation.eulerAngles.x > 100 || transform.rotation.eulerAngles.x < -100 ||
            transform.rotation.eulerAngles.z > 100 || transform.rotation.eulerAngles.z < -100)
        {
            transform.rotation = playerOldRotation;
            print($"Rotation reset");
        }
        //print($"X:{transform.rotation.eulerAngles.x}");
        //print($"Z:{transform.rotation.eulerAngles.z}");
    }
    private void ResetHeight()
    {
        // relocate height = y < 5  or > 10 = 3
        //print(transform.position.y);
        if (transform.position.y < -10 || transform.position.y > 20)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 0;
            transform.position = newPosition;
            transform.rotation = playerOldRotation;
            print($"Position reset");
        }
    }

    // mesh collider, // Destroy = on collision with Obstacle //OnTriggerEnter
    private void OnTriggerEnter(Collider other) //OnCollisionEnter
    {
        if (other.gameObject.name.Contains(obstacleName))//CompareTag("Collider"))
        {
            Debug.Log($"Destroyed: {other.gameObject.name}");
            Destroy(other.gameObject);
        }
    }

    private void PlayerInput()
    {
        // Player input
        horizontalInput = Input.GetAxis("HorizontalPlayer1");
        forwardInput = Input.GetAxis("VerticalPlayer1");


        // backwards
        if (forwardInput < 0)
        {
            speed = minSpeed;
            turnSpeed = minTurnSpeed;
            turning = inverse;
        }
        else
        {
            turning = normal;
        }
        if (forwardInput == 1) //|| forwardInput == -1)
        {
            // max acceleration
            if (speed < maxSpeed)
            {
                speed++;
            }
            if (turnSpeed < maxTurnSpeed)
            {
                turnSpeed++;
            }
        }
        else
        {
            if (speed > minSpeed)
            {
                speed--;
                transform.Translate(Vector3.forward * Time.deltaTime * speed * 0.5f);
            }
            if (turnSpeed > minTurnSpeed)
            {
                turnSpeed--;
            }
        }
    }
    private void SecondPlayerInput()
    {
        // Player input
        horizontalInput = Input.GetAxis("HorizontalPlayer2");
        forwardInput = Input.GetAxis("VerticalPlayer2");

        // backwards
        if (forwardInput < 0)
        {
            speed = minSpeed;
            turnSpeed = minTurnSpeed;
            turning = inverse;
        }
        else
        {
            turning = normal;
        }
        if (forwardInput == 1) //|| forwardInput == -1)
        {
            // max acceleration
            if (speed < maxSpeed)
            {
                speed++;
            }
            if (turnSpeed < maxTurnSpeed)
            {
                turnSpeed++;
            }
        }
        else
        {
            if (speed > minSpeed)
            {
                speed--;
                transform.Translate(Vector3.forward * Time.deltaTime * speed * 0.5f);
            }
            if (turnSpeed > minTurnSpeed)
            {
                turnSpeed--;
            }
        }
    }
    private void PlayerMove()
    {
        // Move the vehicle vertical/ forward
        transform.Translate(Vector3.forward * speed * forwardInput * Time.deltaTime);

        // Move the vehicle horizontal/ turn
        if (forwardInput != 0)
        {
            transform.Rotate(turning * turnSpeed * horizontalInput * Time.deltaTime);
        }
    }
    private void Frame()
    {
        // Debug info
        if (frame % 100 == 0) // print every second or so
        {
            frame = 0;
        }
        frame++; // used in FollowPlaye.cs aswell
    }
    private void Debugging()
    {
        if (frame % 100 == 0) // print every second or so
        {
            print($"Speed: {speed}, " +
                            $"turnSpeed: {turnSpeed}, " +
                            $"horizontalInput: {horizontalInput}, " +
                            $"forwardInput: {forwardInput}");
        }
    }
}
