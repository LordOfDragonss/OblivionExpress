using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineVirtualCamera mainCam;
    public Camera mainCamera;
    public LayerMask MainMask;
    public LayerMask FollowMask;
    public LayerMask HideMask;
    public LayerMask RoomMask;
    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public FriendScene friendScene;
    void Start()
    {
        UpdateCameras();
        friendScene.StartScene();
    }

    public void UpdateCameras()
    {
        cameras.Clear();
        cameras = FindObjectsOfType<CinemachineVirtualCamera>(true).ToList();
        foreach (var cam in cameras)
        {
            if (cam != mainCam)
                cam.enabled = false;
        }
    }

    public void RemoveDoorWall()
    {
        mainCamera.cullingMask = FollowMask;
    }

    public void HideAngle()
    {
        mainCamera.cullingMask = HideMask;
    }
    public void RoomAngle()
    {
        mainCamera.cullingMask = RoomMask;
    }

    public void SwitchToCamera(CinemachineVirtualCamera CamToSwap)
    {
        if (CamToSwap == mainCam)
        {
            mainCamera.cullingMask = MainMask;
        }
        foreach (var cam in cameras)
        {
            if (cam == CamToSwap)
            {
                CamToSwap.enabled = true;
                cam.enabled = true;
            }
            else
            {
                if (cam != null)
                    cam.enabled = false;
            }

        }
    }
}
