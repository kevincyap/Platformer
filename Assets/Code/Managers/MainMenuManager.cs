using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    GameObject mainMenu;
    GameObject optionsMenu;
    public GameObject cam;
    void Start()
    {
        GameStateManager.Instance.SetState(GameState.Cutscene);
        mainMenu = transform.Find("Canvas").gameObject;
        optionsMenu = transform.Find("OptionsMenu").gameObject;
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
    }
    public void handleCloseOptions() {
        optionsMenu.SetActive(false);
    }
}
