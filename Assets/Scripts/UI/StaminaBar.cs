using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] public FirstPersonController characterController;
    [SerializeField] float currentStamina;
    [SerializeField] float lerpTimer = 0.05f;
    [SerializeField] float lerpMultiplier = 0.2f;
    [SerializeField] Color32 frontBarColor;
    [SerializeField] Color32 backBarColor;
    [SerializeField] Color32 backBarRefillColor;
    public Image frontStaminaBar;
    public Image backStaminaBar;
    public float chipSpeed = 2f;
    private float maxStamina;

    Image staminaColor;

    void Awake()
    {
        frontStaminaBar.color = frontBarColor;
        currentStamina = characterController.maxStamina;
        maxStamina = characterController.maxStamina;
    }

    void Update()
    {
        currentStamina = characterController.currentStamina;

        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        float fillF = frontStaminaBar.fillAmount;
        float fillB = backStaminaBar.fillAmount;
        float sFraction = currentStamina / maxStamina;

        if (fillB > sFraction)
        {
            frontStaminaBar.fillAmount = sFraction;
            backStaminaBar.color = backBarColor;
            lerpTimer += Time.deltaTime;
            backStaminaBar.fillAmount = Mathf.Lerp(fillB, sFraction, lerpMultiplier);
        }

        if (fillF < sFraction)
        {
            backStaminaBar.color = backBarRefillColor;
            backStaminaBar.fillAmount = sFraction;
            lerpTimer += Time.deltaTime;
            frontStaminaBar.fillAmount = Mathf.Lerp(fillF, backStaminaBar.fillAmount, lerpMultiplier);
        }
    }
}
