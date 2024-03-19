using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateChange : MonoBehaviour
{
    public ShopDisplay shopDisplayScript;
    public ShopUpgrade updateToChange;
    public TextMeshProUGUI buttonName;

    void Start()
    {
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
}
