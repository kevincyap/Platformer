using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    public string nextLevel;
    public float vertSpeed = 50f;
    public float delay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0f, vertSpeed);
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextLevel);
    }
}
