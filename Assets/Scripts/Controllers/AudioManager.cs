using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Scripted Instances
    public bool musicStart1 = true;

    public Music[] music;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach(Music m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();

            m.source.clip = m.clip;
            m.source.loop = m.loop;
            m.source.pitch = m.pitch;
            m.source.volume = m.volume;
        }
    }

    public void Play(string name)
    {
        foreach(Music m in music)
        {
            if(m.name == name)
            {
                m.source.Play();
                return;
            }
        }

        Debug.Log("No EXISTE el clip " + name);
    }

    public void Stop(string name)
    {
        foreach (Music m in music)
        {
            if (m.name == name)
            {
                m.source.Stop();
                return;
            }
        }

        Debug.Log("No EXISTE el clip " + name);
    }
}
