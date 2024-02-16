using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField] float destroyInSeconds = 3;

    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
