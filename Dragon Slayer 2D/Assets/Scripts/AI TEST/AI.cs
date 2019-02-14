using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed;
    float turnDelay;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject player;
    bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((spriteRenderer.flipX ? -1 : 1) * speed, rb.velocity.y);
        if (Vector2.Distance(player.transform.position, transform.position) < 5)
        {
            HandleRaycast();
        }
        if (inRange)
        {
            HandleShooting();
        }


    }

    private void HandleShooting()
    {
        
    }

    private void HandleRaycast()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (raycastHit.transform.gameObject.CompareTag("Player")) {
            inRange = true;
        }
        else inRange = false;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            bool isMeleeAttacking = collision.gameObject.GetComponent<PlayerControl>().isAttacking;

            if (isMeleeAttacking)
            {
                GameManager.instance.IncreaseScore(1);
                Destroy(gameObject);
            }
            else
            {
                if (Time.time > GameManager.instance.invincibleTime)
                {
                    GameManager.instance.DecreaseHealth();
                }
            }
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if the enemy recently turned around
        if (turnDelay > Time.time)
        {
            return;
        }
        if (collision.gameObject.name.Equals("Foreground"))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            turnDelay = Time.time + 1;
        }
    }
}
