using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : Interactable
{
    public GameObject loot;
    public float lootReleaseCooldown;
    public AudioSource containerSFX;
    Animator containerAnimator;
    int lootQuantity;
    Vector3 offsetPosition = new Vector3(0, 0.7f, 0);


    void Start()
    {
        containerAnimator = GetComponent<Animator>();
    }

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        OpenContainer();
    }

    public override void OnLoseFocus()
    {

    }

    void OpenContainer()
    {
        // Roulette
        int roulette = Random.Range(1, 21);

        if (roulette > 18)
            lootQuantity = 25;
        else if (roulette > 15)
            lootQuantity = 20;
        else if (roulette > 8)
            lootQuantity = 15;
        else if (roulette > 5)
            lootQuantity = 10;
        else if (roulette > 0)
            lootQuantity = 5;

        // SFX & Animation
        containerAnimator.SetBool("isOpen", true);
        containerSFX.Play();

        // Loot Release
        StartCoroutine(LootInstantiate());
    }

    IEnumerator LootInstantiate()
    {
        yield return new WaitForSeconds(0.75f);

        for (int i = 0; i < lootQuantity; i++)
        {
            Instantiate(loot, transform.position + offsetPosition, Quaternion.identity);
            yield return new WaitForSeconds(lootReleaseCooldown);
        }
    }
}
