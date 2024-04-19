using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public bool turretAware;
    public float fireRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turretAware = true;
        }
    }
}
