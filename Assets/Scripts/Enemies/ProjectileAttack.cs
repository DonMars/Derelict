using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileAttack : MonoBehaviour
{
    public float waitTimeDetectEnemy = 0.1f;
    public bool enemyDetect = false;
    bool damageSwitch = false;

    [Header("Damage Lv. 1")]
    public int lvl1DamageMinValue = 4;
    public int lvl1DamageMaxValue = 6;
    int level1Damage;

    [Header("Damage Lv. 3")]
    public int lvl3DamageMinValue = 10;
    public int lvl3DamageMaxValue = 14;
    int level3Damage;

    [Header("Damage Lv. 5")]
    public int lvl5DamageMinValue;
    public int lvl5DamageMaxValue;
    int level5Damage;

    public GameObject impactSFX;
    public GameObject playerImpactSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            ImpactAudio();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.level > 0)
            {
                level1Damage = Random.Range(lvl1DamageMinValue, lvl1DamageMaxValue + 1);
                FirstPersonController.OnTakeDamage(level1Damage);
                ImpactPlayerAudio();
                Destroy(gameObject);
            }
            else if (GameManager.Instance.level > 2)
            {
                level3Damage = Random.Range(lvl3DamageMinValue, lvl3DamageMaxValue + 1);
                FirstPersonController.OnTakeDamage(level3Damage);
                ImpactPlayerAudio();
                Destroy(gameObject);
            }
            else if (GameManager.Instance.level > 4)
            {
                level5Damage = Random.Range(lvl5DamageMinValue, lvl5DamageMaxValue + 1);
                FirstPersonController.OnTakeDamage(level5Damage);
                ImpactPlayerAudio();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Enemy") && !enemyDetect && !other.GetComponent<BoxCollider>().isTrigger)
        {
            StartCoroutine(DetectEnemy());
        }
        else if (other.CompareTag("Enemy") && enemyDetect && !other.GetComponent<BoxCollider>().isTrigger && !damageSwitch)
        {

            Collider sphereCollider = other.transform.GetComponent<SphereCollider>();

            sphereCollider.enabled = false;

            if (GameManager.Instance.level > 0)
            {
                EnemyHealth enemyHealth = other.transform.GetComponent<EnemyHealth>();

                level1Damage = Random.Range(lvl1DamageMinValue, lvl1DamageMaxValue + 1);

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(level1Damage);
                }
            }
            else if (GameManager.Instance.level > 2)
            {
                EnemyHealth enemyHealth = other.transform.GetComponent<EnemyHealth>();

                level3Damage = Random.Range(lvl3DamageMinValue, lvl3DamageMaxValue + 1);

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(level3Damage);
                }
            }
            else if (GameManager.Instance.level > 4)
            {
                EnemyHealth enemyHealth = other.transform.GetComponent<EnemyHealth>();

                level5Damage = Random.Range(lvl5DamageMinValue, lvl5DamageMaxValue + 1);

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(level3Damage);
                }
            }

            damageSwitch = true;

            sphereCollider.enabled = true;

            ImpactAudio();
            Destroy(gameObject);
        }
    }

    void ImpactAudio()
    {
        Instantiate(impactSFX.gameObject, transform.position, Quaternion.identity);
    }

    void ImpactPlayerAudio()
    {
        Instantiate(playerImpactSFX.gameObject, transform.position, Quaternion.identity);
    }

    IEnumerator DetectEnemy()
    {
        yield return new WaitForSeconds(waitTimeDetectEnemy);

        enemyDetect = true;
    }
}
