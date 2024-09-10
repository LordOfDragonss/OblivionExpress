using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public AudioSource[] audioClips;
    public GameObject[] Scene;
    [SerializeField] float timer;
    public float CutsceneTime;
    public bool SceneStarted;
    private void Start()
    {
        foreach (GameObject gO in Scene)
        {
            gO.SetActive(false);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (SceneStarted)
        {
            if(timer > CutsceneTime)
            {
                StopScene();
                SceneManager.LoadScene("Death");
            }
        }
    }

    public void StartScene()
    {
        timer = 0;
        SceneStarted = true;
        foreach(AudioSource clip in audioClips)
        {
            clip.Play();
        }
        foreach(GameObject gO in Scene)
        {
            gO.SetActive(true);
        }
    }

    private void StopScene()
    {
        SceneStarted = false;
        foreach (AudioSource clip in audioClips)
        {
            clip.Stop();
        }
        foreach (GameObject gO in Scene)
        {
            gO.SetActive(false);
        }
    }

}
