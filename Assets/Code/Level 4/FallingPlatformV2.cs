using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformV2 : MonoBehaviour
{
    public float delay = 1.5f;
    public float fallTime = 4f;

    bool falling = false;
    bool respawning = false;

    public GameObject parent;
    public GameObject cables;
    Vector2 parentInitPos;
    Vector2 cablesInitPos;
    Vector2 platformInitPos;

    Rigidbody2D platformRB;

    public GameObject trigger;
    Collider2D triggerCollider;
    Collider2D playerCollider;

    // Start is called before the first frame update
    void Start() {
        parentInitPos = parent.transform.position;
        cablesInitPos = cables.transform.position;
        platformInitPos = transform.position;

        platformRB = GetComponent<Rigidbody2D>();

        triggerCollider = trigger.GetComponent<Collider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void Update() {
        
    }

    public void Fall() {
        Physics2D.IgnoreCollision(triggerCollider, playerCollider);
        if (!falling && !respawning) {
            StartCoroutine(Fall(delay, fallTime));
        }
    }

    IEnumerator Fall(float delay, float fallTime) {
        yield return new WaitForSeconds(delay);

        platformRB.bodyType = RigidbodyType2D.Dynamic;
        falling = true;
        StartCoroutine(MoveCablesUp(0.05f));
        yield return new WaitForSeconds(fallTime);
        falling = false;
        Respawn();
    }

    IEnumerator MoveCablesUp(float speed) {
        while (falling) {
            Vector2 newPos = cables.transform.position;
            newPos.y += speed;
            cables.transform.position = newPos;

            yield return null;
        }
    }

    public void Respawn() {
        respawning = true;

        platformRB.bodyType = RigidbodyType2D.Static;
        cables.transform.position = cablesInitPos;
        transform.position = platformInitPos;
        parent.transform.position = parentInitPos;

        Vector2 respawnPos = parentInitPos;
        respawnPos.y += 20;
        parent.transform.position = respawnPos;
        StartCoroutine(MoveDownPlatform(0.05f));
    }

    IEnumerator MoveDownPlatform(float speed) {
        while (parent.transform.position.y > parentInitPos.y) {
            Vector2 newPos = parent.transform.position;
            newPos.y -= speed;
            parent.transform.position = newPos;
            yield return null;
        }

        respawning = false;
        Physics2D.IgnoreCollision(triggerCollider, playerCollider, false);
    }


    public void Reset() {
        StopAllCoroutines();
        platformRB.bodyType = RigidbodyType2D.Static;
        cables.transform.position = cablesInitPos;
        transform.position = platformInitPos;
        parent.transform.position = parentInitPos;
        falling = false;
        respawning = false;
        Physics2D.IgnoreCollision(triggerCollider, playerCollider, false);
    }
}
