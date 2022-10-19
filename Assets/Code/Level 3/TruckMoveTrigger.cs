using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMoveTrigger : MonoBehaviour
{
    public TruckController truck;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Please work");
        if(other.gameObject.CompareTag("Player")) {
            truck.move = true;
        }
    }
}
