using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuy : MonoBehaviour
{
    public ShopDisplay displayedUpdate;
    int currentBalance;
    Button buyButton;
    public GameObject soldText;

    void Start()
    {
        buyButton = GetComponent<Button>();
    }

    void Update()
    {
        currentBalance = FindAnyObjectByType<GameManager>().cubes;

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
