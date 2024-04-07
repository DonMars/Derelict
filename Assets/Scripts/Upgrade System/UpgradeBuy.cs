using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuy : MonoBehaviour
{
    public ShopDisplay displayedUpdate;
    public GameObject soldText;
    Button buyButton;
    GameManager gameManager;
    int currentBalance;

    void Start()
    {
        buyButton = GetComponent<Button>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        currentBalance = gameManager.cubes;

        if (currentBalance < displayedUpdate.displayedCost || displayedUpdate.displayedUpgrade.purchased)
            buyButton.interactable = false;
        else
            buyButton.interactable = true;

        if (displayedUpdate.displayedUpgrade.purchased)
            soldText.SetActive(true);
        else
            soldText.SetActive(false);
    }

    public void BuyUpdate()
    {
        if (currentBalance >= displayedUpdate.displayedCost)
        {
            displayedUpdate.displayedUpgrade.purchased = true;
            GameManager.Instance.cubes -= displayedUpdate.displayedCost;
            ApplyUpgrade();
        }
    }

    public void ApplyUpgrade()
    {
        GameObject target = FindObjectOfType<FirstPersonController>().gameObject;
        displayedUpdate.displayedUpgrade.UpgradeApplyEffect(target);
    }
}
