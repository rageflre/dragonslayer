using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    float turnDelay;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2((spriteRenderer.flipX ? -1 : 1) * speed, rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Sword collider"))
        {
            PlayerControl control = collision.transform.parent.GetComponent<PlayerControl>();
            bool isAttacking = control.isAttacking;
            int attackDamage = control.attackDamage;
            if (isAttacking && Time.time > enemyController.invincibleTime)
            {
                GameManager.instance.IncreaseScore(1);
                enemyController.DecreaseHealth(attackDamage, gameObject);
                control.isAttacking = false;
            }
            return;
        }
        else if (collision.name.Equals("Player") && Time.time > GameManager.instance.invincibleTime)
        {
            GameManager.instance.DecreaseHealth();
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