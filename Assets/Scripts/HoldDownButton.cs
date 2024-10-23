using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUpButton : Selectable, IPointerDownHandler, IPointerUpHandler
{
    public float speedUpSpeed;

    public void HideButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void DisplayButton()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = speedUpSpeed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1;
    }
}
