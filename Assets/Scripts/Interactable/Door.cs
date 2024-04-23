using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [Header("Door Settings")]
    public bool canOpen = true;
    [SerializeField] int autoCloseDistance = 10;

    [Header("Animations")]
    [SerializeField] Animator anim1;
    [SerializeField] Animator anim2;

    [Header("Audio")]
    public AudioSource doorInteractSFX;
    public AudioSource doorOpenSFX;
    public AudioSource doorCloseSFX;

    bool isOpen = false;
    bool canBeInteractedWith = true;
    bool openSwitch = false;

    public override void OnFocus()
    {
        print("Door Switch : Press [E] to Interact");
    }

    public override void OnInteract()
    {
        doorInteractSFX.Play();

        if (AudioManager.Instance.musicStart1)
        {
            StartCoroutine(PlayMusic());
        }

        if (canBeInteractedWith && canOpen && !openSwitch)
        {
            isOpen = !isOpen;
            anim1.SetBool("isOpen", isOpen);
            anim2.SetBool("isOpen", isOpen);

            doorOpenSFX.Play();

            StartCoroutine(DoorSwitch());

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

                doorOpenSFX.Play();

                yield return new WaitForSeconds(.64f);

                doorCloseSFX.Play();
            }
        }
    }

    IEnumerator DoorSwitch()
    {
        openSwitch = true;
        yield return new WaitForSeconds(2);
        openSwitch = false;
    }

    IEnumerator PlayMusic()
    {
        AudioManager.Instance.musicStart1 = false;
        yield return new WaitForSeconds(2);
        AudioManager.Instance.Play("SpaceStationAmbience");
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
