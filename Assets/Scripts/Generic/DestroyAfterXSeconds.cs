using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField, Tooltip("Time in seconds to destroy this Game Object")]
    float destroyInSeconds = 3;

    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
