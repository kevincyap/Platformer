using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    GameObject canvas;
    TextMeshProUGUI message;
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
            message.text = GameStateManager.Instance.DeathMessage ?? defaultMessage;
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
