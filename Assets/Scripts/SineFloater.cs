using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct BoolVector3 {
    public bool x;
    public bool y;
    public bool z;

    public BoolVector3(bool x, bool y, bool z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override bool Equals(object obj) {
        if (!(obj is BoolVector3))
            return false;

        BoolVector3 vec = (BoolVector3)obj;
        return vec.x == this.x && vec.y == this.y && vec.z == this.z;
    }
    public static bool operator ==(BoolVector3 a, BoolVector3 b) {
        return a.Equals(b);
    }
    public static bool operator !=(BoolVector3 a, BoolVector3 b) {
        return !a.Equals(b);
    }
}

public class SineFloater : MonoBehaviour {
    [SerializeField] float period = 2f;
    [SerializeField] float amplitude = 2f;
    [SerializeField] BoolVector3 movementAxis = new BoolVector3(false, true, false);

    private BoolVector3 _movementAxis;
    private bool IsMovementVectorChanged => movementAxis != _movementAxis;

    private float movementFactor;
    private Vector3 initialPosition;
    private Vector3 movementVector;

    void Start() {
        initialPosition = transform.position;
        calculateMovementVector();
    }

    private void calculateMovementVector() {
        movementVector = new Vector3((movementAxis.x ? 1 : 0), (movementAxis.y ? 1 : 0), (movementAxis.z ? 1 : 0));
        _movementAxis = movementAxis;
    }

    void Update() {
        if (IsMovementVectorChanged) {
            calculateMovementVector();
        }

        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;

        movementFactor = Mathf.Sin(cycles * tau);
        Vector3 offset = movementFactor * (movementVector * amplitude);
        transform.position = initialPosition + offset;
    }
}
