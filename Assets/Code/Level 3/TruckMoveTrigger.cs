using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMoveTrigger : MonoBehaviour
{
    public TruckController truck;
    bool inTrigger = false;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Please work");
        if(other.gameObject.CompareTag("Player")) {
            // truck.move = true;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inTrigger = false;
        }
    }

    void Update() {
        if (inTrigger) {
            truck.move = true;
            Debug.Log("in truck trigger");
        }
    }
}
