using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactSFXSelector : MonoBehaviour
{
    public AudioSource sfx1;
    public AudioSource sfx2;

    void Start()
    {
        int selector = Random.Range(1, 3);

        if (selector == 1)
        {
            sfx1.pitch = Random.Range(0.9f, 1.1f);
            sfx1.Play();
        }
        else if (selector == 2)
        {
            sfx2.pitch = Random.Range(0.9f, 1.1f);
            sfx2.Play();
        }
    }
}
