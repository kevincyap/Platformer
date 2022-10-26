using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public CocoControllerCutscene coco;
    public GameObject canvas;
    public GameObject bowl;
    public GameObject bowl2;
    public GameObject winScreen;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameStateManager.Instance.SetState(GameState.Cutscene);
            StartCoroutine(PlayCutscene());
        }
    }
    public void FeedCoco() {
        StartCoroutine(PlayPart2());
    }
    IEnumerator PlayPart2() {
        canvas.SetActive(false);
        bowl.SetActive(false);
        bowl2.SetActive(true);
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
    }

    IEnumerator PlayCutscene() {
        yield return new WaitForSeconds(1f);
        coco.TriggerAction("Stand");
        yield return new WaitForSeconds(1f);
        bowl.SetActive(true);
        canvas.SetActive(true);
    }
}
