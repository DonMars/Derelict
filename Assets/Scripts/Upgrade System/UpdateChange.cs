using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateChange : MonoBehaviour
{
    public bool displaysRandomUpgrade = false;
    [SerializeField] public ShopUpgrade[] randomUpgradePool;

    public ShopDisplay shopDisplayScript;
    public ShopUpgrade updateToChange;
    public TextMeshProUGUI buttonName;

    void Start()
    {
        if (displaysRandomUpgrade)
        {
            updateToChange = RandomUpgrade();
        }

        buttonName.text = updateToChange.name;
    }

    void Update()
    {
        
    }

    public void ChangeUpdate()
    {
        shopDisplayScript.displayedUpgrade = updateToChange;
        shopDisplayScript.Display();
    }

    ShopUpgrade RandomUpgrade()
    {
        int random = Random.Range(0, randomUpgradePool.Length);
        return randomUpgradePool[random];
    }
}
