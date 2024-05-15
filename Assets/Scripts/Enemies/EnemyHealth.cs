using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [HideInInspector] public int startingHealth;
    public int health;
    public bool useRandomHealth;
    public int randomHealthMin;
    public int randomHealthMax;
    EnemyLoot lootScript;

    [Header("Enemy Type")]
    public bool spyBot = false;
    public bool spyBot2 = false;
    public bool turretTrap = false;

    EnemyBehavior spybotBehaviorScript;
    TurretBehavior turretBehaviorScript;

    private void Start()
    {
        lootScript = GetComponent<EnemyLoot>();

        if(transform.parent != null)
            spybotBehaviorScript = GetComponentInParent<EnemyBehavior>();
        else
            spybotBehaviorScript = GetComponent<EnemyBehavior>();

        if (transform.parent != null)
            turretBehaviorScript = GetComponentInParent<TurretBehavior>();
        else
            turretBehaviorScript = GetComponent<TurretBehavior>();
    }

    private void Awake()
    {
        if (useRandomHealth)
        {
            health = Random.Range(randomHealthMin, randomHealthMax + 1);
        }

        startingHealth = health;
    }

    private void Update()
    {
        KillCheck();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    void KillCheck()
    {
        if (health <= 0f)
        {
            if (lootScript.dropsLoot)
            {
                lootScript.LootDrop();
            }

            if (spyBot || spyBot2)
            {
                spybotBehaviorScript.InstantiateExplosion();
            }

            if (turretTrap)
            {
                turretBehaviorScript.InstantiateExplosion2();
            }

            Destroy(gameObject);
        }
    }
}
