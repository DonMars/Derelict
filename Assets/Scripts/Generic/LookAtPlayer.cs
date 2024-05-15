using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public float lookSpeed = .96f;
    Transform player;
    public Quaternion rotation;
    bool lookSwitch = false;
    TurretBehavior turret;
    float lookSpeedBackup;
    bool speedChangeSwitch = true;

    void Start()
    {   
        player = FindAnyObjectByType<FirstPersonController>().gameObject.transform;
        turret = GetComponentInParent<TurretBehavior>();
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        rotation = Quaternion.Lerp(Quaternion.LookRotation(direction), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), lookSpeed);
        transform.rotation = rotation;
    }
}
