using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cubesCounter = default;

    private void Update()
    {
        cubesCounter.text = GameManager.Instance.cubes.ToString();
    }
}