using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(ShopUpgrade))]
public class UpdateEditor : Editor
{
    ShopUpgrade _shopUpgrade;

    private void OnEnable()
    {
        _shopUpgrade = (ShopUpgrade)target;
    }

    public override void OnInspectorGUI()
    {

        _shopUpgrade.name = EditorGUILayout.TextField("Name", _shopUpgrade.name);
        _shopUpgrade.description = EditorGUILayout.TextField("Description", _shopUpgrade.description);
        _shopUpgrade.icon = (Sprite) EditorGUILayout.ObjectField("Icon", _shopUpgrade.icon, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        _shopUpgrade.cost = EditorGUILayout.IntField("Cost", _shopUpgrade.cost);

        _shopUpgrade.type = (UpgradeEnum)EditorGUILayout.EnumPopup("Update Type", _shopUpgrade.type);

        Space(3);

        switch (_shopUpgrade.type)
        {
            case UpgradeEnum.AttackType:
                {
                    Space(3);

                    //Damage
                    _shopUpgrade.normalDamageIncrease = EditorGUILayout.IntField("Normal Damage Increase", _shopUpgrade.normalDamageIncrease);
                    _shopUpgrade.randomDamageMinIncrease = EditorGUILayout.IntField("Random Damage Min Increase", _shopUpgrade.randomDamageMinIncrease);
                    _shopUpgrade.randomDamageMaxIncrease = EditorGUILayout.IntField("Random Damage Max Increase", _shopUpgrade.randomDamageMaxIncrease);

                    Space(3);

                    //Fire Rate
                    _shopUpgrade.fireRateIncrease = EditorGUILayout.FloatField("Fire Rate Increase", _shopUpgrade.fireRateIncrease);
                    
                    Space(3);

                    //Critical Damage
                    _shopUpgrade.criticalChanceIncrease = EditorGUILayout.IntField("Critical Hit Chance Increase", _shopUpgrade.criticalChanceIncrease);
                    _shopUpgrade.criticalDamageIncrease = EditorGUILayout.IntField("Critical Damage Increase", _shopUpgrade.criticalDamageIncrease);
                    _shopUpgrade.makeCriticalDamageMultiplicative = EditorGUILayout.Toggle("Make Critical Damage Multiplicative", _shopUpgrade.makeCriticalDamageMultiplicative);
                    _shopUpgrade.makeCriticalDamageRandom = EditorGUILayout.Toggle("Make Critical Damage Random", _shopUpgrade.makeCriticalDamageRandom);
                    _shopUpgrade.criticalDamageRandomMinIncrease = EditorGUILayout.IntField("Random Critical Damage Min Increase", _shopUpgrade.criticalDamageRandomMinIncrease);
                    _shopUpgrade.criticalDamageRandomMaxIncrease = EditorGUILayout.IntField("Random Critical Damage Max Increase", _shopUpgrade.criticalDamageRandomMaxIncrease);

                    Space(3);

                    //Weapon Energy
                    _shopUpgrade.weaponEnergyMaxIncrease = EditorGUILayout.FloatField("Weapon Energy Max Increase", _shopUpgrade.weaponEnergyMaxIncrease);
                    _shopUpgrade.weaponEnergyUseDecrease = EditorGUILayout.FloatField("Weapon Energy Use Decrease", _shopUpgrade.weaponEnergyUseDecrease);
                    _shopUpgrade.weaponEnergyRechargeRateIncrease = EditorGUILayout.FloatField("Weapon Energy Recharge Rate Increase", _shopUpgrade.weaponEnergyRechargeRateIncrease);
                    _shopUpgrade.weaponEnergyOverchargedRechargeRateIncrease = EditorGUILayout.FloatField("Weapon Energy Overcharged Recharge Rate Increase", _shopUpgrade.weaponEnergyOverchargedRechargeRateIncrease);
                    _shopUpgrade.weaponOverchargeDelayDecrease = EditorGUILayout.FloatField("Weapon Overcharge Delay Decrease", _shopUpgrade.weaponOverchargeDelayDecrease);

                    break;
                }

            case UpgradeEnum.DefenseType:
                {
                    Space(3);

                    //Max Health
                    _shopUpgrade.maxHealthIncrease = EditorGUILayout.FloatField("Max Health Increase", _shopUpgrade.maxHealthIncrease);

                    Space(3);

                    //Health Regen
                    _shopUpgrade.timeBeforeRegenDecrease = EditorGUILayout.FloatField("Time Before Health Regen Decrease", _shopUpgrade.timeBeforeRegenDecrease);
                    _shopUpgrade.regenIncrementValueIncrease = EditorGUILayout.FloatField("Health Regen Increment Value Increase", _shopUpgrade.regenIncrementValueIncrease);
                    _shopUpgrade.regenIncrementTimeDecrease = EditorGUILayout.FloatField("Health Regen Increment Time Decrease", _shopUpgrade.timeBeforeRegenDecrease);
                    break;
                }

            case UpgradeEnum.MovementType:
                {
                    Space(3);

                    //Movement
                    _shopUpgrade.baseStepIncrease = EditorGUILayout.FloatField("Base Footstep Increase", _shopUpgrade.baseStepIncrease);
                    _shopUpgrade.walkSpeedIncrease = EditorGUILayout.FloatField("Walk Speed Increase", _shopUpgrade.walkSpeedIncrease);
                    _shopUpgrade.walkBobSpeedIncrease = EditorGUILayout.FloatField("Walk Headbob Increase", _shopUpgrade.walkBobSpeedIncrease);
                    _shopUpgrade.sprintSpeedIncrease = EditorGUILayout.FloatField("Sprint Speed Increase", _shopUpgrade.sprintSpeedIncrease);
                    _shopUpgrade.sprintBobSpeedIncrease = EditorGUILayout.FloatField("Sprint Headbob Increase", _shopUpgrade.sprintBobSpeedIncrease);
                    _shopUpgrade.crouchSpeedIncrease = EditorGUILayout.FloatField("Crouch Speed Increase", _shopUpgrade.crouchSpeedIncrease);
                    _shopUpgrade.crouchBobSpeedIncrease = EditorGUILayout.FloatField("Crouch Headbob Increase", _shopUpgrade.crouchBobSpeedIncrease);

                    Space(3);

                    _shopUpgrade.allSpeedIncrease = EditorGUILayout.FloatField("General Speed Increase", _shopUpgrade.allSpeedIncrease);
                    _shopUpgrade.allBobSpeedIncrease = EditorGUILayout.FloatField("General Headbob Increase", _shopUpgrade.allBobSpeedIncrease);

                    Space(3);

                    //Stamina
                    _shopUpgrade.maxStaminaIncrease = EditorGUILayout.FloatField("Max Stamina Increase", _shopUpgrade.maxStaminaIncrease);

                    Space(3);

                    _shopUpgrade.staminaUseReduction = EditorGUILayout.FloatField("Stamina Use Decrease", _shopUpgrade.staminaUseReduction);
                    _shopUpgrade.timeBeforeStaminaRegenReduction = EditorGUILayout.FloatField("Time Before Stamina Regen Decrease", _shopUpgrade.timeBeforeStaminaRegenReduction);
                    _shopUpgrade.timeBeforeDepletedStaminaRegenReduction = EditorGUILayout.FloatField("Time Before Depleted Stamina Regen Decrease", _shopUpgrade.timeBeforeDepletedStaminaRegenReduction);
                    _shopUpgrade.staminaRegenValueIncrease = EditorGUILayout.FloatField("Stamina Regen Value Increase", _shopUpgrade.staminaRegenValueIncrease);
                    break;
                }

            case UpgradeEnum.SpecialType:
                {
                    Space(3);

                    //Health Regeneration
                    _shopUpgrade.enableHealthRegen = EditorGUILayout.Toggle("Enable Health Regeneration", _shopUpgrade.enableHealthRegen);

                    Space(3);

                    //Dash Ability
                    _shopUpgrade.enableDashAbility = EditorGUILayout.Toggle("Enable Dash Ability", _shopUpgrade.enableDashAbility);

                    Space(3);

                    //Critical Ability
                    _shopUpgrade.enableCriticalDamage = EditorGUILayout.Toggle("Enable Critical Damage", _shopUpgrade.enableCriticalDamage);

                    break;
                }
        }

        EditorUtility.SetDirty(target);
    }

    private void Space(int space)
    {
        for (int i = 0; i < space; i++)
        {
            EditorGUILayout.Space();
        }
    }
}

#endif