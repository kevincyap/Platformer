using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRestartTrigger : MonoBehaviour
{
    Rigidbody2D rb;
    public string DeathMessage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.gravityScale = 1;
            StartCoroutine(DelayDestroy());
            GameStateManager.Instance.SetStateWithMessage(GameState.Dead, DeathMessage);
        }
    }

    IEnumerator DelayDestroy() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
