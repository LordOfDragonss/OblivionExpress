using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDetection : MonoBehaviour
{
    public DeathScene DeathScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathScene.StartScene();
        }
        if(other.gameObject.tag == "Victim")
        {
            other.GetComponent<Victim>().Dissapear();
        }
    }
}
