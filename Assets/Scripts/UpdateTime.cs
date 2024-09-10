using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTime : MonoBehaviour
{
    public TextMeshProUGUI time;
    private float timer;
    public CourierMovement couriermovement;
    // Start is called before the first frame update
    void Start()
    {
        timer = couriermovement.TimeTillCourier;
    }

    // Update is called once per frame
    void Update()
    {
        if (couriermovement.CountdownStarted)
        {
            timer -= Time.deltaTime;
            time.text = string.Format("{0:00}", timer);
        }
    }
}
