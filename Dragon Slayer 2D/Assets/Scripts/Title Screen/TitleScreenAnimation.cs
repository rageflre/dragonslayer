using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScreenAnimation : MonoBehaviour
{
    public GameObject[] buttons;
    public Text[] buttonText;
    public Transform[] pointerTarget;

    private GameObject pointer;
    private int selectedButton;
    private float pointerScale;

    private void Start()
    {
        pointer = GameObject.Find("Pointer");
        selectedButton = 0;
        pointer.transform.position = pointerTarget[0].position;
    }
    private void Update()
    {
        if (Mathf.Round(pointer.transform.position.x * 100) / 100.0 == Mathf.Round(pointerTarget[selectedButton].position.x * 100) / 100.0)
        {
            if (Input.GetKeyDown("enter"))
            {
                if (selectedButton == 0) { StartGame(); }
            }
            if (Input.GetKeyDown("up"))
            {
                if (selectedButton == 0) { selectedButton = 2; }
                else if (selectedButton == 1) { selectedButton = 0; }
                else if (selectedButton == 2) { selectedButton = 1; }
            }
            else if (Input.GetKeyDown("down"))
            {
                if (selectedButton == 0) { selectedButton = 1; }
                else if (selectedButton == 1) { selectedButton = 2; }
                else if (selectedButton == 2) { selectedButton = 0; }
            }
            
        }
        {
            pointer.transform.position = Vector2.MoveTowards(new Vector2(pointer.transform.position.x, pointer.transform.position.y), pointerTarget[selectedButton].position, 10f * Time.deltaTime);
        }
        pointer.transform.localScale = new Vector2 (120f,120f) + new Vector2(15f,15f) * Mathf.Sin(Time.time * 3);
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void EndGame()
    {
        Application.Quit();
    }
}
