using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{

    float speed = 4, rotateTimer;
    Rigidbody2D rb;
    Quaternion originalRotation;
    public GameObject spawnedObject, objectShatterer;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (Time.time > rotateTimer)
        {
            transform.Rotate(Vector3.forward * -90);
            rotateTimer = Time.time + 0.15f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.Equals("Foreground"))
        {
            DestroySword();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            DestroySword();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Candle"))
        {
            GameManager.instance.SpawnBrokenObject(gameObject.transform, objectShatterer);
            GameManager.instance.SpawnRandomPickup(collision.gameObject.transform);
            Destroy(collision.gameObject);
        }
    }

    void DestroySword()
    {
        Destroy(gameObject);
        Instantiate(spawnedObject, transform.position, originalRotation);
    }

}
