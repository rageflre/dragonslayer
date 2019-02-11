using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{
    public GameObject throwableAxe;
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
                Instantiate(throwableAxe,gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);

            }
        }
    }
}