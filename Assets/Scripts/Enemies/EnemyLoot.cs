using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Loot")]
    public bool dropsLoot;

    public bool hasChance;
    [Range(1, 100)] public int chanceSet;

    [SerializeField] GameObject lootToSpawn1;
    [SerializeField] GameObject lootToSpawn2;
    [SerializeField] GameObject lootToSpawn3;

    [Header("Scenario 1")]
    public int loot1QuantityMin1;
    public int loot2QuantityMin1;
    public int loot3QuantityMin1;

    public int loot1QuantityMax1;
    public int loot2QuantityMax1;
    public int loot3QuantityMax1;
    
    [Header("Scenario 2")]
    public int loot1QuantityMin2;
    public int loot2QuantityMin2;
    public int loot3QuantityMin2;

    public int loot1QuantityMax2;
    public int loot2QuantityMax2;
    public int loot3QuantityMax2;

    [Header("Scenario 3")]
    public int loot1QuantityMin3;
    public int loot2QuantityMin3;
    public int loot3QuantityMin3;

    public int loot1QuantityMax3;
    public int loot2QuantityMax3;
    public int loot3QuantityMax3;

    EnemyHealth enemyHealth;

    public void LootDrop()
    {
        LootScenarios();
    }

    void LootScenarios()
    {
        // Count Number of Collectables
        int numberOfCollectables = 1;

        if (lootToSpawn2 != null)
            numberOfCollectables++;

        if (lootToSpawn3 != null)
            numberOfCollectables++;

        // Count Number of Scenarios
        int numberOfScenarios = 1;

        if (loot1QuantityMin2 > 0)
            numberOfScenarios++;
        if (loot1QuantityMin3 > 0)
            numberOfScenarios++;

        // Count Loot Quantity
        int loot1Quantity = 0;
        int loot2Quantity = 0;
        int loot3Quantity = 0;

        // Scenario 1
        if (loot1QuantityMin1 > 0)
            loot1Quantity++;
        if (loot2QuantityMin1 > 0)
            loot1Quantity++;
        if (loot3QuantityMin1 > 0)
            loot1Quantity++;

        // Scenario 2
        if (loot1QuantityMin2 > 0)
            loot2Quantity++;
        if (loot2QuantityMin2 > 0)
            loot2Quantity++;
        if (loot3QuantityMin2 > 0)
            loot2Quantity++;

        // Scenario 3
        if (loot1QuantityMin3 > 0)
            loot3Quantity++;
        if (loot2QuantityMin3 > 0)
            loot3Quantity++;
        if (loot3QuantityMin3 > 0)
            loot3Quantity++;

        if (loot1QuantityMin2 > 0 && loot1QuantityMin3 > 0)
        {
            
        }

        int scenarioSelect = Random.Range(1, numberOfScenarios + 1);

        if (scenarioSelect == 1)
        {
            if (hasChance)
            {
                int chance = Random.Range(1, 101);

                if (chance < chanceSet)
                {

                    if (loot1QuantityMin1 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot1QuantityMin1, loot1QuantityMax1); ++i)
                        {
                            Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                        }
                    }


                    if (loot2QuantityMin1 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot2QuantityMin1, loot2QuantityMax1); ++i)
                        {
                            Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                        }
                    }

                    if (loot3QuantityMin1 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot3QuantityMin1, loot3QuantityMax1); ++i)
                        {
                            Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            else
            {
                if (loot1QuantityMin1 > 0)
                {
                    for (int i = 0; i < Random.Range(loot1QuantityMin1, loot1QuantityMax1); ++i)
                    {
                        Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                    }
                }

                if (loot2QuantityMin1 > 0)
                {
                    for (int i = 0; i < Random.Range(loot2QuantityMin1, loot2QuantityMax1); ++i)
                    {
                        Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                    }
                }

                if (loot3QuantityMin1 > 0)
                {
                    for (int i = 0; i < Random.Range(loot3QuantityMin1, loot3QuantityMax1); ++i)
                    {
                        Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                    }
                }
            }
        }

        if (scenarioSelect == 2)
        {
            if (hasChance)
            {
                int chance = Random.Range(1, 101);

                if (chance < chanceSet)
                {
                    for (int i = 0; i < Random.Range(loot1QuantityMin2, loot1QuantityMax2); ++i)
                    {
                        Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                    }

                    if (loot2QuantityMin2 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot2QuantityMin2, loot2QuantityMax2); ++i)
                        {
                            Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                        }
                    }

                    if (loot3QuantityMin2 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot3QuantityMin2, loot3QuantityMax2); ++i)
                        {
                            Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Random.Range(loot1QuantityMin2, loot1QuantityMax2); ++i)
                {
                    Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                }

                if (loot2QuantityMin2 > 0)
                {
                    for (int i = 0; i < Random.Range(loot2QuantityMin2, loot2QuantityMax2); ++i)
                    {
                        Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                    }
                }

                if (loot3QuantityMin2 > 0)
                {
                    for (int i = 0; i < Random.Range(loot3QuantityMin2, loot3QuantityMax2); ++i)
                    {
                        Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                    }
                }
            }
        }

        if (scenarioSelect == 3)
        {
            if (hasChance)
            {
                int chance = Random.Range(1, 101);

                if (chance < chanceSet)
                {
                    for (int i = 0; i < Random.Range(loot1QuantityMin3, loot1QuantityMax3); ++i)
                    {
                        Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                    }

                    if (loot2QuantityMin3 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot2QuantityMin3, loot2QuantityMax3); ++i)
                        {
                            Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                        }
                    }

                    if (loot3QuantityMin3 > 0)
                    {
                        for (int i = 0; i < Random.Range(loot3QuantityMin3, loot3QuantityMax3); ++i)
                        {
                            Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Random.Range(loot1QuantityMin3, loot1QuantityMax3); ++i)
                {
                    Instantiate(lootToSpawn1, transform.position, Quaternion.identity);
                }

                if (loot2QuantityMin3 > 0)
                {
                    for (int i = 0; i < Random.Range(loot2QuantityMin3, loot2QuantityMax3); ++i)
                    {
                        Instantiate(lootToSpawn2, transform.position, Quaternion.identity);
                    }
                }

                if (loot3QuantityMin3 > 0)
                {
                    for (int i = 0; i < Random.Range(loot3QuantityMin3, loot3QuantityMax3); ++i)
                    {
                        Instantiate(lootToSpawn3, transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}