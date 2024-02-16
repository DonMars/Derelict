using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [SerializeField] float destroyTime = 3f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
