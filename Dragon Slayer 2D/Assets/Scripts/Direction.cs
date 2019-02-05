using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public enum HitDirection { NONE, LEFT, RIGHT, BOTTOM, TOP };

    public HitDirection hitDirection;

    public HitDirection getDirection(Collision2D collision)
    {
        //Initiate the direction you hit the collision
        HitDirection dir = HitDirection.NONE;

        //Loops though all contact points
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            //Checks if the y is higher then 0
            if (hitPos.normal.y > 0)
                dir = HitDirection.BOTTOM;
            //Checks if the y is lower then 0
            else if (hitPos.normal.y < 0)
                dir = HitDirection.TOP;
            //Checks if the x is higher then 0
            else if (hitPos.normal.x > 0)
                dir = HitDirection.LEFT;
            //Checks if the x is lower then 0
            else
                dir = HitDirection.RIGHT;
        }
        return dir;
    }
}
