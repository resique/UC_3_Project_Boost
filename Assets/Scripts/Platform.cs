using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformState {
    active,
    inactive,
    completed
}

public class Platform : MonoBehaviour {
    [SerializeField]
    Material active;
    [SerializeField]
    Material inactive;
    [SerializeField]
    Material completed;
    [SerializeField]
    AudioClip platformActivatedSound;
    [SerializeField]
    ParticleSystem platformActivatedFx;

    AudioSource audioSource;
    public PlatformState state = PlatformState.inactive;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

    }

    public void switchState(PlatformState toState) {
        state = toState;
        setMaterialFoState(state);
    }

    void setMaterialFoState(PlatformState state) {
        switch (state) {
            case PlatformState.active:
                GetComponent<Renderer>().material = active;
                break;
            case PlatformState.inactive:
                GetComponent<Renderer>().material = inactive;
                break;
            case PlatformState.completed:
                GetComponent<Renderer>().material = completed;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (state == PlatformState.active) {
            state = PlatformState.completed;
            setMaterialFoState(state);
            audioSource.PlayOneShot(platformActivatedSound);
            platformActivatedFx.Play();
        }
        
    }

}
