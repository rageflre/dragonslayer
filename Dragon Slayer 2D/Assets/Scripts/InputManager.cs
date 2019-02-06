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
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        jumpButtonDown = Input.GetButtonDown("Jump");
        attackButtonDown = Input.GetButtonDown("Fire1");
    }

}
