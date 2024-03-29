using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStateManager();
 
            return _instance;
        }
    }
 
    public GameState CurrentGameState { get; private set; } = GameState.Cutscene;
    public string DeathMessage { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    public delegate void GameStateReset();
    public event GameStateReset OnGameStateReset;
 
    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;
 
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }

    public void SetStateWithMessage(GameState newGameState, string message)
    {
        DeathMessage = message;
        SetState(newGameState);
    }

    public void TriggerRestart() {
        SetState(GameState.Gameplay);
        OnGameStateReset?.Invoke();
    }

}
