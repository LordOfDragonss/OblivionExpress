using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    public Player player;
    public GameObject[] limbs;
    public GameObject MapPosition;
    [SerializeField] Transform[] OriginalPositions;
    [SerializeField] Outline outline;
    public HideObject hideObject;
    bool beenRescued;

    private void Awake()
    {
        OriginalPositions = new Transform[limbs.Length];
        outline = GetComponent<Outline>();
        SavePositions();
    }
    private void OnMouseDown()
    {
        if (player.canHide && !beenRescued)
        {
            player.AddFriend();
            AudioManager.PlayCall("ping");
            beenRescued = true;
        }
    }

    private void Update()
    {
        if (beenRescued)
        {
            GoBackToFriendScene();
            outline.OutlineColor = Color.yellow;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }

    private void OnMouseEnter()
    {
        if (player.canHide)
            outline.OutlineMode = Outline.Mode.OutlineVisible;
    }
    private void OnMouseExit()
    {
        if (player.canHide)
            outline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    public void ResetLimbs()
    {
        foreach (var item in limbs)
        {
            item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }
    public void BackToHidingSpot()
    {
        if(hideObject != null)
        {
            transform.position = hideObject.HidingSpot.transform.position;
            transform.rotation = hideObject.HidingSpot.transform.rotation;
        }
    }

    public void SavePositions()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            OriginalPositions[i] = limbs[i].transform;
        }
    }

    public void GoBackToFriendScene()
    {
        transform.position = MapPosition.transform.position;
        transform.rotation = MapPosition.transform.rotation;
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].transform.position = OriginalPositions[i].position;
            limbs[i].transform.rotation = OriginalPositions[i].rotation;
        }
    }
}
