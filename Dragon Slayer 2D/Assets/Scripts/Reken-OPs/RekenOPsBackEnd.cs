using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RekenOPsBackEnd : MonoBehaviour
{
    private int num1, num2;
    private int calcoperator; //0 = -, 1 = +
    private int result;
    private bool[] isCorrect;

    public Text equationText;
    public int[] FieldText; 
    

    void Start()
    {
        num1 = Random.Range(0, 100);
        num2 = Random.Range(0, 100);
        calcoperator = Random.Range(0, 1);
        if(calcoperator == 0) { result = num1 - num2; equationText.text = num1 + " - " + num2; }
        else { result = num1 + num2; equationText.text = num1 + " + " + num2; }
        for (int i = 0; i < 4; i++)
        {
            isCorrect[i] = false;

        }
        isCorrect[Random.Range(0, 4)] = true;
        for (int i = 0; i < 4; i++)
        {
            if (!isCorrect[i])
            {
                int calcoperator; //0 = -, 1 = +
                calcoperator = Random.Range(0, 1);
                if (calcoperator == 0) { FieldText[i] = result - Random.Range(1, 10); }
                else { FieldText[i] = result + Random.Range(1, 10); }
            }
            else { FieldText[i] = result; }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(result);
    }
}
