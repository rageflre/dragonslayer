using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{


    public GameObject test;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.name.Equals("Player"))
        {
            bool isAttacking = collision.gameObject.GetComponent<PlayerControl>().isAttacking;

            if (isAttacking)
            {
                var t = transform;
                for (int i = 0; i < 3; i++)
                {

                    t.TransformPoint(0, -100, 0);
                    var clone = Instantiate(test, t.position, Quaternion.identity);
                    var body2D = clone.GetComponent<Rigidbody2D>();
                    body2D.AddForce(Vector3.right * Random.Range(-100, 50)); 
                    body2D.AddForce(Vector3.up * Random.Range(50, 150));
                    //body2D.AddForce(Vector3.right * Random.Range(-100, 100));
                    //body2D.AddForce(Vector3.up * Random.Range(50, 200));
                }

                 GameManager.instance.SpawnRandomPickup(gameObject.transform);
                 Destroy(gameObject);
            }
        }
    }

}