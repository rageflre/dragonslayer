using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Transform groundCheck;
    float speed = 2, jumpForce = 235, groundCheckRadius = 0.105f, dashSpeed = 100;
    public bool isAttacking = false;
    [SerializeField] bool debugGroundCheck = false;

    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    InputManager inputManager;

    float attackTimer, attackReset, throwTimer;
    bool isGrounded, isThrowing, isClimbing, isDashing, canAttack;
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

        HandleThrowing();

        HandleClimbing();

        HandleDash();

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
            Destroy(collision.gameObject);
        }

        if(collision.tag.Equals("Throwing sword"))
        {
            GameManager.instance.PickupThrowableSword();
            Destroy(collision.gameObject);
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
            FlipThrowPosition();
        }
    }

    void HandleJumping()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.36f, 0.105f), groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));
        
        if (inputManager.aButtonPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        animator.SetBool("jumping", isGrounded ? false : true);
    }

    void HandleAttacking()
    {

        if (inputManager.bButtonPressed && canAttack && !isClimbing && !isDashing)
        {
            isAttacking = true;
            canAttack = false;
            attackReset = Time.time + 0.325f;
            attackTimer = Time.time + 0.65f;
        }

        if(Time.time > attackReset)
        {
            isAttacking = false;
        }
        if(Time.time > attackTimer)
        {
            canAttack = true;
        }
        animator.SetBool("attacking", isAttacking);
    }

    void HandleThrowing()
    {
        bool hasThrowable = GameManager.instance.throwableObject != null;

        if (inputManager.xButtonPressed && hasThrowable && !isThrowing && !isClimbing)
        {
            Instantiate(GameManager.instance.throwableObject, transform.GetChild(1).transform.position, transform.GetChild(1).transform.rotation);
            GameManager.instance.throwableObject = null;
            isThrowing = true;
            throwTimer = Time.time + 0.3f;
            animator.SetBool("throwing", isThrowing);
        }

        if (Time.time > throwTimer)
        {
            isThrowing = false;
            animator.SetBool("throwing", isThrowing);
        }
    }

    void HandleClimbing()
    {
        isClimbing = animator.GetBool("climbing") || animator.GetBool("climbing_idle");
        RaycastHit2D hitLadder = Physics2D.Raycast(groundCheck.position, Vector2.up, groundCheckRadius, 1 << LayerMask.NameToLayer("Ladders")), ladderTop = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));

        //Checks if the player is colliding with the top of the ladder
        if(ladderTop.collider != null && ladderTop.collider.gameObject.tag.Equals("LadderTop"))
        {
            PlatformEffector2D platform = ladderTop.collider.GetComponent<PlatformEffector2D>();

            if(platform != null)
            {
                onPlatform = platform;
                if (inputManager.verticalMovement < 0)
                {
                    platform.rotationalOffset = 180;
                    rotationTime = Time.time + 0.5f;
                }
            }
        }
        else if(onPlatform != null && (Time.time > rotationTime || inputManager.verticalMovement > 0))
        {
            onPlatform.rotationalOffset = 0;
            onPlatform = null;
        }

        //Handles if the player is colliding with the regular ladders
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
            else if (!isGrounded && inputManager.horizontalMovement == 0 && rb.gravityScale == 0)
            {
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

    float dashTimer, maxDash = 0.5f, dashCooldownTimer = 2f;
    bool dashCooldown;

    void HandleDash()
    {
        if (inputManager.rbButtonHeld && !isDashing && !dashCooldown && !isClimbing)
        {
            if(isAttacking)
            {
                isAttacking = false;
                animator.SetBool("attacking", isAttacking);
            }
            isDashing = true;
            animator.SetBool("dashing", isDashing);
        }
        if(isDashing && !inputManager.rbButtonHeld && dashTimer >= 0.2f)
        {
            SetDashingCooldown();
        }
        if(isDashing)
        {
            rb.velocity = new Vector2((spriteRenderer.flipX ? -1 : 1) * 5, 0);
            dashTimer += Time.deltaTime;
            if(dashTimer >= maxDash)
            {
                SetDashingCooldown();
            }
        }
        if(dashCooldown)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                dashTimer = 0;
                dashCooldown = false;
            }
        }
    }

    void SetDashingCooldown()
    {
        dashTimer = dashCooldownTimer;
        isDashing = false;
        dashCooldown = true;
        animator.SetBool("dashing", isDashing);
    }

    void FlipThrowPosition()
    {
        transform.GetChild(1).localPosition = new Vector3(!spriteRenderer.flipX ? 0.193f : -0.193f, -0.074f, 0f);
        transform.GetChild(1).transform.Rotate(0f, 180f, 0f);
    }
}
