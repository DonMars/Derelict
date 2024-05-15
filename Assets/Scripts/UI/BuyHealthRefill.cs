using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyHealthRefill : MonoBehaviour
{
    FirstPersonController player;

    void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();
    }

    public void BuyHealth()
    {

        if (GameManager.Instance.cubes >= 350)
        {
            GameManager.Instance.cubes -= 350;
            player.currentHealth = player.maxHealth;

            this.GetComponent<Button>().interactable = false;
        }
    }
}
