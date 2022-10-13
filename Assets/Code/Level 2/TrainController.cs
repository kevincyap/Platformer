using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainController : MonoBehaviour
{
    public int xLeft;
    public int xRight;
    public int speed;
    public int trainDelay;
    public bool onHold = false;
    bool moving;
    public float moveTime;

    public GameObject trainWarning;
    public int warningOffDistance = 20;
    public float flashDelay = 0.5f;
    public float warningOnTime = 3f;
    public GameObject player;
    bool flashing = false;
    // Start is called before the first frame update
    void Start()
    {
        moveTime = Time.time + trainDelay;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (moveTime - Time.time < warningOnTime && !moving && !onHold && !flashing && player.transform.position.y < 0) {
            StartCoroutine(FlashWarning());
        }
        if (!onHold && !moving && Time.time > moveTime) {
            moving = true;
        } 
        else if (moving) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (transform.position.x < xLeft) {
            flashing = false;
            transform.position = new Vector3(xRight, transform.position.y, transform.position.z);
            moveTime = Time.time + trainDelay;
            moving = false;
        }
    }

    public void StartMoving(int delay = 0) {
        moveTime = Time.time + delay;
    }

    public void ReleaseHold() {
        onHold = false;
    }

    public void StopTrain() {
        onHold = true;
    }

    IEnumerator FlashWarning() {
        flashing = true;
        while (Mathf.Abs(transform.position.x - player.transform.position.x) > warningOffDistance && flashing) {
            trainWarning.SetActive(true);
            yield return new WaitForSeconds(flashDelay);
            trainWarning.SetActive(false);
            yield return new WaitForSeconds(flashDelay);
        }
        flashing = false;
    }
}
