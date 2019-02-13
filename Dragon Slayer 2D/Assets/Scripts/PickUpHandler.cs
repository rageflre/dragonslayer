using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Foreground") || collision.name.Equals("Ladder top"))
        {
            rb.gravityScale = 0;

            rb.velocity = Vector2.zero;

        }
    }
}
