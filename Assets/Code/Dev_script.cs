using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayStart());
    }
    IEnumerator delayStart() {
        yield return new WaitForSeconds(1);
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
}
