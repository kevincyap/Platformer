using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutsceneController : MonoBehaviour
{
    public CocoControllerCutscene coco;
    public GameObject canvas;
    public GameObject bowl;
    public GameObject bowl2;
    public GameObject winScreen;
    public GameObject badEnding;
    public GameObject feedChoice, goodMenu, badMenu;


    void Start() {
        GameStateManager.Instance.SetState(GameState.Cutscene);
        StartCoroutine(delayStart());
    }
    IEnumerator delayStart() {
        yield return new WaitForSeconds(1.5f);
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }

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
    public void DontFeedCoco() {
        canvas.SetActive(false);
        badEnding.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(badMenu);
    }
    IEnumerator PlayPart2() {
        canvas.SetActive(false);
        bowl.SetActive(false);
        bowl2.SetActive(true);
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(goodMenu);
    }

    IEnumerator PlayCutscene() {
        yield return new WaitForSeconds(1f);
        coco.TriggerAction("Stand");
        yield return new WaitForSeconds(1f);
        bowl.SetActive(true);
        canvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(feedChoice);
    }
}
