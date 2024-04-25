using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageSteps : MonoBehaviour
{
    [SerializeField] float timeBetweenDamage = 0.25f;
    [SerializeField] float damageToTake = 3;
    bool isTouchingCollider = false;
    Coroutine decreaseHealth;

    public AudioSource playerHit1;
    public AudioSource playerHit2;
    public AudioSource playerHit3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isTouchingCollider = true;
        decreaseHealth = StartCoroutine(DecreaseHealth());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isTouchingCollider = false;
        StopAllCoroutines();
    }

    private IEnumerator DecreaseHealth()
    {
        while (isTouchingCollider)
        {
            FirstPersonController.OnTakeDamage(damageToTake);

            int hitSfxRandomizer = Random.Range(1, 4);

            if (hitSfxRandomizer == 1)
                playerHit1.Play();
            else if (hitSfxRandomizer == 2)
                playerHit2.Play();
            else if (hitSfxRandomizer == 3)
                playerHit3.Play();

            yield return new WaitForSeconds(timeBetweenDamage);
        }
    }
}
