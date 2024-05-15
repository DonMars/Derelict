using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject projectile;

    [Header("Bullet Force")]
    public float shootForce;
    public bool isForceRandom = false;
    public float forceMin;
    public float forceMax;

    [Header("Gun Stats")]
    public float timeBetweenShooting;
    public float spread;
    public float timeBetweenShots;
    public int projectilesPerTap;

    [Header("SFX")]
    public AudioSource turretIdleSFX;
    public AudioSource turretIdle2SFX;
    public AudioSource turretActivateSFX;
    public AudioSource turretActivate2SFX;
    public AudioSource turretDeactivateSFX;
    public AudioSource turretShootSFX;
    public AudioSource turretDestroySFX;

    // Bools
    bool shooting;

    [Header("References")]
    public Transform turretHead;
    public Transform attackPoint;
    public GameObject explosion2VFX;
    public Transform explosionSpawnPoint;
    Transform player;

    [Header("Rate of Fire")]
    public float turretCooldown = 2.2f;
    public float shootCooldown;
    bool shootSwitch = false;

    public bool turretAware = false;
    bool turretSwitch = false;


    public Animator turretAnimator;

    LookAtPlayer followPlayer;
    ObjectRotate idleRotate;

    private void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>().gameObject.transform;

        turretAnimator = GetComponent<Animator>();
        followPlayer = GetComponentInChildren<LookAtPlayer>();
        idleRotate = GetComponentInChildren<ObjectRotate>();

        if (isForceRandom)
        {
            float newForce = Random.Range(forceMin, forceMax);
            shootForce = newForce;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turretAware = true;
            turretActivateSFX.Play();
            turretActivate2SFX.pitch = 0.65f;
            turretActivate2SFX.Play();
            turretIdleSFX.Stop();
            turretIdle2SFX.Stop();
            shootSwitch = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && turretAware)
        {
            turretAware = false;
            StopAllCoroutines();
            StartCoroutine(Cooldown());
        }
    }

    private void Update()
    {
        if (turretAware && !turretSwitch)
        {
            turretSwitch = true;
            turretAnimator.ResetTrigger("bodyUp");
            turretAnimator.SetTrigger("bodyDown");
            idleRotate.enabled = false;
            followPlayer.enabled = true;
            StartCoroutine(Shoot());
        }
    }

    public void InstantiateExplosion2()
    {
        Instantiate(explosion2VFX.gameObject, explosionSpawnPoint.transform.position, Quaternion.identity);
    }

    IEnumerator Shoot()
    {
        if (!shootSwitch)
        {
            yield return new WaitForSeconds(shootCooldown);
            shootSwitch = true;
        }
        yield return new WaitForSeconds(timeBetweenShooting);
        GameObject shot = Instantiate(projectile, attackPoint.position, turretHead.rotation);
        shot.GetComponent<Rigidbody>().AddForce(turretHead.forward * shootForce, ForceMode.Impulse);
        turretShootSFX.pitch = Random.Range(0.8f, 1.6f);
        turretShootSFX.Play();
        StartCoroutine(Shoot());
    }

    IEnumerator Cooldown()
    {   
        followPlayer.enabled = false;
        yield return new WaitForSeconds(turretCooldown);
        idleRotate.enabled = true;
        turretIdleSFX.Play();
        turretIdle2SFX.Play();
        yield return new WaitForSeconds(.8f);
        turretAnimator.ResetTrigger("bodyDown");
        turretAnimator.SetTrigger("bodyUp");
        turretActivate2SFX.pitch = .3f;
        turretActivate2SFX.Play();
        turretSwitch = false;
    }
}
