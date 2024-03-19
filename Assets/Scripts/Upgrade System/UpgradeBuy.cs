using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuy : MonoBehaviour
{
    public ShopDisplay displayedUpdate;
    int currentBalance;
    Button buyButton;

    void Start()
    {
        buyButton = GetComponent<Button>();
    }

    void Update()
    {
        currentBalance = FindAnyObjectByType<GameManager>().cubes;

        if (currentBalance < displayedUpdate.displayedCost)
            buyButton.interactable = false;
        else
            buyButton.interactable = true;

    }

    public void BuyUpdate()
    {
        if (currentBalance >= displayedUpdate.displayedCost)
        {
            GameManager.Instance.cubes -= displayedUpdate.displayedCost;
            // Apply Upgrade
        }
    }   
}
