using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Features")]
    public bool EnableDashing;
    public bool EnableWallClimb;
    public bool EnableDoubleJump;

    //Movement
    //Jumping
    [Header("Basic Movement")]
    public float maxSpeed;
    float maxSpeedSave;
    public float accelSpeed;
    public float deaccelSpeed;
    public float deaccelAir;
    public float jumpSpeed;
    public float jumpAllowance;
    public float jumps;
    private float currJumps;

    //Dashing
    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCD = 1f;
    [SerializeField] private TrailRenderer tr;
    public TrailRenderer walkTr;

    float lastJump = 0f;
    float coyoteTime = 0.1f;
    float lastGrounded = 0f;

    [Header("Wall Grabbing")]
    //Layering
    public Transform feet;
    public Transform wallGrab;
    LayerMask groundLayer;
    LayerMask wallLayer;

    //Wall Jumping
    [Header("Wall Jumping")]
    bool grabbingWall = false;
    float gravScale;
    float facing = 1f;
    float lastGrab = 0f;
    float grabDelay = 0.2f;
    float grabLaunchProp = 0.7f;

    bool enable = false;

    public bool disableJump = false;

    public float velocityX; 

    
    Animator anim;
    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip dashSound;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        GameStateManager.Instance.OnGameStateReset += OnGameStateReset;
    }
 
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        GameStateManager.Instance.OnGameStateReset -= OnGameStateReset;
    }

    void OnGameStateChanged(GameState newGameState)
    {
        print("hi");
        enable = newGameState == GameState.Gameplay;
    }

    void OnGameStateReset() {
        gameObject.SetActive(false);
        tr.emitting = false;
        walkTr.emitting = false;
        transform.position = RespawnPoint.Instance.GetTransform().position;
        rb.velocity = Vector3.zero;
        rb.gravityScale = gravScale;
        grabbingWall = false;
        isDashing = false;
        canDash = true;
        currJumps = jumps;
        maxSpeed = maxSpeedSave;
        gameObject.SetActive(true);
        StartCoroutine(EnableTrail());
    }

    IEnumerator EnableTrail() {
        yield return new WaitForSeconds(0.1f);
        walkTr.emitting = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        feet = transform.Find("Feet");
        wallGrab = transform.Find("WallGrab");
        walkTr = transform.Find("WalkTrail").GetComponent<TrailRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");
        maxSpeedSave = maxSpeed;
        gravScale = rb.gravityScale;

        // CollectibleManager.instance.SetInventoryBasedOnPlayer(this);
        GameObject collectibleManagerObj = GameObject.FindGameObjectWithTag("CollectibleManager");
        if (collectibleManagerObj != null) {
            CollectibleManager collectibleManager = collectibleManagerObj.GetComponent<CollectibleManager>();
            collectibleManager.SetInventoryBasedOnPlayer(this);
        }
    }

    void Update()
    {
        bool justJumped = false;
        enable = GameStateManager.Instance.CurrentGameState == GameState.Gameplay;
        velocityX = rb.velocity.x;
        if (Input.GetButtonDown("Pause") && GameStateManager.Instance.CurrentGameState != GameState.Cutscene)
        {
            GameState state = GameStateManager.Instance.CurrentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay;
            GameStateManager.Instance.SetState(state);
        }

        bool againstWall = Physics2D.OverlapCircle(wallGrab.position, 0.27f, wallLayer);
        bool grounded = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayer) || Physics2D.OverlapCircle(feet.position, 0.1f, wallLayer);
        if (enable) {
            if (isDashing)
            {
                return;
            }
            

            //Wall hanging
            facing = Mathf.Abs(rb.velocity.x) >= 0.5f ? Mathf.Sign(rb.velocity.x) : facing;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*facing, transform.localScale.y, transform.localScale.z);
            if (EnableWallClimb) {
                if (!grabbingWall && againstWall && Time.time - lastGrab > grabDelay) {
                    grabbingWall = true;
                    rb.gravityScale = 0f;
                    rb.velocity = new Vector2(0f, 0f);
                }
                if (grabbingWall && againstWall)
                {
                    if (Input.GetButtonDown("Jump")) {
                        grabbingWall = false;
                        rb.velocity = new Vector2(-facing * maxSpeed * grabLaunchProp, jumpSpeed);
                        audioSource.PlayOneShot(jumpSound);
                        justJumped = true;
                        currJumps = EnableDoubleJump ? jumps - 1 : 0;
                        lastGrab = Time.time;
                    } else {
                        rb.velocity = new Vector2(0f, 0f);
                    } 
                }
                else if (grabbingWall)
                {
                    grabbingWall = false;
                    lastGrab = Time.time;
                }
                else {
                    rb.gravityScale = gravScale;
                } 
            }
            
            //Jumping
            if (!justJumped && !disableJump && (Input.GetButtonDown("Jump") || CheckJumpTolerance()) && !grabbingWall)
            {
                if (grounded || CheckCoyoteTime()) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    anim.SetTrigger("Jump");
                    audioSource.PlayOneShot(jumpSound);
                    lastJump = 0f;
                    lastGrounded = 0f;
                    currJumps = EnableDoubleJump ? jumps - 1 : 0;
                } else if (currJumps > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    anim.SetTrigger("Jump");
                    audioSource.PlayOneShot(jumpSound);
                    lastJump = 0f;
                    currJumps = currJumps - 1;
                } else if (Input.GetButtonDown("Jump")) {
                    lastJump = Time.time;
                }
            }

            //dashing
            if (EnableDashing && Input.GetButtonDown("Dash") && canDash)
            {
                anim.SetTrigger("Dash");
                audioSource.PlayOneShot(dashSound);
                StartCoroutine(Dash());
            }
        }
        if (grounded) {
            lastGrounded = Time.time;
        }
        //Player horizontal movement
        if (Time.time - lastGrab < grabDelay) {
            
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 || !enable) {
            if (grounded) {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, deaccelSpeed * Time.deltaTime), rb.velocity.y);
            } else {
                rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, deaccelAir * Time.deltaTime), rb.velocity.y);
            }
        } else if (!grabbingWall && enable){
            if (Input.GetAxisRaw("Horizontal") == -Mathf.Sign(rb.velocity.x)) {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * accelSpeed*Time.deltaTime, 0));
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);

        if (anim != null && !isDashing) {
            anim.SetBool("Moving", Mathf.Abs(rb.velocity.x) >= 1f && Input.GetAxisRaw("Horizontal") != 0 && enable);
            anim.SetBool("TouchingGround", grounded);
            anim.SetBool("WallHang", grabbingWall);
        }

    }

    private IEnumerator Dash()
    {
        walkTr.emitting = false;
        canDash = false;
        isDashing = true;
        float oGravity = rb.gravityScale;
        float oSpeed = maxSpeed;
        maxSpeed = 24f;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = oGravity;
        maxSpeed = oSpeed;
        walkTr.emitting = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }

    bool CheckJumpTolerance() {
        return (lastJump != 0f && Time.time - lastJump < jumpAllowance);
    }
    bool CheckCoyoteTime() {
        return (Time.time - lastGrounded < coyoteTime);
    }

}
