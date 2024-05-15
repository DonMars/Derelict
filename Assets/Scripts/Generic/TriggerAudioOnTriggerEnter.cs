using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioOnTriggerEnter : MonoBehaviour
{
    public AudioSource audioToPlay;
    public bool playOnce = true;
    bool playOnceSwitch = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnce && !playOnceSwitch)
            {
                playOnceSwitch = true;
                audioToPlay.Play();
            }
            else if (!playOnce)
            {
                audioToPlay.Play();
            }
        }
    }

}
