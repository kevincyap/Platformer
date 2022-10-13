using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectibleController : MonoBehaviour
{
    public AbilityCollectible collectible;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            collectible.Use();
            Destroy(gameObject);
        }
    }
}
