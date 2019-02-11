using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleDespawn : MonoBehaviour
{

    
    private void Update()
    {
        transform.Rotate(Vector3.forward * 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Foreground"))
        {
            Destroy(gameObject);
        }
    }
}
