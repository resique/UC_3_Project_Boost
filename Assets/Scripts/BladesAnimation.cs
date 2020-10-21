using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesAnimation : MonoBehaviour {
    [SerializeField]
    Transform[] blades;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float maxSpeed = 1000;
    [SerializeField]
    float minSpeed = 200;
    [SerializeField]
    float speedIncrement = 200;

    private void Start() {
  
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
      
                speed = maxSpeed * curve.Evaluate(Time.time * curve.length);
        } else {
          
                speed = minSpeed * curve.Evaluate(Time.time * curve.length);
        }

        for (uint i = 0; i < blades.Length; i++) {
            bool isEven = (i + 1) % 2 == 0;
            blades[i].Rotate((isEven ? Vector3.down : Vector3.up) * speed * Time.deltaTime);
        }
    }
}
