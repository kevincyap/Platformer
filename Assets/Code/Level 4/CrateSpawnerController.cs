using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawnerController : MonoBehaviour
{
    public GameObject cratePrefab;
    public Transform crateSpawnPoint;
    public GameObject canvas;
    public float blinkTime = 0.5f; 
    public float spawnTime = 10f;

    bool spawningCrates = false;
    bool blinking = false;

    bool startedSpawning = false;
    IEnumerator SpawnCrates() {
        while(true) {
            if(spawningCrates) {
                CrateMover crate = Instantiate(cratePrefab, crateSpawnPoint.position, Quaternion.identity).GetComponent<CrateMover>();
                crate.controller = this;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
    IEnumerator BlinkCanvas() {
        while(blinking) {
            yield return new WaitForSeconds(blinkTime);
            canvas.SetActive(!canvas.activeSelf);
        }
        canvas.SetActive(false);
    }
    public void StartBlinking() {
        blinking = true;
        StartCoroutine(BlinkCanvas());
    }
    public void StopBlinking() {
        print("tee");
        blinking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawningCrates = true;
            if (!startedSpawning) {
                startedSpawning = true;
                StartCoroutine(SpawnCrates());
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawningCrates = false;
            blinking = false;
        }
    }
}
