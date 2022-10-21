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
                Move(0.1f);
            }
        }

        // reset truck to initial position if it is below a certain y
        if (transform.position.y < -10) {
            Reset();
        }
    }

    void Move(float speed = 0.25f) { // moves truck left
        // rb.velocity = new Vector2(-speed, rb.velocity.y); // speed should be 7.5f for normal, 3f for slow

        Vector2 newPos = transform.position;
        newPos.x -= speed;
        transform.position = newPos;
    }

    void Reset() {
        transform.position = initPos;
        move = false;
    }
}
