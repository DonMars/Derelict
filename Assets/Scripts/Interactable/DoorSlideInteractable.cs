using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSlideInteractable : Interactable
{
    public GameObject activateRoom;
    public Animator secretDoor;
    bool switchTrigger;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        if (!switchTrigger)
        {
            activateRoom.SetActive(true);
            secretDoor.SetTrigger("secretFound");
            switchTrigger = true;
        }
    }

    public override void OnLoseFocus()
    {

    }
}
