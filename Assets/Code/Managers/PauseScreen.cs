using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject optionsMenu;

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
        pauseMenu = transform.Find("PauseMenu").gameObject;
        optionsMenu = transform.Find("OptionsMenu").gameObject;
    }
    void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        else {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
    }
    public void handleRestart() {
        GameStateManager.Instance.TriggerRestart();
    }
    public void handleQuit() {
        Application.Quit();
    }
    public void handleOpenOptions() {
        optionsMenu.SetActive(true);
    }
    public void handleCloseOptions() {
        optionsMenu.SetActive(false);
    }
}
