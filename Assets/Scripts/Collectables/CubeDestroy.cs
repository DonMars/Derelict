using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroy : MonoBehaviour
{
    [SerializeField] Animator cubeAnimator;
    [SerializeField] float destroyInSecondsMin;
    [SerializeField] float destroyInSecondsMax;
    [SerializeField] float destroyAnimationTime;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(Random.Range(destroyInSecondsMin, destroyInSecondsMax));
        cubeAnimator.SetTrigger("Destroy");
        yield return new WaitForSeconds(destroyAnimationTime);
        Destroy(gameObject);
    }
}
