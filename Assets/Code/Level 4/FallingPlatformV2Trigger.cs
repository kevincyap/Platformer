using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformV2Trigger : MonoBehaviour
{
    public GameObject FallingPlatform;
    FallingPlatformV2 FallingPlatformScript;

    void Start() {
        FallingPlatformScript = FallingPlatform.GetComponent<FallingPlatformV2>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Collided with player");
            FallingPlatformScript.Fall();
        }
    }
}
