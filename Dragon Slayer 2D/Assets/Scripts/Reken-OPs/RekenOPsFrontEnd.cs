using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RekenOPsFrontEnd : MonoBehaviour
{
    public GameObject[] buttons;
    public Text[] buttonText;
    public Transform[] pointerTarget;
    public GameObject pointer;
    public GameObject pointerSprite;

    private int selectedButton;

    private void Start()
    {
        selectedButton = 0;
        pointer.transform.position = pointerTarget[0].position;
        
    }
    private void Update()
    {
        //Button input en logica (input werkt alleen als een pointer animatie niet bezig is)
        if (Mathf.Round(pointer.transform.position.y * 100) / 100.0 == Mathf.Round(pointerTarget[selectedButton].position.y * 100) / 100.0)
        {
            if (Input.GetKeyDown("return"))
            {
                
            }
            if (Input.GetKeyDown("up"))
            {
                if (selectedButton == 0) { selectedButton = 3; }
                else if (selectedButton == 1) { selectedButton = 0; }
                else if (selectedButton == 2) { selectedButton = 1; }
                else if (selectedButton == 3) { selectedButton = 2; }
            }
            else if (Input.GetKeyDown("down"))
            {
                if (selectedButton == 0) { selectedButton = 1; }
                else if (selectedButton == 1) { selectedButton = 2; }
                else if (selectedButton == 2) { selectedButton = 3; }
                else if (selectedButton == 3) { selectedButton = 0; }
            }
            
        }
        //Pointer verplaatst zich naar de pointer target position van de geselecteerd button
        {
            pointer.transform.position = Vector2.MoveTowards(new Vector2(pointer.transform.position.x, pointer.transform.position.y), pointerTarget[selectedButton].position, 10f * Time.deltaTime);
        }
        //Oscilerende horizontale shift effect voor de pointer sprite
        {
            pointerSprite.transform.localPosition = new Vector2(.15f * Mathf.Sin(Time.time * 3f), 0);
        }
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != selectedButton)
                {
                    //reset scale en kleur voor de buttons
                    buttons[i].transform.localScale = new Vector2(1f, 1f);
                    buttonText[i].color = Color.black;
                }
                else
                {
                    //Oscillerende scale effect en rood kleur voor de buttons
                    buttons[i].transform.localScale = new Vector2(1.1f, 1.1f) + new Vector2(0.1f, 0.1f) * Mathf.Sin(Time.time * 3f);
                    buttonText[i].color = Color.red;
                }
            }
        }
    }
}
