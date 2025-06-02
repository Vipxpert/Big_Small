using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSmall : MonoBehaviour
{
    public AudioManager sfx;
    bool winPlayed = false;
    private Rigidbody2D rb2d;
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    public GroundCheckCollider Ground;
    public Gravity gravity;
    bool isFlippedVertically = false;
    public WallRightCheckCollider WallRight;
    public WallLeftCheckCollider WallLeft;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] public Lives live;
    bool isLookingForward = false;
    public bool isSeeingDialogue = false;
    public GameObject bigCube;
    public GameObject smallCube;

    float prevSpeed;


    float hardDropVelocity = 22f;
    float fatalDropVelocity = 40f;
    bool isDelayed = false;

    public BigCheckBelowLeft bcbl;
    public BigCheckBelowRight bcbr;
    public BigCheckUpperLeft bcul;
    public BigCheckUpperRight bcur;
    public BigCheckUpperMiddle bcum;
    public BigCheckUpperFarLeft bcufl;
    public BigCheckUpperFarRight bcufr;
    public BigCheckUpperMiddleLeft bcuml;
    public BigCheckUpperMiddleRight bcumr;

    public CheckSpike checkSpike;

    private Collider2D jumpThroughCollider;
    private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        prevSpeed = animator.speed;
        live.UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (!live.isInvincible && checkSpike.isSpike)
            {
                LoseLives();
            }

        if (live.isDead)
        {
            rb2d.isKinematic = true;
            rb2d.simulated = false;
            live.playerSmallRenderer.enabled = true;
            live.playerBigRenderer.enabled = true;
            live.lives = -1;
            live.disappearTimer += Time.deltaTime;
            if (live.disappearTimer >= 0.99f)
            {
                smallCube.GetComponent<SpriteRenderer>().enabled = false;
                bigCube.GetComponent<SpriteRenderer>().enabled = false;
            }
            //animator.SetFloat("Horizontal", 0);
            animator.SetBool("IsDeath", true);
            horizontal = 0;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (checkSpike.isWin)
        {
            rb2d.isKinematic = true;
            rb2d.simulated = false;
            horizontal = 0;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (!winPlayed)
            {
                live.playerSmallRenderer.enabled = true;
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
                    live.playerSmallRenderer.enabled = true;
                    live.playerBigRenderer.enabled = true;
                }
            }

           
            

            if (Input.GetKeyDown(KeyCode.C))
            {
                isLookingForward = !isLookingForward;
            }
            if (!isLookingForward)
            {
                //Debug.Log(countCheck);
                if (Input.GetKeyDown(KeyCode.X) && (!((bcul.isTooBig || bcur.isTooBig || bcum.isTooBig || bcuml.isTooBig || bcumr.isTooBig) && Ground.isOnGround) && !(bcbl.isTooBig && bcbr.isTooBig) && !(bcbl.isTooBig && bcufr.isTooBig && Ground.isOnGround) && !(bcbr.isTooBig && bcufl.isTooBig && Ground.isOnGround) && !(bcbr.isTooBig && bcbl.isTooBig) || (bcufl.isTooBig&&bcufr.isTooBig)&&!Ground.isOnGround))
                {
                   
                    Vector2 smallCubePos = smallCube.transform.position;
                    if (isFlippedVertically)
                    {
                        smallCubePos.y -= 1f;
                    }
                    else
                    {
                        smallCubePos.y += 1f;
                    }
                    bigCube.transform.position = smallCubePos;
                    bigCube.SetActive(true);
                    smallCube.SetActive(false);
                }

                

                //Debug.Log(WallLeft.isOnWallLeft + " " + WallLeft.isOnWallLeft + " " + Input.GetAxisRaw("Horizontal"));
                if (WallRight.isOnWallRight && !WallLeft.isOnWallLeft)
                {
                    horizontal = Input.GetAxisRaw("Horizontal") == 1 ? 0 : Input.GetAxisRaw("Horizontal");
                }
                if (WallLeft.isOnWallLeft && !WallRight.isOnWallRight)
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
                if (Mathf.Abs(horizontal) > 0)
                {
                    animator.speed = prevSpeed;
                }
                else
                {
                    animator.speed = prevSpeed/2;
                }
                animator.SetFloat("Horizontal", horizontal);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));

                float groundAngle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
                if (groundAngle > 44f && groundAngle < 46f)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -45);
                }
                else if (groundAngle > 134f && groundAngle < 136f)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 45);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }


                if (gravity.isGravityInverted && !isFlippedVertically)
                {
                    isFlippedVertically = true;
                    Vector2 localScale = transform.localScale;
                    localScale.y *= -1f;
                    transform.localScale = localScale;
                }
                else if (!gravity.isGravityInverted && isFlippedVertically)
                {
                    isFlippedVertically = false;
                    Vector2 localScale = transform.localScale;
                    localScale.y *= -1f;
                    transform.localScale = localScale;
                }

                if (gravity.isGravityInverted)
                {
                    rb.gravityScale = Mathf.Abs(rb.gravityScale)*-1;
                }
                else if (!gravity.isGravityInverted)
                {
                    rb.gravityScale = Mathf.Abs(rb.gravityScale);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) && Ground.isOnGround)
                {
                    //Debug.Log("Pressed");
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
                if (Ground.isOnGround)
                {
                    if (jumpThroughCollider != null)
                    {
                        // Disable the BoxCollider2D of the JumpThrough platform
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
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        /*if (Ground.isOnGround)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < hardDropVelocity)
            {
                LoseLives();
            }
            if (GetComponent<Rigidbody2D>().velocity.y < fatalDropVelocity)
            {
                live.isDead = true;
            }
        }*/
    }

    /*private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }*/

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (!live.isInvincible && collision.CompareTag("Spike"))
        {
            LoseLives();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!live.isInvincible && collision.CompareTag("Spike"))
        {
            LoseLives();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpThrough"))
        {
            jumpThroughCollider = collision;
        }

    }
    //Cube dies if hit too hard
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            float fallSpeed = Mathf.Abs(collision.relativeVelocity.y);
            Debug.Log(fallSpeed + ">" + hardDropVelocity);
            if (fallSpeed > hardDropVelocity)
            {
                live.lives = -1;
                LoseLives();
            }
        }
    }
    void LoseLives()
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
}
