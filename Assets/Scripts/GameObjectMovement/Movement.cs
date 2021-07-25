using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameObjectMovement
{
public class Movement :MonoBehaviour
{
    public GameObject[] sphereList;
    float speed = 3;
    float speedRigid = 150;
    float speedMulti = 2;
    Rigidbody rb1, rb2, rb3;

    void Awake()
    {
        rb1 = sphereList[0].GetComponent<Rigidbody>();
        rb2 = sphereList[1].GetComponent<Rigidbody>();
        rb3 = sphereList[2].GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Rigidbody SetVelocity       - physics           - .velocity =
        Rigidbody MovePosition                          - .MovePosition(
        Rigidbody AddForce          - physics           - .AddForce(
        Transform Translate                             - .transform.Translate(
        Transform SetPosition       - used most         - .transform.position +=
        */

        // Rigidbody SetVelocity - sphereList[0]
        rb1.velocity = sphereList[0].transform.forward * (speedRigid*speedMulti) * Time.deltaTime; // set in Awake, doesn't need update, cannot be kinematic

        // Rigidbody MovePosition - sphereList[1]
        Vector3 newPos = sphereList[1].transform.position + (sphereList[1].transform.forward * (speed*speedMulti) * Time.deltaTime);
        rb2.MovePosition(newPos);

        // Rigidbody AddForce - sphereList[2]
        rb3.AddForce(sphereList[3].transform.forward * (speedRigid/speedMulti) * Time.deltaTime); // * forceMult ... = 5, cannot be kinematic

        // Transform Translate - sphereList[3]
        sphereList[3].transform.Translate(Vector3.forward * speed * Time.deltaTime); //, Space.World

        // Transform SetPosition - sphereList[4], used most
        sphereList[4].transform.position += sphereList[4].transform.forward * speed * Time.deltaTime;

    }
}
}
