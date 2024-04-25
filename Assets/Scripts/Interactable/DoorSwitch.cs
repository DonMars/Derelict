using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorSwitch : Interactable
{
    public Door doorToOpen;
    public Animator switchAnimator;
    public AudioSource switchSFX;
    bool switchSwitch = true;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        if (switchSwitch)
        {
            switchSFX.Play();
            switchAnimator.SetBool("activated", true);
            doorToOpen.canOpen = true;
            switchSwitch = false;
        }
    }

    public override void OnLoseFocus()
    {

    }
}
