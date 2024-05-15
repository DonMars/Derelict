using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplayAfterLogos : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject logos;

    public void DisplayMenu()
    {
        mainMenu.SetActive(true);
        logos.SetActive(false);
    }
}
