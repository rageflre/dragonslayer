using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    float turnDelay;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((spriteRenderer.flipX ? -1 : 1) * speed, rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            bool isAttacking = collision.gameObject.GetComponent<PlayerControl>().isAttacking;

            if (isAttacking)
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
