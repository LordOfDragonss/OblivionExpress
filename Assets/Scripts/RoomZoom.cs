using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomZoom : MonoBehaviour
{
    public CinemachineVirtualCamera RoomCam;
    [SerializeField] CameraHandler cameraHandler;
    [SerializeField] GameObject PlayerPosition;
    private Player player;
    public Outline[] outlines;
    [SerializeField] private ExitButton exitButton;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        exitButton = FindObjectOfType<ExitButton>();
        foreach (var outline in outlines)
        {
            outline.OutlineMode = Outline.Mode.OutlineHidden;
        }
    }
    private void OnMouseDown()
    {
        if (player.canHide)
        {
            cameraHandler.SwitchToCamera(RoomCam);
            cameraHandler.RoomAngle();
            if (!player.isHiding && PlayerPosition != null)
                player.Hide(PlayerPosition);
            exitButton.EnableButton();
        }
    }
    private void OnMouseEnter()
    {
        if (player.canHide)
        {
            foreach (var outline in outlines)
            {
                outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            }
        }

    }
    private void OnMouseExit()
    {
        if (player.canHide)
        {
            foreach (var outline in outlines)
            {
                outline.OutlineMode = Outline.Mode.OutlineHidden;
            }
        }
    }
}
