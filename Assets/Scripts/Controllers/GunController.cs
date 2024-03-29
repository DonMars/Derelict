using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] new Camera camera;

    [Header("Fire Rate")]
    public float fireRate = 30f;
    public float nextTimeToFire = 0;

    [Header("Damage")]
    public int normalDamage = 10;

    // Random Damage
    public bool dealsRandomDamage;
    public int randomDamageMin = 1;
    public int randomDamageMax = 3;
    int randomDamage;

    // Critical Chance
    public bool hasCriticalChance;
    [Range(0, 100)] public int criticalChance;
    public int criticalDamage;
    public bool isCriticalDamageMultiplicative;
    public bool isCriticalDamageRandom;
    public int criticalDamageMin;
    public int criticalDamageMax;
    bool isDamageCritical = false;
    int randomCriticalDamage;

    [Header("Energy")]
    public float weaponEnergy = 0f;
    public float weaponEnergyMax = 100f;
    public float energyUse = 6.5f;
    public float energyRechargeRate = 0.1f;
    public float overchargedEnergyRechargeRate = 0.5f;
    public float overchargeDelay = 5f;
    public bool canShoot = true;
    float energyRechargeRateOriginal;
    bool isOvercharged = false;

    [Header("Range")]
    [SerializeField] float range = 100f;

    [Header("Pysical Force")]
    public float impactForce = 32f;

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

    private void Awake()
    {
        energyRechargeRateOriginal = energyRechargeRate;
    }

    private void Update()
    {
        // Energy
        if (!isOvercharged)
            weaponEnergy -= energyRechargeRate;
        else if (isOvercharged)
            StartCoroutine(RechargeDelay());

        if (weaponEnergy >= weaponEnergyMax && !isOvercharged)
        {
            weaponEnergy = weaponEnergyMax;
            isOvercharged = true;
        }
        else if (weaponEnergy <= 0)
            weaponEnergy = 0;
        else if (weaponEnergy <= energyUse)
            isOvercharged = false;

        // Shooting
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !isOvercharged && canShoot)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Energy Use
        weaponEnergy += energyUse;

        // Particle & FX
        muzzleFlash.Play();
        Instantiate(trailEffect, shootPoint.position, shootPoint.rotation);
        laserShotSFX.Play();
        laserShotSFX2.Play();

        // Random Damage Control
        if (dealsRandomDamage)
        {
            randomDamage = Random.Range(randomDamageMin, randomDamageMax + 1);

            normalDamage = randomDamage;
        }

        // Critical Damage Control
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
                    randomCriticalDamage = Random.Range(randomDamageMin, randomDamageMax + 1);
                    normalDamage += randomCriticalDamage;
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
                // Damage Apply
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

                    // Critical Damage Reset
                    if (isCriticalDamageMultiplicative)
                        normalDamage *= -criticalDamage;
                    else if (isCriticalDamageRandom)
                        normalDamage -= randomCriticalDamage;
                    else
                        normalDamage -= criticalDamage;

                    isDamageCritical = false;
                }
            }

            // Critical Damage Reset
            if (isDamageCritical)
            {
                if (isCriticalDamageMultiplicative)
                    normalDamage *= -criticalDamage;
                else if (isCriticalDamageRandom)
                    normalDamage -= randomCriticalDamage;
                else
                    normalDamage -= criticalDamage;

                isDamageCritical = false;
            }

            // Force Apply
            if (hit.rigidbody != null && !hit.collider.isTrigger)
                hit.rigidbody.AddForce(-hit.normal * impactForce * 20f);

            // On-hit Effects
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                
            if(leavesDecal && hit.collider.gameObject.isStatic)
                Instantiate(impactDecal, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    IEnumerator RechargeDelay()
    {
        canShoot = false;
        energyRechargeRate = overchargedEnergyRechargeRate;
        yield return new WaitForSeconds(overchargeDelay);
        isOvercharged = false;
        yield return new WaitUntil(() => weaponEnergy == 0);
        energyRechargeRate = energyRechargeRateOriginal;
        canShoot = true;
    }
}
