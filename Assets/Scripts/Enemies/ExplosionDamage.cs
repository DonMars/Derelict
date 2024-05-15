using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damageMin;
    public int damageMax;
    public bool damagesEnemies;
    bool damageEnemySwitch = false;
    bool damagePlayerSwitch = false;
    EnemyHealth enemyHealthScript;
    int damage;

    private void Awake()
    {
        damage = Random.Range(damageMin, damageMax);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !damagePlayerSwitch)
        {
            FirstPersonController.OnTakeDamage(damage);
            damagePlayerSwitch = true;
        }

        if (damagesEnemies && other.CompareTag("Enemy") && !other.GetComponent<Collider>().isTrigger && !damageEnemySwitch)
        {
            enemyHealthScript = other.GetComponent<EnemyHealth>();
            enemyHealthScript.TakeDamage(damage);
            damageEnemySwitch = true;
        }

        Destroy(this.gameObject);
    }
} 
