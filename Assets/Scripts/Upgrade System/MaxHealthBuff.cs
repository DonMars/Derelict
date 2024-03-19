using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MaxHealthBuff")]

public class MaxHealthBuff : UpgradeEffect
{
    public float increaseAmount;
    public bool healthRefill = false;

    public override void UpgradeApplyEffect(GameObject target)
    {
        target.GetComponent<FirstPersonController>().maxHealth += increaseAmount;

        if (healthRefill)
        {
            target.GetComponent<FirstPersonController>().currentHealth += increaseAmount;
            FirstPersonController.OnTakeDamage(0);
            FirstPersonController.OnHeal(increaseAmount);
        }
    }

    public override void UpgradeUnapplyEffect(GameObject target)
    {
        target.GetComponent<FirstPersonController>().maxHealth -= increaseAmount;
    }
}
