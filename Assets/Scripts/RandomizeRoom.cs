using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeRoom : MonoBehaviour
{
    public Transform resetTransform;
    public HideObject[] obstacles;
    public RoomZoom zoom;
    public Transform[] obstaclePositions;
    public Victim[] potentialVictims;
    public Friend[] friends;
    public FriendScene friendScene;
    public bool dontRandomize;
    public bool friendActive;
    [SerializeField] private CameraHandler cameraHandler;
    private List<HideObject> activeObstacles = new List<HideObject>();
    System.Random r = new System.Random();
    // Start is called before the first frame update
    void Awake()
    {
        friends = friendScene.Friends;
    }

    public void Randomize()
    {
        if(friendActive)
        {
            dontRandomize = true;
        }
        if(!dontRandomize)
        {
            RandomizeRoomObstacles();
            RandomizeRoomVictims();
        }
    }

    void RandomizeRoomObstacles()
    {
        activeObstacles.Clear();
        foreach (var position in obstaclePositions)
        {
            for(int i = 0; i < position.childCount; i++)
            {
                Destroy(position.GetChild(i).gameObject);
            }
            zoom.RoomCam.enabled = true;
            int randomObs = r.Next(0, obstaclePositions.Length);
            if (randomObs > 0)
            {
                HideObject obstacle = Instantiate(obstacles[randomObs - 1], position);
                activeObstacles.Add(obstacle);
            }
            else if (randomObs == 0)
            {
                Debug.Log("Hit Zero");
            }
        }
        cameraHandler.UpdateCameras();
    }

    void RandomizeRoomVictims()
    {
        foreach(var victim in potentialVictims)
        {
            
            foreach (Friend friend in friends)
            {
                if (friend.gameObject != victim.gameObject)
                {
                    victim.transform.position = resetTransform.position;
                }
            }
        }
        foreach (var obstacle in activeObstacles)
        {

            int randomVictim = r.Next(0, potentialVictims.Length - 1);
            // Check if the victim is in the friends array and reset their limbs if true
            if (potentialVictims[randomVictim].GetComponent<Friend>() != null)
            {
                if (potentialVictims[randomVictim].GetComponent<Friend>().beenRescued)
                    break;
            }
            GameObject victimGameObject = potentialVictims[randomVictim].gameObject;
            foreach (Friend friend in friends)
            {
                if (friend.gameObject == victimGameObject)
                {
                    Friend friendvict = potentialVictims[randomVictim].GetComponent<Friend>();
                    if(!friendvict.beenRescued)
                    {
                        friendvict.ResetLimbs();
                        friendvict.hideObject = obstacle;
                        friendvict.room = this;
                        friendActive = true;
                    }

                    break;
                }
            }
            if (randomVictim > 0)
            {
                potentialVictims[randomVictim].transform.position = obstacle.HidingSpot.transform.position;
                potentialVictims[randomVictim].transform.rotation = obstacle.HidingSpot.transform.rotation;

            }

        }
    }
}
