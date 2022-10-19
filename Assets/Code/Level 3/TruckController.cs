using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 initPos;

    public float xSlow;

    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initPos = transform.position;
    }

    void FixedUpdate()
    {
        if (move) {
            // move at normal speed when x-pos of truck is > xSlow
            if (transform.position.x > xSlow) {
                Move();
            } else {
                Move(3f);
            }
        }

        // reset truck to initial position if it is below a certain y
        if (transform.position.y < -10) {
            Reset();
        }
    }

    void Move(float speed = 7.5f) { // moves truck left
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void Reset() {
        transform.position = initPos;
        move = false;
    }
}
