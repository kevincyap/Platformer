using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 initPos;

    public float xSlow;

    public bool move;

    // Warning
    public GameObject truckWarning;
    public float flashDelay = 0.5f;
    bool flashing = false;

    GameObject player;
    public int warningOffDistance = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initPos = transform.position;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (move) {
            // move at normal speed when x-pos of truck is > xSlow
            if (transform.position.x > xSlow) {
                Move();
            } else {
                Move(0.1f);
            }
        }

        // warning starts flashing in TruckMoveTrigger.cs
        // stop flashing if truck is close to player
        if (flashing && transform.position.x - player.transform.position.x < warningOffDistance) {
            flashing = false;
        }

        // reset truck to initial position if it is below a certain y
        if (transform.position.y < -10) {
            Reset();
        }
    }

    void Move(float speed = 0.2f) { // moves truck left, set speed to 0.15f for easier time
        // rb.velocity = new Vector2(-speed, rb.velocity.y); // speed should be 7.5f for normal, 3f for slow

        Vector2 newPos = transform.position;
        newPos.x -= speed;
        transform.position = newPos;
    }

    void Reset() {
        transform.position = initPos;
        move = false;
        flashing = false;
    }


    public void StartFlashing() {
        StartCoroutine(FlashWarning());
    }

    IEnumerator FlashWarning() {
        flashing = true;
        while (flashing) {
            truckWarning.SetActive(true);
            yield return new WaitForSeconds(flashDelay);
            truckWarning.SetActive(false);
            yield return new WaitForSeconds(flashDelay);
        }
    }
}
