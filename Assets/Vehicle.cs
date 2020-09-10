using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
    [SerializeField]
    float thrustSpeed = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;
    bool isOnTheGround = false;
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (GameManager.instance.gameState == GameState.diying) {
            return;
        }
        Thrust();
        Rotate();
    }

    void Rotate() {
        if (isOnTheGround) return;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.back);
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Rotate(Vector3.right);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Rotate(Vector3.left);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.down);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)
                audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Platform":
                print("PLATFORM");
                break;
            case "Ground":
                GameManager.instance.gameState = GameState.diying;
                break;
        }
    }

    void OnCollisionStay(Collision collisionInfo) {
        isOnTheGround = true;
    }

    private void OnCollisionExit(Collision collision) {
        isOnTheGround = false;
    }
}