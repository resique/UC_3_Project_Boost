using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFloater : MonoBehaviour {
    [SerializeField]
    AnimationCurve curve;
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
        
        movementFactor = curve.Evaluate((Time.time * curve.length));
        Vector3 offset = movementFactor * (movementVector * amplitude);
        transform.position = initialPosition + offset;
    }
}
