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
    public bool purchased = false;

    #region Attack Type
    //Attack Damage
    public int normalDamageIncrease;
    public int randomDamageMinIncrease;
    public int randomDamageMaxIncrease;
    //Fire Rate
    public float fireRateIncrease;
    //Critical Damage
    public int criticalChanceIncrease;
    public int criticalDamageIncrease;
    public bool makeCriticalDamageMultiplicative;
    public bool makeCriticalDamageRandom;
    public int criticalDamageRandomMinIncrease;
    public int criticalDamageRandomMaxIncrease;
    // Weapon Energy
    public float weaponEnergyMaxIncrease;
    public float weaponEnergyUseDecrease;
    public float weaponEnergyRechargeRateIncrease;
    public float weaponEnergyOverchargedRechargeRateIncrease;
    public float weaponOverchargeDelayDecrease;
    #endregion

    #region Defense Type
    //Max Health
    public float maxHealthIncrease;
    //Health Regen
    public float timeBeforeRegenDecrease;
    public float regenIncrementValueIncrease;
    public float regenIncrementTimeDecrease;
    #endregion

    #region Movement Type
    //Movement Speed
    public float baseStepIncrease;
    public float walkSpeedIncrease;
    public float walkBobSpeedIncrease;
    public float sprintSpeedIncrease;
    public float sprintBobSpeedIncrease;
    public float crouchSpeedIncrease;
    public float crouchBobSpeedIncrease;
    public float allSpeedIncrease;
    public float allBobSpeedIncrease;
    //Stamina
    public float maxStaminaIncrease;
    public float staminaUseReduction;
    public float timeBeforeStaminaRegenReduction;
    public float timeBeforeDepletedStaminaRegenReduction;
    public float staminaRegenValueIncrease;
    #endregion

    #region Special Type
    //Health Regeneration Ability
    public bool enableHealthRegen = false;
    //Dash Ability
    public bool enableDashAbility = false;
    //Critical Ability
    public bool enableCriticalDamage = false;
    #endregion

    public override void UpgradeApplyEffect(GameObject target)
    {
        #region Apply Attack Type

        // If Normal Damage Update
        if (normalDamageIncrease < 0)
        {
            target.GetComponentInChildren<GunController>().normalDamage += normalDamageIncrease;
        }

        // If Random Damage Update
        if (randomDamageMinIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().randomDamageMin += randomDamageMinIncrease;
            target.GetComponentInChildren<GunController>().randomDamageMax += randomDamageMaxIncrease;
        }

        // If Fire Rate Update
        if (fireRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().fireRate += fireRateIncrease;
        }

        // If Critical Damage Update
        if (criticalChanceIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().criticalChance += (criticalChanceIncrease);
        }

        if (criticalDamageIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().criticalDamage += criticalDamageIncrease;
        }

        if (makeCriticalDamageMultiplicative)
        {
            target.GetComponentInChildren<GunController>().isCriticalDamageMultiplicative = true;
            target.GetComponentInChildren<GunController>().isCriticalDamageRandom = false;
        }

        if (makeCriticalDamageRandom)
        {
            target.GetComponentInChildren<GunController>().isCriticalDamageRandom = true;
            target.GetComponentInChildren<GunController>().isCriticalDamageMultiplicative = false;
        }

        if (criticalDamageRandomMinIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().criticalDamageMin += criticalDamageRandomMinIncrease;
            target.GetComponentInChildren<GunController>().criticalDamageMax += criticalDamageRandomMaxIncrease;
        }

        // If Weapon Energy Update
        if (weaponEnergyMaxIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().weaponEnergyMax += weaponEnergyMaxIncrease;
        }

        if (weaponEnergyUseDecrease > 0)
        {
            target.GetComponentInChildren<GunController>().energyUse -= weaponEnergyUseDecrease;
        }

        if (weaponEnergyRechargeRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().energyRechargeRate += weaponEnergyRechargeRateIncrease;
        }

        if (weaponEnergyOverchargedRechargeRateIncrease > 0)
        {
            target.GetComponentInChildren<GunController>().overchargedEnergyRechargeRate += weaponEnergyOverchargedRechargeRateIncrease;
        }

        if (weaponOverchargeDelayDecrease > 0)
        {
            target.GetComponentInChildren<GunController>().overchargeDelay -= weaponOverchargeDelayDecrease;
        }

        #endregion

        #region Apply Defense Type

        // If Max Health Update
        if (maxHealthIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().maxHealth += maxHealthIncrease;
        }

        // If Health Regen Update
        if (timeBeforeRegenDecrease > 0)
        {
            target.GetComponent<FirstPersonController>().timeBeforeRegen -= timeBeforeRegenDecrease;
        }

        if (regenIncrementValueIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().regenIncrementValue += regenIncrementValueIncrease;
        }

        if (regenIncrementTimeDecrease > 0)
        {
            target.GetComponent<FirstPersonController>().regenIncrementTime -= regenIncrementTimeDecrease;
        }

        #endregion

        #region Apply Movement Type

        // If Walk Speed Update (Percentage based)
        if (walkSpeedIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().walkSpeed += (target.GetComponent<FirstPersonController>().walkSpeed / 100) * walkSpeedIncrease;
            target.GetComponent<FirstPersonController>().walkBobSpeed += (target.GetComponent<FirstPersonController>().walkBobSpeed / 100) * walkBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().baseStepSpeed -= (target.GetComponent<FirstPersonController>().baseStepSpeed / 100) * baseStepIncrease;

        }

        // If Run Speed Update (Percentage based)
        if (sprintSpeedIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().sprintSpeed += (target.GetComponent<FirstPersonController>().sprintSpeed / 100) * sprintSpeedIncrease;
            target.GetComponent<FirstPersonController>().sprintBobSpeed += (target.GetComponent<FirstPersonController>().sprintBobSpeed / 100) * sprintBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().baseStepSpeed -= (target.GetComponent<FirstPersonController>().baseStepSpeed / 100) * baseStepIncrease;
        }

        // If Crouch Speed Update (Percentage based)
        if (crouchSpeedIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().crouchSpeed += (target.GetComponent<FirstPersonController>().crouchSpeed / 100) * crouchSpeedIncrease;
            target.GetComponent<FirstPersonController>().crouchBobSpeed += (target.GetComponent<FirstPersonController>().crouchBobSpeed / 100) * crouchBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().baseStepSpeed -= (target.GetComponent<FirstPersonController>().baseStepSpeed / 100) * baseStepIncrease;
        }

        // If All Speed Update (Percentage based)
        if (allBobSpeedIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().walkSpeed += (target.GetComponent<FirstPersonController>().walkSpeed / 100) * allSpeedIncrease;
            target.GetComponent<FirstPersonController>().walkBobSpeed += (target.GetComponent<FirstPersonController>().walkBobSpeed / 100) * walkBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().sprintSpeed += (target.GetComponent<FirstPersonController>().sprintSpeed / 100) * allSpeedIncrease;
            target.GetComponent<FirstPersonController>().sprintBobSpeed += (target.GetComponent<FirstPersonController>().sprintBobSpeed / 100) * sprintBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().crouchSpeed += (target.GetComponent<FirstPersonController>().crouchSpeed / 100) * allSpeedIncrease;
            target.GetComponent<FirstPersonController>().crouchBobSpeed += (target.GetComponent<FirstPersonController>().crouchBobSpeed / 100) * crouchBobSpeedIncrease;
            target.GetComponent<FirstPersonController>().baseStepSpeed -= (target.GetComponent<FirstPersonController>().baseStepSpeed / 100) * baseStepIncrease;
        }

        // If Stamina Update
        if (maxStaminaIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().maxStamina += maxStaminaIncrease;
        }
        if (staminaUseReduction > 0)
        {
            target.GetComponent<FirstPersonController>().staminaUseMultiplier -= (staminaUseReduction * (target.GetComponent<FirstPersonController>().staminaUseMultiplier / 100));
        }
        if (timeBeforeStaminaRegenReduction > 0)
        {
            target.GetComponent<FirstPersonController>().timeBeforeStaminaRegenStarts -= timeBeforeStaminaRegenReduction;
            target.GetComponent<FirstPersonController>().depletedStaminaRegenTime -= timeBeforeDepletedStaminaRegenReduction;
            target.GetComponent<FirstPersonController>().originalStaminaRegenTime = target.GetComponent<FirstPersonController>().timeBeforeStaminaRegenStarts;
        }
        if (staminaRegenValueIncrease > 0)
        {
            target.GetComponent<FirstPersonController>().staminaValueIncrement += staminaRegenValueIncrease;
        }

        #endregion

        #region Apply Special Type
        //Health Regeneration
        if (enableHealthRegen)
        {
            target.GetComponent<FirstPersonController>().canRegenerateHealth = true;
            target.GetComponent<FirstPersonController>().RegenerateHealth();
        }

        //Dash Ability
        if (enableDashAbility)
        {
            target.GetComponent<FirstPersonController>().canUseDash = true;
        }

        //Critical Ability
        if (enableCriticalDamage)
        {
            target.GetComponentInChildren<GunController>().hasCriticalChance = true;
        }
        #endregion
    }

    public override void UpgradeUnapplyEffect(GameObject target)
    {
        #region Unapply Special Type
        //Health Regeneration
        if (enableHealthRegen)
        {
            target.GetComponent<FirstPersonController>().canRegenerateHealth = false;
        }

        //Dash Ability
        if (enableDashAbility)
        {
            target.GetComponent<FirstPersonController>().canUseDash = false;
        }

        //Critical Ability
        if (enableCriticalDamage)
        {
            target.GetComponentInChildren<GunController>().hasCriticalChance = false;
        }
        #endregion
    }
}
