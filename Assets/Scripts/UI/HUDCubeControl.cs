using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCubeControl : MonoBehaviour
{
    public Animator cubeHUD;
    bool activationSwitch = false;

    void Update()
    {
        if (!activationSwitch && GameManager.Instance.firstCube)
        {
            cubeHUD.SetTrigger("cubeCounterHUD");
            activationSwitch = true;
        }
    }
}
