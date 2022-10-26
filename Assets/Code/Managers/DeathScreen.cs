using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class DeathScreen : MonoBehaviour
{
    GameObject canvas;
    TextMeshProUGUI message;
    public GameObject deathMenu;
    string defaultMessage = "Coco is now free to terrorize the city. The city burns at his mercy.";
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
        message = transform.Find("Canvas/EndDesc").GetComponent<TextMeshProUGUI>();
    }
    void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Dead)
        {
            if (GameStateManager.Instance.DeathMessage != null)
            {
                message.text = GameStateManager.Instance.DeathMessage;
            }
            else
            {
                message.text = defaultMessage;
            }
            canvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(deathMenu);
        }
        else {
            canvas.SetActive(false);
        }
    }
    public void handleRestart() {
        GameStateManager.Instance.TriggerRestart();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void handleQuit() {
        Application.Quit();
    }
}
