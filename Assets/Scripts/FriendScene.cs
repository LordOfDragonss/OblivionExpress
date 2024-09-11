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
    public GameObject flbutton;
    public TextMeshProUGUI text;
    public TextMeshProUGUI ExitButtonText;
    public ExitButton exitButton;
    public bool FirstTime;

    private void Awake()
    {
        text.enabled = false;
        roomrandomizers = FindObjectsOfType<RandomizeRoom>();
        foreach (RandomizeRoom r in roomrandomizers)
        {
            r.friendScene = this;
        }
        FirstTime = true;
        player.FriendsCount = Friends.Length;
    }

    private void Update()
    {
        if (!player.canHide)
        {
            flbutton.SetActive(false);
        }
        if (player.canHide)
        {
            flbutton.SetActive(true);
        }
    }

    public void StartScene()
    {
        player.canHide = false;
        player.isOnFriendList = true;
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
        player.isOnFriendList = false;
        ExitButtonText.text = "Exit Room";
        exitButton.HideButton();
        foreach (Friend friend in Friends)
        {
            friend.BackToHidingSpot();
        }
        if (player.isHiding)
        {
            player.Hide(player.hideObject);
        }
        text.enabled = false;
        CameraHandler.UpdateCameras();
        if (FirstTime)
        {
            FirstTime = false;
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
