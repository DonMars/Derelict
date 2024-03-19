    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Upgrades/Shop Upgrade")]

public class ShopUpgrade : UpgradeEffect
{
    public UpgradeEnum type;

    public new string name;
    public string description;
    public Sprite icon;
    public int cost;

    //Attack Type
    public int normalDamageIncreaseMin;
    public int normalDamageIncreaseMax;
    public int randomMinIncrease;
    public int randomMaxIncrease;
    public int fireRateIncrease;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void UpgradeApplyEffect(GameObject target)
    {
        #region Apply Attack Type
        // If Damage Update
        if (normalDamageIncreaseMax > 0 || randomMinIncrease > 0)
        {
            if (target.GetComponentInChildren<GunController>().dealsRandomDamage)
            {
                target.GetComponentInChildren<GunController>().randomDamageMin += randomMinIncrease;
                target.GetComponentInChildren<GunController>().randomDamageMax += randomMaxIncrease;
            }
            else
            {
                int increaseValue = Random.Range(normalDamageIncreaseMin, normalDamageIncreaseMax + 1);
                target.GetComponentInChildren<GunController>().normalDamage += increaseValue;
            }
        }

        // If Fire Rate Update
        if (fireRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().fireRate += fireRateIncrease;
        }
        #endregion
    }

    public override void UpgradeUnapplyEffect(GameObject target)
    {
        #region Unapply Attack Type
        // If Damage Update
        if (normalDamageIncreaseMax > 0 || randomMinIncrease > 0)
        {
            if (target.GetComponentInChildren<GunController>().dealsRandomDamage)
            {
                target.GetComponentInChildren<GunController>().randomDamageMin -= randomMinIncrease;
                target.GetComponentInChildren<GunController>().randomDamageMax -= randomMaxIncrease;
            }
            else
            {
                int increaseValue = Random.Range(normalDamageIncreaseMin, normalDamageIncreaseMax + 1);
                target.GetComponentInChildren<GunController>().normalDamage -= increaseValue;
            }
        }

        // If Fire Rate Update
        if (fireRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().fireRate -= fireRateIncrease;
        }
        #endregion
    }
}
