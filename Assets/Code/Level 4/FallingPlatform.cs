using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    GameObject player;
    public float delay = 1f;
    public float fallSpeed = 0.01f;
    bool falling = false;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (falling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed, transform.position.z);
            parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + (fallSpeed/2), parent.transform.position.z);
        }
    }
        // Update is called once per frame
    //void OnTriggerEnter2D(Collider2D other){
    //   if (other.CompareTag("Player"))
    //    {
    //        falling = true;
    //        GameObject par = transform.parent.gameObject;
    //        Destroy(parent, 5);
    //    }
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        falling = true;
        GameObject par = transform.parent.gameObject;
        Destroy(parent, 5);
    }
}
