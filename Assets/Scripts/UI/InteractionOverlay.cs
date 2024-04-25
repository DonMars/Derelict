using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOverlay : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(9);
        this.gameObject.SetActive(false);
    }
}
