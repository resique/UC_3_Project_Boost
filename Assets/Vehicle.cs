using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;
    bool isOnTheGround = false;
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        Thrust();
        Rotate();
    }

    void Rotate() {
        if (isOnTheGround) return;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
                audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }

    void OnCollisionStay(Collision collisionInfo) {
        isOnTheGround = true;
    }

    private void OnCollisionExit(Collision collision) {
        isOnTheGround = false;
    }
}