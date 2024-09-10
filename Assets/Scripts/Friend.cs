using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    public Player player;
    public GameObject[] limbs;
    [SerializeField] Transform[] OriginalPositions;
    [SerializeField] Outline outline;

    private void Awake()
    {
        OriginalPositions = new Transform[limbs.Length];
        outline = GetComponent<Outline>();
        SavePositions();
    }
    private void OnMouseDown()
    {
        if (player.canHide)
        {
            player.AddFriend();
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
            item.transform.position = Vector3.zero;
            item.transform.rotation = Quaternion.identity;
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
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].transform.position = OriginalPositions[i].position;
            limbs[i].transform.rotation = OriginalPositions[i].rotation;
        }
    }
}
