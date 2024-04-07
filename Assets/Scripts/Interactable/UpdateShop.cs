using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShop : Interactable
{
    public bool shopUIActive;
    public GameObject shopUI;
    public GameObject HUD;
    bool shopOpen = false;
    PauseMenu pauseMenu;

    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        pauseMenu.shopOpen = shopOpen;

        if (shopUIActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }
    
    public override void OnFocus()
    {
        print("LOOKING AT " + gameObject.name);
    }

    public override void OnInteract()
    {
        OpenShop();
    }

    public override void OnLoseFocus()
    {
        print("STOPPED LOOKING AT " + gameObject.name);
    }

    public void OpenShop()
    {
        shopOpen = true;
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<FirstPersonController>().enabled = false;
        FindObjectOfType<GunController>().enabled = false;
        shopUIActive = true;
        shopUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopOpen = false;
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindAnyObjectByType<FirstPersonController>().enabled = true;
        FindObjectOfType<GunController>().enabled = true;
        shopUIActive = false;
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }
}
