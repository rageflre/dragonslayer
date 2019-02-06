using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed, jumpForce, groundCheckRadius;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Direction direction;
    Animator animator;
    InputManager inputManager;
    public Transform groundCheck;

    float attackTimer;
    public bool isAttacking = false;
    bool isGrounded;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        direction = GetComponent<Direction>();

        animator = GetComponent<Animator>();

        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {

        HandleMovement();

        HandleJumping();

        HandleAttacking();

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Grabs on what side you entered the collision
        direction.hitDirection = direction.getDirection(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Sets on what side you entered the collision
        direction.hitDirection = direction.getDirection(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Sets on what side you entered the collision
        direction.hitDirection = direction.getDirection(collision);
    }
    
    void HandleMovement()
    {
        bool hitLeftWall = direction.hitDirection.Equals(Direction.HitDirection.LEFT);
        bool hitRightWall = direction.hitDirection.Equals(Direction.HitDirection.RIGHT);

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));

        if (inputManager.jumpButtonDown && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        animator.SetBool("jumping", isGrounded ? false : true);
    }

    void HandleAttacking()
    {
        if (inputManager.attackButtonDown && !isAttacking) //Input.GetKeyDown(KeyCode.RightShift)
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

}
