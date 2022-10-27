using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    GameObject mainMenu;
    GameObject optionsMenu;
    public GameObject startMenu;
    public GameObject optionSelected, optionsClose;
    public GameObject cam;
    void Start()
    {
        GameStateManager.Instance.SetState(GameState.Cutscene);
        mainMenu = transform.Find("Canvas").gameObject;
        optionsMenu = transform.Find("OptionsMenu").gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startMenu);
    }
    public void handleStart() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        GameStateManager.Instance.SetState(GameState.Gameplay);
        cam.SetActive(false);
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
