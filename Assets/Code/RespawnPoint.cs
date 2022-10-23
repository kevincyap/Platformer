using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static RespawnPoint Instance { get; private set; }
    GameObject player;
    GameObject bowl;
    public bool DefaultSpawn = false;
    bool active = false;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        bowl = transform.Find("Bowl").gameObject;
        if (DefaultSpawn) {
            Instance = this;
            SetBowlActive();
        }
    }

    public Transform GetTransform() {
        return transform;
    }

    protected void SetBowlActive() {
        if (Instance == this) {
            bowl.SetActive(true);
        }
        else {
            bowl.SetActive(false);
        }
    }

    protected void SetRespawnPoint(RespawnPoint rp) {
        Instance = rp;
        rp.SetBowlActive();
        SetBowlActive();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instance.SetRespawnPoint(this);
        }
    }
}
