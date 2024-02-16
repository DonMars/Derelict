using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeResource : MonoBehaviour
{
    [SerializeField] int CubeValue = 1;
    [SerializeField] bool isCubeValueRandom;
    [SerializeField] int minRandomValue;
    [SerializeField] int maxRandomValue;
    [SerializeField] BoxCollider cubeCollider;
    GameObject cubeHUDIcon;
    Animator cubeHUDIconAnimator;

    private void Awake()
    {
        cubeHUDIcon = GameObject.Find("CubeImage");
        cubeHUDIconAnimator = cubeHUDIcon.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(EnableCollider());
    }

    public void Collect()
    {
        if (isCubeValueRandom)
        {
            int randomValue = Random.Range(minRandomValue, maxRandomValue);
            CubeValue = randomValue;
        }

        GameManager.Instance.cubes += CubeValue;
        AudioManager.Instance.Play("CubeCollectSfx");
        cubeHUDIconAnimator.SetTrigger("cubeCollected");
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.2f);
        cubeCollider.enabled = true;
    }
}
