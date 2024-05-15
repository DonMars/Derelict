using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRandomTurnOff : MonoBehaviour
{
    [Header("Properties")]
    public Light lightToTurnOff;
    [Range(1, 100)] public int chance;

    [Header("References")]
    public Material materialToSwitchTo;
    Material materialToSwitch;
    MeshRenderer lightMeshRenderer;

    void Start()
    {
        int chanceRoll = Random.Range(1, 101);

        if (chanceRoll <= chance)
        {
            lightMeshRenderer = GetComponent<MeshRenderer>();
            materialToSwitch = lightMeshRenderer.material;
            lightMeshRenderer.material = materialToSwitchTo;

            lightToTurnOff.enabled = false;
        }
    }
}
