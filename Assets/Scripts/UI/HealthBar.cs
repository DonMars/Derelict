using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public FirstPersonController characterController;
    [SerializeField] float currentHealth;
    [SerializeField] float lerpTimer = 0.05f;
    [SerializeField] float lerpMultiplier = 0.2f;
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] float ultraLowHealth = 5f;
    [SerializeField] float lowHealth = 25f;
    [SerializeField] float midHealth = 45f;
    [SerializeField] Color32 frontBarColor;
    [SerializeField] Color32 frontBarUltraLowColor;
    [SerializeField] Color32 frontBarLowColor;
    [SerializeField] Color32 frontBarMidColor;
    [SerializeField] Color32 backBarColor;
    [SerializeField] Color32 backBarRefillColor;
    Color32 frontBarColorBak;
    public Image frontHealthBar;
    public Image backHealthBar;
    public float chipSpeed = 2f;
    float maxHealth;
    float delayTimer;

    Image healthColor;

    void Awake()
    {
        frontHealthBar.color = frontBarColor;
        frontBarColorBak = frontBarColor;
        currentHealth = characterController.maxHealth;
        maxHealth = characterController.maxHealth;
    }

    void Update()
    {
        maxHealth = characterController.maxHealth;
        currentHealth = characterController.currentHealth;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;

        if (currentHealth <= ultraLowHealth)
            frontHealthBar.color = frontBarUltraLowColor;
        else if (currentHealth <= lowHealth)
            frontHealthBar.color = frontBarLowColor;
        else if (currentHealth <= midHealth)
            frontHealthBar.color = frontBarMidColor;
        else
            frontHealthBar.color = frontBarColorBak;

        delayTimer += Time.deltaTime;

        if (delayTimer > delayTime)
        {
            float hFraction = currentHealth / maxHealth;

            if(fillB > hFraction)
            {
                frontHealthBar.fillAmount = hFraction;
                backHealthBar.color = backBarColor;
                lerpTimer += Time.deltaTime;
                backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, lerpMultiplier);
            }

            if(fillF < hFraction)
            {
                backHealthBar.color = backBarRefillColor;
                backHealthBar.fillAmount = hFraction;
                lerpTimer += Time.deltaTime;
                frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, lerpMultiplier);
            }
        }
    }
}
