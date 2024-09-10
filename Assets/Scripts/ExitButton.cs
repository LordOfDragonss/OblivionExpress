using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public CameraHandler CameraHandler;
    public Player player;
    public GameObject PlayerPosition;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        HideButton();
    }
    public void ExitRoom()
    {
        player.Hide(PlayerPosition);
        CameraHandler.SwitchToCamera(CameraHandler.mainCam);
        HideButton();
    }
    public void EnableButton()
    {
        gameObject.SetActive(true);
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }
}
