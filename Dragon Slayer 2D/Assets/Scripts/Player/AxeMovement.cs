using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : MonoBehaviour
{

    public int attackDamage;
    float speed = 4, rotateTimer;
    Rigidbody2D rb;
    Quaternion originalRotation;
    public GameObject spawnedObject;

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
            EnemyController controller = collision.GetComponent<EnemyController>();
            controller.DecreaseHealth(attackDamage, collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Candle"))
        {
            GameManager.instance.SpawnBrokenObject(gameObject.transform, collision.gameObject.tag);
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
