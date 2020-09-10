using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesAnimation : MonoBehaviour {
    [SerializeField]
    Transform[] blades;
    [SerializeField]
    float speed = 0;
    [SerializeField]
    float maxSpeed = 1000;
    [SerializeField]
    float minSpeed = 200;
    [SerializeField]
    float speedIncrement = 200;

    Rigidbody rigidBody;
    float maxAltitude, minAltitude;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        maxAltitude = transform.position.y;
        minAltitude = transform.position.y;
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            if (speed < maxSpeed)
                speed += speedIncrement * Time.deltaTime;
            else
                speed = maxSpeed;
        } else {
            if (speed > minSpeed)
                speed -= speedIncrement * Time.deltaTime;
            else
                speed = minSpeed;
        }

        for (uint i = 0; i < blades.Length; i++) {
            bool isEven = i % 2 == 0;
            blades[i].Rotate((isEven ? Vector3.down : Vector3.up) * Time.deltaTime * speed);
        }
    }
}
