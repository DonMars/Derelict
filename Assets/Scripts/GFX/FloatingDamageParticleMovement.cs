using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageParticleMovement : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 rbForce = new Vector3(Random.Range(-5, 5), Random.Range(4, 6), Random.Range(-5, 5));

        rb.AddForce(rbForce, ForceMode.Force);
    }
}
