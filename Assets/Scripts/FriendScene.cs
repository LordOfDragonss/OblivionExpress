using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendScene : MonoBehaviour
{
    public RandomizeRoom[] roomrandomizers;
    public CourierMovement conductor;
    public Player player;
    public Friend[] Friends;
    public CinemachineVirtualCamera cam;
    public CameraHandler CameraHandler;
    public TextMeshProUGUI text;
    public TextMeshProUGUI ExitButtonText;
    public ExitButton exitButton;
    bool firsttime;

    private void Awake()
    {
        text.enabled = false;
        roomrandomizers = FindObjectsOfType<RandomizeRoom>();
        foreach (RandomizeRoom r in roomrandomizers)
        {
            r.friendScene = this;
        }
        firsttime = true;
    }

    private void Start()
    {
        StartScene();
    }

    public void StartScene()
    {
        player.canHide = false;
        CameraHandler.SwitchToCamera(cam);
        text.enabled = true;
        ExitButtonText.text = "Exit Screen";
        exitButton.EnableButton();
        foreach (Friend friend in Friends)
        {
            friend.GoBackToFriendScene();
        }

    }
    public void EndScene()
    {
        player.canHide = true;
        ExitButtonText.text = "Exit Room";
        exitButton.HideButton();
        text.enabled = false;
        CameraHandler.UpdateCameras();
        if (firsttime)
        {
            firsttime = false;
            conductor.murderStart = true;
            foreach (var room in roomrandomizers)
            {
                if (!room.dontRandomize)
                {
                    room.Randomize();
                }
            }
        }
    }

}
