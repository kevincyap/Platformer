using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed = 4f;
    public float accelSpeed = 400f;
    public float jumpSpeed = 20f;
    public float jumpAllowance = 0.1f;

    float lastJump = 0f;

    public Transform feet;
    LayerMask groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = transform.Find("Feet");
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        bool grounded = Physics2D.OverlapCircle(feet.position, 0.25f, groundLayer);
        if (Input.GetButtonDown("Jump") || (lastJump != 0f && Time.time - lastJump < jumpAllowance))
        {
            if (grounded) {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                lastJump = 0f;
            } else if (Input.GetButtonDown("Jump")){
                lastJump = Time.time;
            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0) {
            rb.AddForce(new Vector2(Mathf.Sign(Input.GetAxisRaw("Horizontal"))*accelSpeed, 0));
        } else if (grounded) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);

    }
}
