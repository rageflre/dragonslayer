using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{


    public GameObject objectShatterer;

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.name.Equals("Player"))
        {
            //Checks if the player is attacking - Anthony
            bool isAttacking = collision.gameObject.GetComponent<PlayerControl>().isAttacking;

            if (isAttacking)
            {
                //If the player is attacking, the method SpawnBrokenObject and SpawnRandomPickup will be called from the GameManager script - Anthony
                GameManager.instance.SpawnBrokenObject(transform, objectShatterer);
                GameManager.instance.SpawnRandomPickup(gameObject.transform);
                //Gets rid of the game object that's colliding with "Player" - Anthony
                Destroy(gameObject);
            }
        }
    }
}