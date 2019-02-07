﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Transform groundCheck;
    public float speed, jumpForce, groundCheckRadius;
    public bool isAttacking = false;
    [SerializeField] bool debugGroundCheck = false;

    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    Animator animator;
    InputManager inputManager;

    float attackTimer;
    bool isGrounded;

    private void Awake()
    {

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

    private void OnDrawGizmos()
    {
        if (debugGroundCheck)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(groundCheck.position, new Vector3(0.36f, 0.105f));
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
