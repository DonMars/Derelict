using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Normal Flicker")]
    public Light lightToFlicker;
    public float flickerValueMin;
    public float flickerValueMax;
    public float flickerDelay;

    [Header("Flicker Chance")]
    public bool hasChance = false;
    [Range(1, 100)] public int chance;

    void Start()
    {
        if (hasChance)
        {
            int chanceRoll = Random.Range(1, 101);

            if (chanceRoll <= chance)
            {
                StartCoroutine(LightFlick());
            }
        }
        else if (!hasChance)
        {
            StartCoroutine(LightFlick());
        }
    }

    IEnumerator LightFlick()
    {
        float flickerValue = Random.Range(flickerValueMin, flickerValueMax);
        lightToFlicker.intensity = flickerValue;
        yield return new WaitForSeconds(flickerDelay);
        StartCoroutine(LightFlick());
    }
}
