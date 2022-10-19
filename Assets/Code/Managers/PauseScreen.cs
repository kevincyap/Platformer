using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    GameObject canvas;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
    }
    void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            canvas.SetActive(true);
        }
        else {
            canvas.SetActive(false);
        }
    }
    public void handleRestart() {
        GameStateManager.Instance.SetState(GameState.Gameplay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void handleQuit() {
        Application.Quit();
    }
}
