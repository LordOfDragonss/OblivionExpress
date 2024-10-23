using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUpButton : Selectable, IPointerDownHandler, IPointerUpHandler
{
    public float speedUpSpeed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = speedUpSpeed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1;
    }
}
