using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
    Rigidbody rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessInput();
    }

    void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) {
            print("Rotate Left");
        } else if (Input.GetKey(KeyCode.D)) {
            print("Rotate Right");
        }
    }
}