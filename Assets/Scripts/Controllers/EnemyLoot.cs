using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Loot")]
    public bool dropsLoot;
    public int lootQuantityMin;
    public int lootQuantityMax;
    public bool hasChance;
    [Range(1, 100)] public int chancePercentage;
    [SerializeField] GameObject loot;
    bool lootSwitch = false;

    EnemyHealth enemyHealth;
    
    public void LootDrop()
    {
        if (hasChance)
        {
            int chance = Random.Range(1, 101);

            if (chance < chancePercentage)
            {
                lootSwitch = true;

                for (int i = 0; i < Random.Range(lootQuantityMin, lootQuantityMax); ++i)
                {
                    Instantiate(loot, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            lootSwitch = true;

            for (int i = 0; i < Random.Range(lootQuantityMin, lootQuantityMax); ++i)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }
}