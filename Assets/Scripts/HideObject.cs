using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public CameraHandler CameraHandler;
    public Outline outline;
    public GameObject HidingSpot;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        CameraHandler = FindObjectOfType<CameraHandler>();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    private void OnMouseEnter()
    {
        if (player.canHide)
            outline.OutlineMode = Outline.Mode.OutlineVisible;
    }

    private void OnMouseDown()
    {
        if (player.canHide)
            Hide();
    }
    private void OnMouseExit()
    {
        if(player.canHide)
        outline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    private void Hide()
    {
        CameraHandler.SwitchToCamera(cam);
        CameraHandler.HideAngle();
        player.Hide(HidingSpot);
    }
}
