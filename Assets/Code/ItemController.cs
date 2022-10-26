using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (InventoryManager.instance != null) {
                InventoryManager.instance.AddItem(item);
            }
            Destroy(gameObject);
        }
    }
}
