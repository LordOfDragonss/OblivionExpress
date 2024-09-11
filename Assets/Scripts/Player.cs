using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isHiding;
    public bool isOnFriendList;
    public bool canHide;
    public GameObject hideObject;

    public int FriendsRescued;
    public int FriendsCount;
    public TextMeshProUGUI friendtracker;
    public void Hide(GameObject Spot)
    {
        isHiding = true;
        hideObject = Spot;
        transform.position = Spot.transform.position;
        if(Spot.transform.rotation != Quaternion.identity)
        {
            transform.rotation = Spot.transform.rotation;
        }

    }

    private void Update()
    {
        friendtracker.text = "Friends Rescued: " + FriendsRescued.ToString() + "/" + FriendsCount.ToString();
        if(FriendsRescued == FriendsCount )
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public void AddFriend()
    {
        FriendsRescued++;
    }
}
