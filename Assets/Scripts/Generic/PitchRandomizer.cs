using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    public AudioSource sfxToPitch1;
    public float pitch1Min;
    public float pitch1Max;
    public AudioSource sfxToPitch2;
    public float pitch2Min;
    public float pitch2Max;
    public AudioSource sfxToPitch3;
    public float pitch3Min;
    public float pitch3Max;

    private void Awake()
    {
        float pitch1 = Random.Range(pitch1Min, pitch1Max);
        sfxToPitch1.pitch = pitch1;

        if (sfxToPitch2 != null)
        {
            float pitch2 = Random.Range(pitch2Min, pitch2Max);
            sfxToPitch2.pitch = pitch2;
        }
        
        if (sfxToPitch3 != null)
        {
            float pitch3 = Random.Range(pitch3Min, pitch3Max);
            sfxToPitch3.pitch = pitch3;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
