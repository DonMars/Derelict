using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using static UnityEngine.Rendering.DebugUI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] float ultraLowEnergy = 10f;
    [SerializeField] float lowEnergy = 25f;
    [SerializeField] float midEnergy = 45f;
    [SerializeField] Color32 barColor;
    [SerializeField] Color32 barMidColor;
    [SerializeField] Color32 barLowColor;
    [SerializeField] Color32 barUltraLowColor;
    public Image energyBar;

    float currentEnergy;
    float maxEnergy;

    [SerializeField] float delayTime = 0.5f;
    [SerializeField] float lerpTimer = 0.05f;
    float delayTimer;

    private void Awake()
    {
        energyBar.color = barColor;
    }

    void Update()
    {
        currentEnergy = FindAnyObjectByType<GunController>().weaponEnergy;
        maxEnergy = FindAnyObjectByType<GunController>().weaponEnergyMax;

        float fill = energyBar.fillAmount;

        if (currentEnergy <= ultraLowEnergy)
            energyBar.color = barUltraLowColor;
        else if (currentEnergy <= lowEnergy)
            energyBar.color = barLowColor;
        else if (currentEnergy <= midEnergy)
            energyBar.color = barMidColor;
        else
            energyBar.color = barColor;

        delayTimer += Time.deltaTime;

        if (delayTimer > delayTime)
        {
            float eFraction = currentEnergy / maxEnergy;

            if (fill > eFraction)
            {
                energyBar.fillAmount = eFraction;
                lerpTimer += Time.deltaTime;
            }

            if (fill < eFraction)
            {
                lerpTimer += Time.deltaTime;
                energyBar.fillAmount = eFraction;
            }
        }
    }
}
