using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    float turnDelay;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = GetComponent<Direction>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((spriteRenderer.flipX ? -1 : 1) * speed, rb.velocity.y);

        if(turnDelay > Time.time)
        {
            return;
        }

        if (direction.hitDirection.Equals(Direction.HitDirection.LEFT) || direction.hitDirection.Equals(Direction.HitDirection.RIGHT))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            turnDelay = Time.time + 1;
        }
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
}
