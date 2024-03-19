using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopDisplay : MonoBehaviour
{
    public ShopUpgrade displayedUpgrade;

    public string displayedName;
    public string displayedDescription;
    public Sprite displayedIcon;
    public int displayedCost;
    public string displayedType;

    public TextMeshProUGUI nameUI;
    public TextMeshProUGUI descriptionUI;
    public Image iconUI;
    public TextMeshProUGUI costUI;
    public TextMeshProUGUI typeUI;

    void Start()
    {
        displayedName = displayedUpgrade.name;
        displayedDescription = displayedUpgrade.description;
        displayedIcon = displayedUpgrade.icon;
        displayedCost = displayedUpgrade.cost;
        displayedType = displayedUpgrade.type.ToString();

        nameUI.text = displayedName;
        descriptionUI.text = displayedDescription;
        iconUI.sprite = displayedIcon;
        costUI.text = displayedCost.ToString();
        typeUI.text = displayedType;
    }

    void Update()
    {
        
    }

    public void Display()
    {
        displayedName = displayedUpgrade.name;
        displayedDescription = displayedUpgrade.description;
        displayedIcon = displayedUpgrade.icon;
        displayedCost = displayedUpgrade.cost;
        displayedType = displayedUpgrade.type.ToString();

        nameUI.text = displayedName;
        descriptionUI.text = displayedDescription;
        iconUI.sprite = displayedIcon;
        costUI.text = displayedCost.ToString();
        typeUI.text = displayedType;
    }
}
