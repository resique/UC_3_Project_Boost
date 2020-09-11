using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour {
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    float height;
    [SerializeField]
    float speed;

    float yPos;
    void Start() {
        yPos = transform.position.y;
    }

    void Update() {
        float direction = 0f;
        if (transform.position.y > (yPos + height)) {
            direction = -1f;
        }
        if (transform.position.y < (yPos - height)) {
            direction = 1f;
        }
        transform.position = new Vector3(transform.position.x, curve.Evaluate((Time.time * curve.length)) * direction, transform.position.z);
    }
}
