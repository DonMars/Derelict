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
    public float timeBeforeDestroy;
    EnemyLoot lootScript;

    private void Start()
    {
        lootScript = GetComponent<EnemyLoot>();
    }

    private void Awake()
    {
        if (useRandomHealth)
        {
            health = Random.Range(randomHealthMin, randomHealthMax + 1);
        }

        startingHealth = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            if (lootScript.dropsLoot)
            {
                lootScript.LootDrop();
            }

            Destroy(gameObject);
        }
    }
}
