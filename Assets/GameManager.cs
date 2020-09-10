using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    active,
    diying,
    transcending,
}

public class GameManager : MonoBehaviour {
    #region PROPERTIES
    private static readonly GameManager _instance = new GameManager();
    public static GameManager instance {
        get { return _instance; }
    }

    List<Transform> _platforms;
    [SerializeField]
    List<Transform> platforms;
    Transform currentPlatform;
    int currentPlatformIndex;
    GameState _gameState = GameState.active;
    public GameState gameState {
        get {
            return _gameState;
        }

        set {
            _gameState = value;
            OnGameStateChanged();
        }
    }
    int currentSceneId = 0;
    #endregion

    #region UNITY
    void Start() {
        _platforms = platforms;
        assignNewCurrentPlatfrom();
    }
    void Update() {
        if (platforms.Count == 0) {
            print("WIN");
            return;
        }
    }
    #endregion

    #region PUBLIC
    public void platformReached() {
        _platforms.RemoveAt(currentPlatformIndex);
        assignNewCurrentPlatfrom();
    }
    #endregion

    #region PRIVATE
    private void OnGameStateChanged() {
        switch (_gameState) {
            case GameState.active:
                break;
            case GameState.diying:
                OnDie();
                break;
            case GameState.transcending:
                break;
            default:
                break;
        }
    }

    private void OnDie() {
        StartCoroutine(LoadScene(currentSceneId, 2.0f));
    }

    private IEnumerator LoadScene(int sceneId, float delay) {
        SceneManager.LoadScene(sceneId);
        yield return new WaitForSeconds(delay);
    }

    private void assignNewCurrentPlatfrom() {
        if (_platforms.Count == 0) {
            currentPlatform = null;
            return;
        }
        currentPlatformIndex = Random.Range(0, _platforms.Count);
        currentPlatform = _platforms[currentPlatformIndex];
        currentPlatform.GetComponent<Platform>().switchState(PlatformState.active);
    }
    #endregion
}
