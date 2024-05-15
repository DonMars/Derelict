using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOverlayHandler : MonoBehaviour
{
    public GameObject weaponBarFrame;
    Animator weaponBarAnimator;

    public GameObject healthBarFrame;
    Animator healthBarAnimator;

    public GameObject staminaIcon;
    Animator staminaIconAnimator;

    public GameObject staminaFrame;
    Animator staminaFrameAnimator;

    public GameObject overlayGlow;
    Animator overlayGlowAnimator;

    public GameObject overlayTexture;
    Animator overlayTextureAnimator;

    public GameObject screenFade;
    Animator screenFadeAnimator;

    FirstPersonController player;
    float currentHealth;

    private void Awake()
    {
        weaponBarAnimator = weaponBarFrame.GetComponent<Animator>();
        healthBarAnimator = healthBarFrame.GetComponent<Animator>();
        staminaIconAnimator = staminaIcon.GetComponent<Animator>();
        staminaFrameAnimator = staminaFrame.GetComponent<Animator>();
        overlayGlowAnimator = overlayGlow.GetComponent<Animator>();
        overlayTextureAnimator = overlayTexture.GetComponent<Animator>();
        screenFadeAnimator = screenFade.GetComponent<Animator>();
        player = FindAnyObjectByType<FirstPersonController>();
    }

    void Start()
    {
        TriggerScreenFadeOut();
    }

    void Update()
    {
        currentHealth = player.currentHealth;
        
        if (currentHealth < (player.maxHealth / 5))
        {
            healthBarAnimator.SetBool("hbarLowHealth", true);
            healthBarAnimator.SetBool("hbarMidHealth", false);
            healthBarAnimator.SetBool("hbarIdle", false);

            overlayGlowAnimator.SetBool("health70", false);
            overlayGlowAnimator.SetBool("health45", false);
            overlayGlowAnimator.SetBool("health25", false);
            overlayGlowAnimator.SetBool("health15", true);
            overlayGlowAnimator.SetBool("healthIdle", false);

            overlayTextureAnimator.SetBool("texHealth45", false);
            overlayTextureAnimator.SetBool("texHealth25", false);
            overlayTextureAnimator.SetBool("texHealth15", true);
            overlayTextureAnimator.SetBool("texIdle", false);
        }
        else if (currentHealth < (player.maxHealth / 4))
        {
            healthBarAnimator.SetBool("hbarLowHealth", false);
            healthBarAnimator.SetBool("hbarMidHealth", true);
            healthBarAnimator.SetBool("hbarIdle", false);

            overlayGlowAnimator.SetBool("health70", false);
            overlayGlowAnimator.SetBool("health45", false);
            overlayGlowAnimator.SetBool("health25", true);
            overlayGlowAnimator.SetBool("health15", false);
            overlayGlowAnimator.SetBool("healthIdle", false);

            overlayTextureAnimator.SetBool("texHealth45", false);
            overlayTextureAnimator.SetBool("texHealth25", true);
            overlayTextureAnimator.SetBool("texHealth15", false);
            overlayTextureAnimator.SetBool("texIdle", false);
        }
        else if (currentHealth < (player.maxHealth / 3))
        {
            healthBarAnimator.SetBool("hbarLowHealth", false);
            healthBarAnimator.SetBool("hbarMidHealth", true);
            healthBarAnimator.SetBool("hbarIdle", false);

            overlayGlowAnimator.SetBool("health70", false);
            overlayGlowAnimator.SetBool("health45", true);
            overlayGlowAnimator.SetBool("health25", false);
            overlayGlowAnimator.SetBool("health15", false);
            overlayGlowAnimator.SetBool("healthIdle", false);

            overlayTextureAnimator.SetBool("texHealth45", true);
            overlayTextureAnimator.SetBool("texHealth25", false);
            overlayTextureAnimator.SetBool("texHealth15", false);
            overlayTextureAnimator.SetBool("texIdle", false);
        }
        else if (currentHealth < (player.maxHealth / 2))
        {
            healthBarAnimator.SetBool("hbarLowHealth", false);
            healthBarAnimator.SetBool("hbarMidHealth", false);
            healthBarAnimator.SetBool("hbarIdle", true);

            overlayGlowAnimator.SetBool("health70", true);
            overlayGlowAnimator.SetBool("health45", false);
            overlayGlowAnimator.SetBool("health25", false);
            overlayGlowAnimator.SetBool("health15", false);
            overlayGlowAnimator.SetBool("healthIdle", false);

            overlayTextureAnimator.SetBool("texHealth45", false);
            overlayTextureAnimator.SetBool("texHealth25", false);
            overlayTextureAnimator.SetBool("texHealth15", false);
            overlayTextureAnimator.SetBool("texIdle", true);
        }
        else if (currentHealth > (player.maxHealth / 2))
        {
            healthBarAnimator.SetBool("hbarLowHealth", false);
            healthBarAnimator.SetBool("hbarMidHealth", false);
            healthBarAnimator.SetBool("hbarIdle", true);

            overlayGlowAnimator.SetBool("health70", false);
            overlayGlowAnimator.SetBool("health45", false);
            overlayGlowAnimator.SetBool("health25", false);
            overlayGlowAnimator.SetBool("health15", false);
            overlayGlowAnimator.SetBool("healthIdle", true);

            overlayTextureAnimator.SetBool("texHealth45", false);
            overlayTextureAnimator.SetBool("texHealth25", false);
            overlayTextureAnimator.SetBool("texHealth15", false);
            overlayTextureAnimator.SetBool("texIdle", true);
        }
    }

    public void TriggerSmallDamageAnimations()
    {
        overlayGlowAnimator.ResetTrigger("smallDamage");
        overlayGlowAnimator.SetTrigger("smallDamage");

        overlayTextureAnimator.ResetTrigger("texDamage");
        overlayTextureAnimator.SetTrigger("texDamage");
    }

    public void TriggerMediumDamageAnimations()
    {
        overlayGlowAnimator.ResetTrigger("mediumDamage");
        overlayGlowAnimator.SetTrigger("mediumDamage");

        overlayTextureAnimator.ResetTrigger("texMediumDamage");
        overlayTextureAnimator.SetTrigger("texMediumDamage");
    }

    public void TriggerScreenFadeIn()
    {
        screenFadeAnimator.ResetTrigger("fadein");
        screenFadeAnimator.SetTrigger("fadein");
    }
    
    public void TriggerScreenFadeOut()
    {
        screenFadeAnimator.ResetTrigger("fadeout");
        screenFadeAnimator.SetTrigger("fadeout");
    }

    public void TriggerUseStaminaAnimations()
    {
        staminaIconAnimator.ResetTrigger("staminaUsed");
        staminaIconAnimator.SetTrigger("staminaUsed");

        staminaFrameAnimator.ResetTrigger("staminaUse");
        staminaFrameAnimator.SetTrigger("staminaUse");
    }

    public void TriggerStaminaRechargeAnimation()
    {
        staminaFrameAnimator.ResetTrigger("staminaRecharge");
        staminaFrameAnimator.SetTrigger("staminaRecharge");
    }

    public void TriggerStaminaDepletedAnimation()
    {
        staminaFrameAnimator.SetBool("isDepleted", true);
    }
    
    public void TriggerStaminaIdleAnimation()
    {
        staminaFrameAnimator.SetBool("isDepleted", false);
    }

    public void TriggerOverlayHealthRegen()
    {
        overlayGlowAnimator.ResetTrigger("healthRegen");
        overlayGlowAnimator.SetTrigger("healthRegen");
    }

    public void TriggerWeaponUse()
    {
        weaponBarAnimator.ResetTrigger("weaponUse");
        weaponBarAnimator.SetTrigger("weaponUse");
    }

    public void TriggerWeaponOvercharge()
    {
        weaponBarAnimator.SetBool("isOvercharged", true);
        weaponBarAnimator.SetBool("isRecharging", false);
    }

    public void TriggerWeaponRecharge()
    {
        weaponBarAnimator.SetBool("isOvercharged", false);
        weaponBarAnimator.SetBool("isRecharging", true);
    }

    public void TriggerWeaponIdle()
    {
        weaponBarAnimator.SetBool("isOvercharged", false);
        weaponBarAnimator.SetBool("isRecharging", false);
    }
}
