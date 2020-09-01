using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesAnimation : MonoBehaviour {
    public Transform[] blades;
    public float speed = 0;
    Rigidbody rigidBody;
    float maxAltitude, minAltitude;
    public float maxSpeed = 1000;
    public float minSpeed = 200;
    public float speedIncrement = 200;
    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        maxAltitude = transform.position.y;
        minAltitude = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        /* if (transform.position.y > maxAltitude) {
             speed += 150 * Time.deltaTime;
         }
         if (transform.position.y < maxAltitude) {
             speed -= 150 * Time.deltaTime;
         }*/
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
            blades[i].Rotate((i % 2 == 0 ? Vector3.down : Vector3.up) * Time.deltaTime * speed);
        }
    }
}
