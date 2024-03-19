using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShop : Interactable
{
    public bool shopUIActive;
    public GameObject shopUI;
    public GameObject HUD;
    bool shopOpen = false;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        FindAnyObjectByType<PauseMenu>().shopOpen = shopOpen;

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
        FindAnyObjectByType<FirstPersonController>().enabled = false;
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
        shopUIActive = false;
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }
}
