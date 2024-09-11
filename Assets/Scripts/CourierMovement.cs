using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CourierMovement : MonoBehaviour
{
    public Player player;
    public ExitButton exitButton;
    public List<GameObject> Locations;
    [SerializeField] int Locationindex;
    public RandomizeRoom[] rooms;
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera murderCam;
    public float Speed;
    float OgSpeed;
    [SerializeField] float MurderSpeed;
    public float rotationSpeed;
    [SerializeField] bool cyclestarted;
    bool cycleended;
    public UpdateTime timerUpdater;
    [SerializeField] public float timer;
    public bool CountdownStarted;
    public float TimeTillCourier;
    public CameraHandler CameraHandler;
    public bool murderStart;
    public bool murdering;
    public float MurderTime;
    public Victim firstVictim;
    // Start is called before the first frame update
    void Start()
    {
        Locationindex = 0;
        cyclestarted = false;
        OgSpeed = Speed;
        rooms = FindObjectsOfType<RandomizeRoom>();
        AudioManager.PlayCall("ShadowOfDeath");
    }

    // Update is called once per frame
    void Update()
    {
        if (murderStart)
        {
            AudioManager.PlayCall("Breath");
            CameraHandler.SwitchToCamera(murderCam);
            CameraHandler.RoomAngle();
            murderStart =false;
            player.canHide = false;
            murdering = true;
        }
        if (murdering)
        {
            Speed = MurderSpeed;
            timer += Time.deltaTime;
            firstVictim.Dissapear();
            transform.position = Vector3.MoveTowards(transform.position, Locations[Locationindex].transform.position, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Locations[Locationindex].transform.rotation, rotationSpeed * Time.deltaTime);
            if (timer >= MurderTime)
            {
                Locations.Remove(Locations[Locationindex]);
                murdering = false;
                StartGame();
            }
        }
        if (CountdownStarted)
            timer += Time.deltaTime;
        if (timer >= TimeTillCourier)
        {
            CountdownStarted = false;
            AudioManager.PlayCall("Horns");
            StartCycle();
        }
        if (cyclestarted)
        {
            MoveTroughCycle();
        }
        if (cycleended)
        {
            timerUpdater.timer = TimeTillCourier;
            Locationindex = 0;
            transform.position = Locations[Locationindex].transform.position;
            cycleended = false;
            StartGame();
            foreach(RandomizeRoom room in rooms)
            {
                room.Randomize();
            }
        }
    }

    public void StartGame()
    {
        timer = 0;
        CameraHandler.SwitchToCamera(CameraHandler.mainCam);
        CountdownStarted = true;
        player.canHide = true;
    }

    public void StartCycle()
    {
        cyclestarted = true;
        Speed = OgSpeed;
        player.canHide = false;
        exitButton.HideButton();
        CameraHandler.SwitchToCamera(followCam);
        CameraHandler.RemoveDoorWall();
    }

    public void MoveTroughCycle()
    {

        if (Locationindex >= Locations.Count)
        {
            cycleended = true;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, Locations[Locationindex].transform.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Locations[Locationindex].transform.rotation, rotationSpeed * Time.deltaTime);
        if (transform.position == Locations[Locationindex].transform.position && transform.rotation == Locations[Locationindex].transform.rotation)
        {
            cyclestarted = false;
            Locationindex++;
        }

    }
}
