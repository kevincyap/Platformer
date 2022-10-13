using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CamMove : MonoBehaviour
{
    Transform target;
    public CinemachineVirtualCamera cam;
    public int orthoSize = 4;
    void Start(){
        target = transform.Find("Target");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Follow = target;
            cam.m_Lens.OrthographicSize = orthoSize;
        }
    }
}
