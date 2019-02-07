using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{

    float speed = 4;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);

            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
