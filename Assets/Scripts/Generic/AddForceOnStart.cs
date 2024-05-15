using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnStart : MonoBehaviour
{
    [SerializeField] ForceMode forceMode;
    [SerializeField] float horizontalRangeMin;
    [SerializeField] float horizontalRangeMax;
    [SerializeField] float verticalRangeMin;
    [SerializeField] float verticalRangeMax;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.Range(horizontalRangeMin, horizontalRangeMax),
        Random.Range(verticalRangeMin, verticalRangeMax),Random.Range(horizontalRangeMin, horizontalRangeMax), forceMode);
    }
}
