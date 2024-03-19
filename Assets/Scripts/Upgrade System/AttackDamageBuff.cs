using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/AttackDamageBuff")]

public class AttackDamageBuff : UpgradeEffect
{
    public new string name;
    public string description;
    public Sprite icon;
    public int cost;

    public UpdateType updateType = new UpdateType();

    public enum UpdateType
    {
        AttackType,
        DefenseType,
        MovementType
    }

    public int normalDamageIncreaseMin;
    public int normalDamageIncreaseMax;
    public int randomMinIncrease;
    public int randomMaxIncrease;
    public int fireRateIncrease;

    public override void UpgradeApplyEffect(GameObject target)
    {
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
                int increaseValue = Random.Range(normalDamageIncreaseMin, normalDamageIncreaseMax+1);
                target.GetComponentInChildren<GunController>().normalDamage += increaseValue;
            }
        }

        // If Fire Rate Update
        if (fireRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().fireRate += fireRateIncrease;
        }
    }

    public override void UpgradeUnapplyEffect(GameObject target)
    {
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
    }
}
