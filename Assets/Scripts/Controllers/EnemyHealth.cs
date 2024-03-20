using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int health;
    [SerializeField] bool useRandomHealth;
    [SerializeField] int randomHealthMin;
    [SerializeField] int randomHealthMax;

    [Header("Loot")]
    [SerializeField] bool dropsLoot;
    [SerializeField] GameObject loot;
    [SerializeField] int lootQuantityMin;
    [SerializeField] int lootQuantityMax;
    [SerializeField] bool hasChance;
    
    private void Awake()
    {
        if (useRandomHealth)
        {
            health = Random.Range(randomHealthMin, randomHealthMax + 1);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            if (dropsLoot)
                LootDrop();

            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void LootDrop()
    {
        for (int i = 0; i < Random.Range(lootQuantityMin,lootQuantityMax); ++i)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
