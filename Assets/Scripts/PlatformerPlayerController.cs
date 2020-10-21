using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class PlatformerPlayerController : ExtendedMonoBehaviour {
    [SerializeField]
    float thrustSpeed = 20f;
    [SerializeField]
    float movementSpeed = 50f;
    [SerializeField]
    AudioClip mainEngineSound;
    [SerializeField]
    AudioClip crashSound;
    [SerializeField]
    ParticleSystem crashFx;
    [SerializeField]
    LayerMask groundLayers;
    [SerializeField]
    private bool isMovingAllowed = true;

    Rigidbody rigidBody;
    bool isOnTheGround = true;
    GameState gameState = GameState.active;
    private Vector3 inputVector;
    private bool isHorizontalAxisDown = false;
    private bool isVerticalAxisDown = false;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (gameState == GameState.diying) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround) {
            isOnTheGround = false;
            rigidBody.AddForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        MoveAlongAxis(InputAxis.horizontal, ref isHorizontalAxisDown);
        MoveAlongAxis(InputAxis.vertical, ref isVerticalAxisDown);
    }

    private Vector3 CalculateInputVector(InputAxis axis) {
        switch (axis) {
            case InputAxis.horizontal:
                return new Vector3(Input.GetAxisRaw(GetAxisStringName(axis)) * movementSpeed * Time.fixedDeltaTime, rigidBody.velocity.y, 0);
            case InputAxis.vertical:
                return new Vector3(0, rigidBody.velocity.y, Input.GetAxisRaw(GetAxisStringName(axis)) * movementSpeed * Time.fixedDeltaTime);
            default:
                return Vector3.zero;
        }
    }

    private void MoveAlongAxis(InputAxis axis, ref bool isAxisDown) {
        float axisValue = Input.GetAxisRaw(GetAxisStringName(axis));
        if (axisValue != 0 && !isAxisDown) {
            inputVector = CalculateInputVector(axis);
            Move();
            isAxisDown = true;
        } else if (axisValue == 0) {
            isAxisDown = false;
        }
    }

    private void Move() {
        if (!isMovingAllowed) return;
        rigidBody.velocity = inputVector;
    }

    private void OnCollisionEnter(Collision collision) {
        if (gameState != GameState.active) return;

        if (collision.gameObject.tag == TagFor(EditorTag.Ground)) {
            isOnTheGround = true;
        }
    }
}