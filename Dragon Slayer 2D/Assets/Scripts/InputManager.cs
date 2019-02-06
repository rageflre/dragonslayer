using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public float horizontalMovement
    {
        get;
        set;
    }

    public float verticalMovement
    {
        get;
        set;
    }

    public bool jumpButtonDown
    {
        get;
        set;
    }

    public bool attackButtonDown
    {
        get;
        set;
    }

    private void Update()
    {
        bool[] controllerConnect = new bool[2];

        foreach(string name in Input.GetJoystickNames())
        {
            if(name.Contains("Xbox One"))
            {
                controllerConnect[0] = true;
            }
        }


        //Xbox one controller conntected
        if (controllerConnect[0])
        {
            horizontalMovement = Input.GetAxisRaw("LeftJoyStickHorizontal");
            verticalMovement = Input.GetAxisRaw("LeftJoyStickVertical");
            jumpButtonDown = Input.GetButtonDown("AButton");
            attackButtonDown = Input.GetButtonDown("BButton");
        } else
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            jumpButtonDown = Input.GetButtonDown("Jump");
            attackButtonDown = Input.GetButtonDown("Fire1");
        }
    }

}
