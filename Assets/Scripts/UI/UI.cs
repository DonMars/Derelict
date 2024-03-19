using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;
    [SerializeField] private TextMeshProUGUI staminaText = default;
    [SerializeField] private TextMeshProUGUI energyText = default;
    float currentEnergy;
    float currentStamina;
    
    private void OnEnable()
    {
        FirstPersonController.OnDamage += UpdateHealth;
        FirstPersonController.OnHeal += UpdateHealth;
    }

    private void OnDisable()
    {
        FirstPersonController.OnDamage -= UpdateHealth;
        FirstPersonController.OnHeal -= UpdateHealth;
        FirstPersonController.OnStaminaChange -= UpdateStamina;
    }

    private void Update()
    {
        currentEnergy = FindObjectOfType<GunController>().weaponEnergy;
        energyText.text = currentEnergy.ToString("F0");
        
        currentStamina = FindObjectOfType<FirstPersonController>().currentStamina;
        staminaText.text = currentStamina.ToString("F0");
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
    }

    private void UpdateStamina(float currentStamina)
    {
        staminaText.text = currentStamina.ToString("00");
    }
}
