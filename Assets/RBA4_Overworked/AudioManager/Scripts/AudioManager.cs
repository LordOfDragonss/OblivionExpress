using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static Action<string> PlayCall;
    public static Action<string> StopCall;
    public static Action<string> PauseCall;
    public static Action<string> UnPauseCall;

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.priority = s.priority;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }

        PlayCall += Play;
        StopCall += Stop;
        PauseCall += Pause;
        UnPauseCall += UnPause;
    }

    public void Play(string name)
    {
        Sound s = GetSoundByName(name);
        if (s == null)
        {
            SendErrorMessageForName(name);
            return;
        }
        if (s.source != null)
        {
            s.source.Play();
        }

    }

    public void Stop(string name)
    {
        Sound s = GetSoundByName(name);
        if (s == null)
        {
            SendErrorMessageForName(name);
            return;
        }
        if (s.source != null)
        {
            s.source.Stop();
        }

    }

    public void Pause(string name)
    {
        Sound s = GetSoundByName(name);
        if (s == null)
        {
            SendErrorMessageForName(name);
            return;
        }
        if (s.source != null)
        {
            s.source.Pause();
        }
    }

    public void UnPause(string name)
    {
        Sound s = GetSoundByName(name);
        if (s == null)
        {
            SendErrorMessageForName(name);
            return;
        }
        if (s.source != null)
        {
            s.source.UnPause();
        }
    }

    public Sound GetSoundByName(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            SendErrorMessageForName(name);
            return null;
        }
        return s;
    }

    public void SendErrorMessageForName(string name)
    {
        Debug.LogWarning("Audio of " + name + "not found");
    }
}
