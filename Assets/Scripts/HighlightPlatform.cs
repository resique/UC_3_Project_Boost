using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPlatform : MonoBehaviour {
    [SerializeField]
    Material active;
    [SerializeField]
    Material inactive;

    void Start() {

    }

    void Update() {

    }

    private void OnCollisionEnter(Collision collision) {
        GetComponent<Renderer>().material = active;
    }
}
