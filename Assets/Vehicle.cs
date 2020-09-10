using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public enum GameState
{
	active,
	diying,
	transcending,
}

public class Vehicle : MonoBehaviour {
    [SerializeField]
    float thrustSpeed = 100f;
    [SerializeField]
    AudioClip mainEngineSound;
    [SerializeField]
    AudioClip crashSound;
    Rigidbody rigidBody;
    AudioSource audioSource;
    bool isOnTheGround = false;
	List<GameObject> platforms;
	GameState gameState = GameState.active;
	int currentPlatformIndex;
	GameObject currentPlatform;

	void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
		platforms = GameObject.FindGameObjectsWithTag("Platform").ToList();
		assignNewCurrentPlatfrom();
	}

    void Update() {
        if (gameState == GameState.diying) {
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

	public void platformReached()
	{
		platforms.RemoveAt(currentPlatformIndex);
		assignNewCurrentPlatfrom();
	}

	private void assignNewCurrentPlatfrom()
	{
		if (platforms.Count == 0)
		{
			currentPlatform = null;
			return;
		}
		currentPlatformIndex = Random.Range(0, platforms.Count);
		currentPlatform = platforms[currentPlatformIndex];
		currentPlatform.GetComponent<Platform>().switchState(PlatformState.active);
	}

	private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngineSound);
            } 
        } else {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (gameState != GameState.active) return;

        switch (collision.gameObject.tag) {
            case "Platform":
                if (collision.gameObject.GetComponent<Platform>().state == PlatformState.active) {
                    platformReached();
                }
                break;
            case "Ground":
				gameState = GameState.diying;
                audioSource.Stop();
                audioSource.PlayOneShot(crashSound);
                Invoke("OnDie", 1.0f);
                break;
        }
    }

    void OnCollisionStay(Collision collisionInfo) {
        isOnTheGround = true;
    }

    private void OnCollisionExit(Collision collision) {
        isOnTheGround = false;
    }

    private void OnDie() {
		gameState = GameState.active;
		SceneManager.LoadScene(0);
    }
}