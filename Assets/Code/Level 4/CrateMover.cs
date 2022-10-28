using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMover : MonoBehaviour
{
    public float speed = -1f;
    public float upStartX = 142f; 
    public float YDestroy = 63f;
    public CrateSpawnerController controller;
    public float blinkStopDist = 10f;
    public float blinkStartDist = 100f;

    bool sentStartBlinking = false;
    bool sentStopBlinking = false;
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Mathf.Abs(player.transform.position.x - transform.position.x) < blinkStartDist && controller != null && !sentStartBlinking) {
            controller.StartBlinking();
            sentStartBlinking = true;
        }
        else if (player != null && Mathf.Abs(player.transform.position.x - transform.position.x) < blinkStopDist && controller != null && !sentStopBlinking) {
            controller.StopBlinking();
            sentStopBlinking = true;
        }
        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        if (upStartX > transform.position.x) {
            transform.position = transform.position + new Vector3(0, Mathf.Log10((upStartX - transform.position.x)+1)/20f, 0);
        }
        if (transform.position.y > YDestroy) {
            Destroy(gameObject);
        }
    }
}
