using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject optionsMenu;
    public GameObject pauseSelected, optionSelected, optionsClose;

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

    void Update() {
        if (gameObject.activeSelf == true && Input.GetButtonDown("Dash")) {
            if (optionsMenu.activeSelf == true) {
                handleCloseOptions();
            } else {
                GameStateManager.Instance.SetState(GameState.Gameplay);
            }
        }
    }

    void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseSelected);
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionSelected);
    }
    public void handleCloseOptions() {
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClose);
    }
}
