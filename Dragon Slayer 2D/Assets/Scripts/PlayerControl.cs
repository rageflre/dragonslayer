using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed, jumpForce, groundCheckRadius;
    float horizontalMovement;
    States state;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Direction direction;
    Animator animator;
    public Transform groundCheck;

    private void Awake()
    {
        state = GetComponent<States>();

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        direction = GetComponent<Direction>();

        animator = GetComponent<Animator>();

        state.SetState(States.State.IDLE);
    }

    private void Update()
    {

        //print("state: " + state.GetState().ToString() + ", hitDirection: " + direction.hitDirection.ToString());

        HandleMovement();

        HandleJumping();

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

        horizontalMovement = Input.GetAxis("Horizontal");

        if ((hitLeftWall && horizontalMovement < -0.01f || hitRightWall && horizontalMovement > 0.01f) && state.GetState().Equals(States.State.JUMPING))
            return;

        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);

        if (!state.GetState().Equals(States.State.JUMPING))
        {
            //TODO: Set speed for animator
            animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
            state.SetState(States.State.WALKING);
        }

        if (horizontalMovement > 0.01f && spriteRenderer.flipX || horizontalMovement < -0.01f && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (state.GetState().Equals(States.State.WALKING) && horizontalMovement == 0)
        {
            state.SetState(States.State.IDLE);
        }
    }

    void HandleJumping()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 1 << LayerMask.NameToLayer("Solid"));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if(!isGrounded)
        {
            //TODO: Set jumping for animator

            state.SetState(States.State.JUMPING);
        } else
        {
            state.SetState(horizontalMovement != 0 ? States.State.WALKING : States.State.IDLE);
        }

    }

}
