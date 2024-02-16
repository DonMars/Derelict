using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera camera;

    [Header("Fire Rate")]
    [SerializeField] float fireRate = 30f;
    [SerializeField] float nextTimeToFire = 0;

    [Header("Damage")]
    [SerializeField] int normalDamage = 10;
    // Random Damage
    [SerializeField] bool dealsRandomDamage;
    [SerializeField] int randomDamageMin = 1;
    [SerializeField] int randomDamageMax = 3;
    // Critical Chance
    [SerializeField] bool hasCriticalChance;
    [Range(0, 100)] [SerializeField] int criticalChance;
    [SerializeField] int criticalDamage;
    [SerializeField] bool isCriticalDamageMultiplicative;
    [SerializeField] bool isCriticalDamageRandom;
    [SerializeField] int criticalDamageMin;
    [SerializeField] int criticalDamageMax;
    bool isDamageCritical = false;

    [Header("Range")]
    [SerializeField] float range = 100f;

    [Header("Pysical Force")]
    [SerializeField] float impactForce = 32f;

    [Header("SFX")]
    [SerializeField] AudioSource laserShotSFX;
    [SerializeField] AudioSource laserShotSFX2;


    [Header("GFX")]
    [SerializeField] Transform shootPoint;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;
    [SerializeField] bool leavesDecal;
    [SerializeField] GameObject impactDecal;
    [SerializeField] Transform impactDecalTransform;
    [SerializeField] GameObject trailEffect;
    [SerializeField] GameObject floatingDamage;
    [SerializeField] GameObject floatingCriticalDamage;

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
        // Particle & FX
        muzzleFlash.Play();
        Instantiate(trailEffect, shootPoint.position, shootPoint.rotation);
        laserShotSFX.Play();
        laserShotSFX2.Play();

        // Damage Control
        if (dealsRandomDamage)
        {
            int newDamage = Random.Range(randomDamageMin, randomDamageMax + 1);

            normalDamage = newDamage;
        }

        if (hasCriticalChance)
        {
            int chance = Random.Range(1, 101);

            if (chance < criticalChance)
            {
                isDamageCritical = true;

                if (isCriticalDamageMultiplicative)
                    normalDamage *= criticalDamage;
                else if (isCriticalDamageRandom)
                {
                    int randomDamage = Random.Range(randomDamageMin, randomDamageMax + 1);
                    normalDamage += randomDamage;
                }
                else
                {
                    normalDamage += criticalDamage;
                }
            }
        }

        // Raycast Control
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target != null)
            {
                target.TakeDamage(normalDamage);

                // Floating Damage
                GameObject newInstance = Instantiate(floatingDamage, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject newInstance2 = Instantiate(floatingCriticalDamage, hit.point, Quaternion.LookRotation(hit.normal));

                if (!isDamageCritical)
                {
                    Instantiate(newInstance);
                    newInstance.GetComponentInChildren<TextMeshPro>().text = normalDamage.ToString();
                }
                else
                {
                    Instantiate(newInstance2);
                    newInstance2.GetComponentInChildren<TextMeshPro>().text = normalDamage.ToString();
                    isDamageCritical = false;
                }
            }

            if (hit.rigidbody != null && !hit.collider.isTrigger)
                hit.rigidbody.AddForce(-hit.normal * impactForce * 20f);

            // On-hit Effects
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            if(leavesDecal && hit.collider.gameObject.isStatic)
                Instantiate(impactDecal, hit.point, Quaternion.LookRotation(hit.normal));

            Debug.Log(hit.collider.gameObject.name);
        }
    }

    void ShowFloatingText()
    {

    }
}
