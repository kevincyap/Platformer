using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CamMoveL3 : MonoBehaviour
{
    Transform target;

    public string camAnim = "StaticCam";
    public CinemachineVirtualCamera cam;
    public int orthoSize = 4;

    public bool enableExitAnim = true;
    public string exitAnim = "Default";

    void Start() {
        target = transform.Find("Target");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && camAnim != "") {
            cam.Follow = target;
            cam.m_Lens.OrthographicSize = orthoSize;

            CamController.instance.Play(camAnim);
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && enableExitAnim && exitAnim != "") {
            CamController.instance.Play(exitAnim);
        }
    }
    
}
