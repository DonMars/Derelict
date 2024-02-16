using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
// Start is called before the first frame update
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(-3, 3)]
    public float pitch = 1;
    [Range(0,1)]
    public float volume = 1;

    [HideInInspector]
    public AudioSource source;
}
