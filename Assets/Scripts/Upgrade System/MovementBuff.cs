using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MovementBuff")]

public class MovementBuff : UpgradeEffect
{
    public float walkSpeedIncrease;
    public float sprintSpeedIncrease;
    public float crouchSpeedIncrease;

    public override void UpgradeApplyEffect(GameObject target)
    {
        target.GetComponent<FirstPersonController>().walkSpeed += walkSpeedIncrease;
        target.GetComponent<FirstPersonController>().sprintSpeed += sprintSpeedIncrease;
        target.GetComponent<FirstPersonController>().crouchSpeed += crouchSpeedIncrease;
    }

    public override void UpgradeUnapplyEffect(GameObject target)
    {
        
    }
}
