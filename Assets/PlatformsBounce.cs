using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsBounce : MonoBehaviour {
    [SerializeField]
    Transform[] transforms;
    [SerializeField]
    float speed = 50f;

    [SerializeField]
    float height = 0.5f;
    [SerializeField]
    AnimationCurve myCurve;

    void Start() {
        
    }

    void Update() {
        for (var i = 0; i < transforms.Length; i++) {
            float yMovement = (i % 2 == 0) ? 1f : -1f;
            // transforms[i].position = new Vector3(transforms[i].position.x, Mathf.SmoothStep(10, 20, Time.deltaTime) * yMovement, transforms[i].position.z);
            transforms[i].position = new Vector3(transforms[i].position.x, (Mathf.Sin(Time.deltaTime * speed) * height) + transforms[i].position.y/*transforms[i].position.y + (myCurve.Evaluate((Time.deltaTime % myCurve.length)) * yMovement)*/, transforms[i].position.z);
            //get the objects current position and put it in a variable so we can access it later with less code
            //Vector3 pos = transform.position;
            //calculate what the new Y position will be
           // float newY = Mathf.Sin(Time.deltaTime * speed);
            //set the object's Y to the new calculated Y
           // transform.position = new Vector3(pos.x, newY * height, pos.z);
        }
    }
}
