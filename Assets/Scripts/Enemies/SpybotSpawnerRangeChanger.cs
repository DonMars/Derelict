using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpybotSpawnerRangeChanger : MonoBehaviour
{
    public float radiusMinValue;
    public float radiusMaxValue;
    float radiusValue;
    SphereCollider radiusToChange;

    private void Awake()
    {
        radiusToChange = GetComponent<SphereCollider>();
    }

    void Start()
    {
        radiusValue = Random.Range(radiusMinValue, radiusMaxValue);

        radiusToChange.radius = radiusValue;
    }
}
