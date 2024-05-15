using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusScreen : MonoBehaviour
{
    public TextMeshProUGUI weaponStats1;
    public TextMeshProUGUI weaponStats2;
    public TextMeshProUGUI defenseStats1;
    public TextMeshProUGUI defenseStats2;
    public TextMeshProUGUI movementStats1;
    public TextMeshProUGUI movementStats2;

    FirstPersonController player;
    GunController weapon;

    void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();
        weapon = FindAnyObjectByType<GunController>();
    }

    void Update()
    {
        if (!weapon.hasCriticalChance)
        {
            weaponStats1.text = "Current Damage: " + weapon.randomDamageMin + "-" + weapon.randomDamageMax + "\n" +
                        "Fire Rate: " + weapon.fireRate + "\n" +
                        "Critical Chance: " + "0" + "%" + "\n" +
                        "Critical Damage: -null-";
        }
        else if (weapon.hasCriticalChance)
        {
            weaponStats1.text = "Current Damage: " + weapon.randomDamageMin + "-" + weapon.randomDamageMax + "\n" +
                "Fire Rate: " + weapon.fireRate + "\n" +
                "Critical Chance: " + weapon.criticalChance + "%" + "\n" +
                "Critical Damage: +" + weapon.criticalDamageMin + "-" + weapon.criticalDamageMax;
        }


        weaponStats2.text = "Max Weapon Energy: " + weapon.weaponEnergyMax + "\n" +
                    "Energy Use p/ Shot: " + weapon.energyUse + "\n" +
                    "Recharge Rate: " + weapon.energyRechargeRate + "\n" +
                    "Overcharge Delay: " + weapon.overchargeDelay + " Sec.";

        // defenseStats1

        defenseStats2.text = "Max Health: " + player.maxHealth + "\n" +
                    "Armor Level: " + "0" + "\n" +
                    "Damage Reduction: " + "0" + "%" + "\n" +
                    "Shield: " + "-null-" + "\n" +
                    "Shield Capacity: " + "0";

        movementStats1.text = "Stamina Use Value: " + player.staminaUseMultiplier + "\n" +
                    "Stamina Regen Value: " + player.staminaValueIncrement + "\n" +
                    "Stamina Regen Delay: " + player.timeBeforeStaminaRegenStarts + " Sec." + "\n" +
                    "Depleted Stamina Delay: " + player.depletedStaminaRegenTime + " Sec.";

        movementStats2.text = "Walking Speed: " + player.walkSpeed + "\n" +
                    "Sprinting Speed: " + player.sprintSpeed + "\n" +
                    "Crouching Speed: " + player.crouchSpeed + "\n" +
                    "Max Stamina: " + player.maxStamina;
    }
}
