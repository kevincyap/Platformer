using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRestartTrigger : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPosition;
    public string DeathMessage;
    void Awake() {
        GameStateManager.Instance.OnGameStateReset += OnGameStateReset;
    }
    void OnDestroy() {
        GameStateManager.Instance.OnGameStateReset -= OnGameStateReset;
    }
    void OnGameStateReset() {
        StopAllCoroutines();
        transform.position = startPosition;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
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
        gameObject.SetActive(false);
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }
}
