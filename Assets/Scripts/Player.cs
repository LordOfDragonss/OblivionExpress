using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isHiding;
    public bool canHide;

    public int FriendsRescued;
    public TextMeshProUGUI friendtracker;
    public void Hide(GameObject Spot)
    {
        transform.position = Spot.transform.position;
        if(Spot.transform.rotation != Quaternion.identity)
        {
            transform.rotation = Spot.transform.rotation;
        }

    }

    private void Update()
    {
        friendtracker.text = FriendsRescued.ToString();
    }

    public void AddFriend()
    {
        FriendsRescued++;
    }
}
