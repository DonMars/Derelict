using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightToFlicker;
    public float flickerValueMin;
    public float flickerValueMax;
    public float flickerDelay;

    void Start()
    {
        StartCoroutine(LightFlick());
    }

    IEnumerator LightFlick()
    {
        float flickerValue = Random.Range(flickerValueMin, flickerValueMax);
        lightToFlicker.intensity = flickerValue;
        yield return new WaitForSeconds(flickerDelay);
        StartCoroutine(LightFlick());
    }
}
