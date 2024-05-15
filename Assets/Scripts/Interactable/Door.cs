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
    public AudioSource cantOpenSFX;

    public GameObject openSign1;
    public GameObject openSign2;
    public GameObject closeSign1;
    public GameObject closeSign2;
    bool signSwitch = false;

    bool isOpen = false;
    bool canBeInteractedWith = true;
    bool openSwitch = false;

    public GameObject interactionOverlay;

    private void Update()
    {
        // Changes Open/Closed door sign
        if (!canOpen)
        {
            openSign1.SetActive(false);
            openSign2.SetActive(false);
            closeSign1.SetActive(true);
            closeSign2.SetActive(true);
        }
        else if (canOpen && !signSwitch)
        {
            signSwitch = true;
            openSign1.SetActive(true);
            openSign2.SetActive(true);
            closeSign1.SetActive(false);
            closeSign2.SetActive(false);
        }
    }

    public override void OnFocus()
    {
        // Displays 'E' to interact message
        if (!GameManager.Instance.firstDoor)
        {
            GameManager.Instance.firstDoor = true;
            interactionOverlay.SetActive(true);
        }
    }

    public override void OnInteract()
    {
        doorInteractSFX.Play();

        // Starts ambiance music
        if (AudioManager.Instance.musicStart1)
        {
            StartCoroutine(PlayMusic());
        }

        // Opens/Closes the door via animation
        if (canBeInteractedWith && canOpen && !openSwitch)
        {
            isOpen = !isOpen;
            anim1.SetBool("isOpen", isOpen);
            anim2.SetBool("isOpen", isOpen);

            doorOpenSFX.Play();

            StartCoroutine(DoorSwitch());

            StartCoroutine(AutoClose());
        }
        else if (!canOpen)
        {
            cantOpenSFX.Play();
        }
    }

    public override void OnLoseFocus()
    {
        
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
