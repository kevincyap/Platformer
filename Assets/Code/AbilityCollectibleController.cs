using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectibleController : MonoBehaviour
{
    public AbilityCollectible collectible;

    AudioSource audioSource;
    public AudioClip audioClip;

    bool visible = true;

    CollectibleManager collectibleManager;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        collectibleManager = GameObject.FindGameObjectWithTag("CollectibleManager").GetComponent<CollectibleManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // check player is picking up visible collectible
        if (other.gameObject.CompareTag("Player") && visible) {
            // set invisible
            visible = false;
            GetComponent<Renderer>().enabled = false;

            // add to inventory
            if (InventoryManager.instance != null) {
                InventoryManager.instance.AddItem(collectible); // for inventory
            }
            /*
            if (CollectibleManager.instance != null) {
                CollectibleManager.instance.AddItem(collectible);
            }
            */
            if (collectibleManager != null) {
                collectibleManager.AddItem(collectible);
            }

            collectible.Use();

            // play sound and destroy
            if (audioSource != null && audioClip != null) {
                audioSource.PlayOneShot(audioClip);
                DestroyAfterWait(audioClip.length);
            }
        }
    }

    IEnumerator DestroyAfterWait(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
