using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEnabler : MonoBehaviour
{
    public Animator crossHairDot;
    public Animator crosshairFrame;
    public Animator healthText;
    public Animator healthBar;
    public Animator staminaBar;
    public Animator healthBarFrame;
    public Animator healthIcon;
    public Animator staminaBarFrame;
    public Animator staminaIcon;
    public Animator weaponBar;
    public Animator cubeCounter;
    public Animator cubeIcon;

    public bool firstHUDEnable = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !firstHUDEnable)
        {
            firstHUDEnable = true;

            TriggerHUDDisplay();
        }
    }

    public void TriggerHUDDisplay()
    {
        if(GameManager.Instance.firstCube)
        {
            cubeCounter.ResetTrigger("cubeCounterHUD");
            cubeIcon.ResetTrigger("cubeCounterHUD");

            cubeCounter.SetTrigger("cubeCounterHUD");
            cubeIcon.SetTrigger("cubeCounterHUD");
        }

        crossHairDot.ResetTrigger("showHUD");
        crosshairFrame.ResetTrigger("showHUD");
        healthText.ResetTrigger("showHUD");
        healthBar.ResetTrigger("showHUD");
        staminaBar.ResetTrigger("showHUD");
        healthBarFrame.ResetTrigger("showHUD");
        healthIcon.ResetTrigger("showHUD");
        staminaBarFrame.ResetTrigger("showHUD");
        staminaIcon.ResetTrigger("showHUD");
        weaponBar.ResetTrigger("showHUD");

        crossHairDot.SetTrigger("showHUD");
        crosshairFrame.SetTrigger("showHUD");
        healthText.SetTrigger("showHUD");
        healthBar.SetTrigger("showHUD");
        staminaBar.SetTrigger("showHUD");
        healthBarFrame.SetTrigger("showHUD");
        healthIcon.SetTrigger("showHUD");
        staminaBarFrame.SetTrigger("showHUD");
        staminaIcon.SetTrigger("showHUD");
        weaponBar.SetTrigger("showHUD");
    }

    public void TriggerCubeDisplay()
    {
        cubeCounter.ResetTrigger("cubeCollected");

        cubeCounter.ResetTrigger("cubeCounterHUD");
        cubeIcon.ResetTrigger("cubeCounterHUD");

        cubeCounter.SetTrigger("cubeCounterHUD");
        cubeIcon.SetTrigger("cubeCounterHUD");
    }
}
