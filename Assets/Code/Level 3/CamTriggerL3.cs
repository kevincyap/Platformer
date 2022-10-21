using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CamTriggerL3 : MonoBehaviour
{

    //public CinemachineVirtualCamera cam;
    GameObject cam;

    public bool deactivateOnExit = true;

    void Start() {
        cam = transform.Find("Cam").gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("enter triggered");
            cam.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && deactivateOnExit) {
            Debug.Log("exit triggered");
            cam.SetActive(false);
        }
    }
    
}
