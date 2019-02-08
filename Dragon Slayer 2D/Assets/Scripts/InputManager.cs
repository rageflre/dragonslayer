using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool throwButtonDown
    {
        get;
        set;
    }

    public bool quitButtonDown
    {
        get;
        set;
    }

    private void Update()
    {
        bool[] controllerConnect = new bool[2];

        //Loops though all connected joysticks
        foreach(string name in Input.GetJoystickNames())
        {
            //print("" + name);
            if(name.Contains("Xbox One"))
            {
                controllerConnect[0] = true;
            }
            //Wireless gamepad = nintendo pro controller/joycons
            else if(name.Equals("Wireless Gamepad"))
            {
                controllerConnect[1] = true;
            }
        }


        //Xbox one controller conntected
        if (controllerConnect[0])
        {
            horizontalMovement = Input.GetAxisRaw("LeftJoyStickHorizontal");
            verticalMovement = Input.GetAxisRaw("LeftJoyStickVertical");
            jumpButtonDown = Input.GetButtonDown("AButton");
            attackButtonDown = Input.GetButtonDown("BButton");
            throwButtonDown = Input.GetButtonDown("XButton");
        }
        //Nintendo joycon connected
        else if(controllerConnect[1])
        {
            horizontalMovement = Input.GetAxisRaw("LeftJoyStickHorizontalJoycon");
            verticalMovement = Input.GetAxisRaw("LeftJoyStickVerticalJoycon");
            jumpButtonDown = Input.GetButtonDown("AButton");
            attackButtonDown = Input.GetButtonDown("BButton");
            throwButtonDown = Input.GetButtonDown("XButton");
        }
        //No controller connected so use keyboard
        else
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            jumpButtonDown = Input.GetButtonDown("Jump");
            attackButtonDown = Input.GetButtonDown("Fire1");
            throwButtonDown = Input.GetButtonDown("Fire2");
            quitButtonDown = Input.GetKeyDown(KeyCode.Escape);
        }

        if (quitButtonDown)
        {
            SceneManager.LoadScene("TItle Screen");
        }
    }

}
