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

    public bool aButtonPressed
    {
        get;
        set;
    }

    public bool bButtonPressed
    {
        get;
        set;
    }

    public bool xButtonPressed
    {
        get;
        set;
    }

    public bool yButtonPressed
    {
        get;
        set;
    }

    public bool lbButtonPressed
    {
        get;
        set;
    }

    public bool rbButtonPressed
    {
        get;
        set;
    }

    public bool rbButtonHeld
    {
        get;
        set;
    }

    public bool quitButtonPressed
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
            aButtonPressed = Input.GetButtonDown("AButton");
            bButtonPressed = Input.GetButtonDown("BButton");
            xButtonPressed = Input.GetButtonDown("XButton");
            yButtonPressed = Input.GetButtonDown("YButton");
        }
        //Nintendo joycon connected
        else if(controllerConnect[1])
        {
            horizontalMovement = Input.GetAxisRaw("LeftJoyStickHorizontalJoycon");
            verticalMovement = Input.GetAxisRaw("LeftJoyStickVerticalJoycon");
            aButtonPressed = Input.GetButtonDown("AButton");
            bButtonPressed = Input.GetButtonDown("BButton");
            xButtonPressed = Input.GetButtonDown("XButton");
            yButtonPressed = Input.GetButtonDown("YButton");
            quitButtonPressed = Input.GetButtonDown("MinusButton");
            lbButtonPressed = Input.GetButtonDown("SLButton");
            rbButtonPressed = Input.GetButtonDown("SRButton");
            rbButtonHeld = Input.GetButton("SRButton");
        }
        //No controller connected so use keyboard
        else
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            aButtonPressed = Input.GetButtonDown("Jump");
            bButtonPressed = Input.GetButtonDown("Fire1");
            xButtonPressed = Input.GetButtonDown("Fire2");
            quitButtonPressed = Input.GetKeyDown(KeyCode.Escape);
        }

        if (quitButtonPressed)
        {
            SceneManager.LoadScene("TItle Screen");
        }
    }

}
