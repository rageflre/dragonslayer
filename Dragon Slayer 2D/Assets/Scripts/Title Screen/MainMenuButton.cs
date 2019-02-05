using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);

    }

    void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 255);

    }
}
