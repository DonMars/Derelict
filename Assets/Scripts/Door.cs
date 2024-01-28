using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private int autoCloseDistance = 10;
    [SerializeField] private Animator anim1;
    [SerializeField] private Animator anim2;
    private bool isOpen = false;
    private bool canBeInteractedWith = true;

    public override void OnFocus()
    {
        print("Door Switch : Press [E] to Interact");
    }

    public override void OnInteract()
    {
        print("Open Sesame");

        if (canBeInteractedWith)
        {
            isOpen = !isOpen;
            anim1.SetBool("isOpen", isOpen);
            anim2.SetBool("isOpen", isOpen);

            StartCoroutine(AutoClose());
        }
    }

    public override void OnLoseFocus()
    {
        print("- - - - -");
    }

    private IEnumerator AutoClose()
    {
        while (isOpen)
        {
            yield return new WaitForSeconds(3);

            if (Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > autoCloseDistance)
            {
                isOpen = false;
                anim1.SetBool("isOpen", isOpen);
                anim2.SetBool("isOpen", isOpen);
            }
        }
    }

    private void Animator_LockInteraction()
    {
        canBeInteractedWith = false;
    }

    private void Animator_UnlockInteraction()
    {
        canBeInteractedWith = true;
    }
}
