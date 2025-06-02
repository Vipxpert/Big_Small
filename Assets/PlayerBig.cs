using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioManager sfx;
    bool winPlayed = false;
    private Rigidbody2D rb2d;
    public GroundCheckBig Ground;
    public Gravity gravity;
    bool isFlippedVertically = false;
    public WallRightCheckCollider WallRight;
    public WallLeftCheckCollider WallLeft;
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private LayerMask groundLayer;
    public GameObject bigCube;
    public GameObject smallCube;
    bool isLookingForward = false;
    public bool isSeeingDialogue = false;

    private Collider2D fallThrough;

    public List<GameObject> list;
    public Lives live;
    private Collider2D jumpThroughCollider;
    public BigHitbox bigHitbox;

    //Shrink and big
    public BoxCollider2D boxCollider;
    float changingSpeed = 100f;
    private Vector2 smallOffset = new Vector2(0.02f, -0.7f);
    private Vector2 smallSize = new Vector2(0.7f, 0.73f);
    private float smallEdgeRadius = 0.1f;

    private Vector2 bigOffset = new Vector2(0.02f, -0.3086951f);
    private Vector2 bigSize = new Vector2(1.15f, 1.28017f);
    private float bigEdgeRadius = 0.2f;

    public LayerMask surfaceLayer;
    private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fallThrough = GetComponent<Collider2D>();
        smallCube.SetActive(false);
        live.UpdateHearts();
        Time.timeScale = 1;
    }

    private void SetPlayerOnPlatform()
    {

    }

    void Update()
    {
        
        Bigger();
        
        if (live.isDead)
        {
            rb2d.isKinematic = true;
            rb2d.simulated = false;
            live.playerSmallRenderer.enabled = true;
            live.playerBigRenderer.enabled = true;
            live.disappearTimer += Time.deltaTime;
            if (live.disappearTimer >= 0.91f)
            {
                smallCube.GetComponent<SpriteRenderer>().enabled = false;
                bigCube.GetComponent<SpriteRenderer>().enabled = false;
                //smallCube.SetActive(false);
                //bigCube.SetActive(false);   
            }
            animator.SetBool("IsDeath", true);
            //animator.SetFloat("Speed", 0);
            horizontal = 0;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            
        }
        else if (bigHitbox.isWin)
        {
            rb2d.isKinematic = true;
            rb2d.simulated = false;
            horizontal = 0;
            animator.SetFloat("Speed", 0);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (!winPlayed)
            {
                live.playerBigRenderer.enabled = true;
                sfx.PlaySFX(sfx.win);
                winPlayed = true;
                sfx.StopTheme();
            }
        }
        else if (isSeeingDialogue)
        {

        }
        else
        {
            if (live.isInvincible)
            {
                // Handle flickering effect during invincibility
                live.FlickerRenderer();

                // Update invincibility timer
                live.invincibilityTimer -= Time.deltaTime;

                // Check if invincibility has ended
                if (live.invincibilityTimer <= 0)
                {
                    live.isInvincible = false;
                    live.playerBigRenderer.enabled = true;
                    live.playerSmallRenderer.enabled = true;
                }
            }
            //Camera
            if (Input.GetKeyDown(KeyCode.C))
            {
                isLookingForward = !isLookingForward;
            }
            if (!isLookingForward)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Smaller();
                    Vector2 bigCubePos = bigCube.transform.position;
                    //bigCubePos.y -= 0.091f;
                    smallCube.transform.position = bigCube.transform.position;
                    smallCube.SetActive(true);
                    bigCube.SetActive(false);
                }


                horizontal = Input.GetAxisRaw("Horizontal");
                
                Flip();

                if(horizontal == 0)
                {
                    animator.SetFloat("Speed", 0);
                }

                if (WallRight.isOnWallRight && isFacingRight)
                {
                    horizontal = Input.GetAxisRaw("Horizontal") == 1 ? 0 : Input.GetAxisRaw("Horizontal");
                }
                if (WallRight.isOnWallRight && !isFacingRight)
                {
                    horizontal = Input.GetAxisRaw("Horizontal") == -1 ? 0 : Input.GetAxisRaw("Horizontal");
                }
                if (WallLeft.isOnWallLeft && WallRight.isOnWallRight)
                {
                    horizontal = 0;
                }
                if (!WallRight.isOnWallRight && !WallLeft.isOnWallLeft)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                }

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));

                float groundAngle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;

                //Debug.Log(groundAngle);
                if (groundAngle > 44f && groundAngle < 46f && IsGrounded())
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -45);
                }
                else if (groundAngle > 134f && groundAngle < 136f && IsGrounded())
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 45);
                }
                else
                {
                    if (rb2d.velocity.sqrMagnitude < 0.1f)
                    {
                        animator.SetFloat("Speed", 0);
                    }
                    else
                    {
                        animator.SetFloat("Speed", Mathf.Abs(horizontal));
                    }
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }





                if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
                {
                    if (gravity.isGravityInverted)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
                    }
                    else if (!gravity.isGravityInverted)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    }
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
                {

                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    // Check if player is currently colliding with a JumpThrough platform
                    if (jumpThroughCollider != null)
                    {
                        // Disable the BoxCollider2D of the JumpThrough platform
                        jumpThroughCollider.enabled = false;
                    }
                }
                if(IsGrounded())
                {
                    if (jumpThroughCollider != null)
                    {
                        jumpThroughCollider.enabled = true;
                    }
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isLookingForward)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Ground.isOnGround;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        if(gravity.isGravityInverted && !isFlippedVertically)
        {
            isFlippedVertically = true;
            Vector2 localScale = transform.localScale;
            localScale.y *= -1f;
            transform.localScale = localScale;
        }
        else if (!gravity.isGravityInverted && isFlippedVertically) {
            isFlippedVertically = false;
            Vector2 localScale = transform.localScale;
            localScale.y *= -1f;
            transform.localScale = localScale;
        }
        if (gravity.isGravityInverted)
        {
            rb.gravityScale = Mathf.Abs(rb.gravityScale) * -1;
        }
        else if (!gravity.isGravityInverted)
        {
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpThrough") )
        {
            if(jumpThroughCollider!=null)
            {
                jumpThroughCollider.enabled = true;
            }
            jumpThroughCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }

    public void LoseLives()
    {
        if (!live.isInvincible)
        {
            live.lives--;
            live.isInvincible = true;
            live.invincibilityTimer = live.invincibilityDuration;
            live.flickerTimer = 0f;
            live.flickerSpeed = 0.2f;
            live.FlickerRenderer();
            if (live.lives < 0)
            {
                live.isDead = true;
            }
        }
        live.UpdateHearts();
    }

    void Bigger()
    {
        float step = changingSpeed * Time.deltaTime;
        boxCollider.offset = Vector2.Lerp(boxCollider.offset, bigOffset, step);
        boxCollider.size = Vector2.Lerp(boxCollider.size, bigSize, step);
        boxCollider.edgeRadius = Mathf.Lerp(boxCollider.edgeRadius, bigEdgeRadius, step);
    }

    void Smaller()
    {
        boxCollider.offset = smallOffset;
        boxCollider.size = smallSize;
        boxCollider.edgeRadius = smallEdgeRadius;
    }
}
