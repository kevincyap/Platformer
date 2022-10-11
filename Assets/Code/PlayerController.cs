using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    //Movement
    public float maxSpeed;
    public float accelSpeed;
    public float deaccelSpeed;
    public float deaccelAir;
    public float jumpSpeed;
    public float jumpAllowance;

    float lastJump = 0f;
    float coyoteTime = 0.1f;
    float lastGrounded = 0f;

    //Layering
    public Transform feet;
    public Transform wallGrab;
    LayerMask groundLayer;
    LayerMask wallLayer;

    //Wall Jumping
    bool grabbingWall = false;
    float gravScale;
    float facing = 1f;
    float lastGrab = 0f;
    float grabDelay = 0.2f;
    float grabLaunchProp = 0.7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = transform.Find("Feet");
        wallGrab = transform.Find("WallGrab");
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");

        gravScale = rb.gravityScale;
    }

    void Update()
    {
        bool againstWall = Physics2D.OverlapCircle(wallGrab.position, 0.255f, wallLayer);
        bool grounded = Physics2D.OverlapCircle(feet.position, 0.05f, groundLayer);
        

        //Wall hanging
        facing = rb.velocity.x != 0 ? Mathf.Sign(rb.velocity.x) : facing;
        if (!grabbingWall && againstWall && Time.time - lastGrab > grabDelay) {
            grabbingWall = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 0f);
        }
        if (grabbingWall)
        {
            if (Input.GetButtonDown("Jump")) {
                grabbingWall = false;
                rb.velocity = new Vector2(-facing * maxSpeed * grabLaunchProp, jumpSpeed);
                lastGrab = Time.time;
            } else {
                rb.velocity = new Vector2(0f, 0f);
            }
        } else {
            rb.gravityScale = gravScale;
        }

        //Jumping
        if ((Input.GetButtonDown("Jump") || CheckJumpTolerance()) && !grabbingWall)
        {
            if (grounded || CheckCoyoteTime()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                lastJump = 0f;
                lastGrounded = 0f;
            } else if (Input.GetButtonDown("Jump")){
                lastJump = Time.time;
            }
        }

        if (grounded) {
            lastGrounded = Time.time;
        }
        //Player horizontal movement
        if (Time.time - lastGrab < grabDelay) {
            
        }
        else if (Input.GetAxisRaw("Horizontal") == 0) {
            if (grounded) {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, deaccelSpeed * Time.deltaTime), rb.velocity.y);
            } else {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, deaccelAir * Time.deltaTime), rb.velocity.y);
            }
        } else if (!grabbingWall){
            if (Input.GetAxisRaw("Horizontal") != Mathf.Sign(rb.velocity.x)) {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * accelSpeed, 0));
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    bool CheckJumpTolerance() {
        return (lastJump != 0f && Time.time - lastJump < jumpAllowance);
    }
    bool CheckCoyoteTime() {
        return (Time.time - lastGrounded < coyoteTime);
    }

}
