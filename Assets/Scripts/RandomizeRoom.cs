using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeRoom : MonoBehaviour
{
    public HideObject[] obstacles;
    public Transform[] obstaclePositions;
    public Victim[] potentialVictims;
    public Friend[] friends;
    public FriendScene friendScene;
    public bool dontRandomize;
    [SerializeField] bool friendActive;
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
            cameraHandler.UpdateCameras();
        }
    }

    void RandomizeRoomObstacles()
    {
        foreach (var position in obstaclePositions)
        {
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
    }

    void RandomizeRoomVictims()
    {
        foreach (var obstacle in activeObstacles)
        {

            int randomVictim = r.Next(0, potentialVictims.Length - 1);
            // Check if the victim is in the friends array and reset their limbs if true
            GameObject victimGameObject = potentialVictims[randomVictim].gameObject;
            foreach (Friend friend in friends)
            {
                if (friend.gameObject == victimGameObject)
                {
                    potentialVictims[randomVictim].GetComponent<Friend>().ResetLimbs();
                    friendActive = true;
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
