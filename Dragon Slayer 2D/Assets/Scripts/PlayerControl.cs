using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Transform groundCheck;
    float speed = 2, jumpForce = 235, groundCheckRadius = 0.105f;
    public bool isAttacking = false;
    [SerializeField] bool debugGroundCheck = false;

    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    InputManager inputManager;

    float attackTimer;
    bool isGrounded;
    PlatformEffector2D onPlatform;
    float rotationTime;

    private void Awake()
    {
        groundCheck = transform.GetChild(0).GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {

        HandleMovement();

        HandleJumping();

        HandleAttacking();

        HandleClimbing();

    }

    private void OnDrawGizmos()
    {
        if (debugGroundCheck)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(groundCheck.position, new Vector3(0.36f, 0.105f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Health") && GameManager.instance.currentHealth < 3)
        {
            GameManager.instance.IncreaseHealth();
            collision.gameObject.SetActive(false);
        }
    }

    void HandleMovement()
    {

        rb.velocity = new Vector2(inputManager.horizontalMovement * speed, rb.velocity.y);

        if (isGrounded)
        {
            animator.SetFloat("speed", Mathf.Abs(inputManager.horizontalMovement));
        }

        if (inputManager.horizontalMovement > 0.01f && spriteRenderer.flipX || inputManager.horizontalMovement < -0.01f && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void HandleJumping()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.36f, 0.105f), groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));
        
        if (inputManager.jumpButtonDown && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        animator.SetBool("jumping", isGrounded ? false : true);
    }

    void HandleAttacking()
    {
        if (inputManager.attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            attackTimer = Time.time + 0.325f;
        }

        if(Time.time > attackTimer)
        {
            isAttacking = false;
        }
        animator.SetBool("attacking", isAttacking);
    }

    void HandleClimbing()
    {
        RaycastHit2D hitLadder = Physics2D.Raycast(groundCheck.position, Vector2.up, 5, 1 << LayerMask.NameToLayer("Ladders")), ladderTop = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));

        if(ladderTop.collider != null && ladderTop.collider.gameObject.tag.Equals("LadderTop"))
        {
            PlatformEffector2D platform = ladderTop.collider.GetComponent<PlatformEffector2D>();

            if(platform != null)
            {
                onPlatform = platform;
                if (inputManager.verticalMovement < 0)
                {
                    platform.rotationalOffset = 180;
                    rotationTime = Time.time + 0.36f;
                }
            }
        } else if(onPlatform != null && (Time.time > rotationTime || inputManager.verticalMovement > 0))
        {
            onPlatform.rotationalOffset = 0;
            onPlatform = null;
        }

        if (hitLadder.collider != null)
        {
            if (Mathf.Abs(inputManager.verticalMovement) >= 0.7071f && Mathf.Abs(inputManager.horizontalMovement) >= 0.7071f && isGrounded)
            {
                return;
            }

            if (inputManager.verticalMovement < 0 && isGrounded)
            {
                animator.SetBool("climbing", false);
                animator.SetBool("climbing_idle", false);
            }
            else if (inputManager.verticalMovement != 0)
            {
                animator.SetBool("climbing", true);
                animator.SetBool("climbing_idle", false);
                rb.velocity = inputManager.verticalMovement > 0 ? Vector2.up : Vector2.down;
                rb.gravityScale = 0;
            }
            else if (!isGrounded && inputManager.horizontalMovement == 0) {
                rb.velocity = Vector2.zero;
                animator.SetBool("climbing_idle", true);
            }
        }
        else
        {
            animator.SetBool("climbing", false);
            animator.SetBool("climbing_idle", false);
            rb.gravityScale = 1;
        }
    }
}
