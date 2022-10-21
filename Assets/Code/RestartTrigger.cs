using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTrigger : MonoBehaviour
{
    public bool DestroyPlayer = false;
    public string DeathMessage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (DestroyPlayer)
            {
                other.gameObject.SetActive(false);
            }
            GameStateManager.Instance.SetStateWithMessage(GameState.Dead, DeathMessage);
        }
    }
}
