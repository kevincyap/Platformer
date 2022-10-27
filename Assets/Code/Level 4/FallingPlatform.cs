using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    GameObject player;
    public float delay = 10f;
    private float idelay;
    public float fallSpeed = 0.01f;
    public float timer = 5f;
    bool falling = false;
    bool respawning = false;
    GameObject parent;
    private Vector2 parentInfo;
    private Vector2 platInfo;
    public GameObject replacement;
    // Start is called before the first frame update
    void Start()
    {
        print("should fall");
        idelay = delay;
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        parentInfo = new Vector2(parent.transform.position.x, parent.transform.position.y);
        platInfo = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (falling)
        {
            delay -= 0.001f;
        }
        if (falling && delay < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed, transform.position.z);
            parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + (fallSpeed / 2), parent.transform.position.z);
            timer -= 0.1f;
            if (timer <= 0)
            {
                falling = false;
                timer = 5f;
                delay = idelay;
                parent.transform.position = new Vector3(parentInfo.x, parentInfo.y + 20, parent.transform.position.z);
                transform.position = new Vector3(platInfo.x, platInfo.y + 20, transform.position.z);
                respawning = true;
            }
        }
        if (respawning)
        {
            timer -= 0.1f;
            parent.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y - 0.01f, parent.transform.position.z);
            if (parent.transform.position.y <= parentInfo.y) {
                respawning = false;
                timer = 5f;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            falling = true;
            print("should fall");
        }
    }
}
