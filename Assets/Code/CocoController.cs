using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoController : MonoBehaviour
{
    Animator anim;
    Transform player;
    public int standDistance = 50;
    public int runDistance = 20;
    public int runSpeed = 5;
    public float runDelay = 0.5f;
    public float runLength = 3f;
    bool running = false;
    public Transform cocoSafeRoom;
    public int cocoSafeRoomOffset;
    public bool wallRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        int distance = (int)Vector3.Distance(transform.position, player.position);
        if (distance < runDistance) {
            StartCoroutine(RunAway());
        } else if (distance < standDistance && !running) {
            anim.SetInteger("State", 1);
            FaceDir(wallRunning ? 1 : -1);
        } else if (!running){
            anim.SetInteger("State", 0);
            FaceDir(wallRunning ? 1 : -1);
        }

        if (running) {
            float runOffset = runSpeed * Time.deltaTime;
            if (wallRunning) {
                transform.position = new Vector3(transform.position.x, transform.position.y + runOffset, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x + runOffset, transform.position.y, transform.position.z);
            }
        }
    }

    void FaceDir (int dir) {
        transform.localScale = new Vector3(dir*1.5f, 1.5f, 1.5f);
    }

    IEnumerator RunAway() {
        anim.SetInteger("State", 2);
        yield return new WaitForSeconds(runDelay);
        FaceDir(1);
        running = true;
        yield return new WaitForSeconds(runLength);
        running = false;
        transform.position = cocoSafeRoom.position + new Vector3(cocoSafeRoomOffset, 0, 0);
    }
}
