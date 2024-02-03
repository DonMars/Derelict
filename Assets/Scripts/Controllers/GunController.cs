using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [Header("Fire Rate")]
    [SerializeField] private float fireRate = 30f;
    [SerializeField] private float nextTimeToFire = 0;

    [Header("Damage")]
    [SerializeField] private float normalDamage = 10f;

    [Header("Range")]
    [SerializeField] private float range = 100f;

    [Header("Pysical Force")]
    [SerializeField] private float impactForce = 32f;

    [Header("SFX")]
    [SerializeField] private AudioSource laserShotSFX;
    [SerializeField] private AudioSource laserShotSFX2;


    [Header("GFX")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject trailEffect;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        Instantiate(trailEffect, shootPoint.position, shootPoint.rotation);
        laserShotSFX.Play();
        laserShotSFX2.Play();

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(normalDamage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce * 20f);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
