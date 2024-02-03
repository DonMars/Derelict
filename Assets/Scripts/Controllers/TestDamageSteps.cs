using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageSteps : MonoBehaviour
{
    [SerializeField] private float timeBetweenDamage = 0.25f;
    [SerializeField] private float damageToTake = 3;
    private bool isTouchingCollider = false;
    private Coroutine decreaseHealth;

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
        StopCoroutine(decreaseHealth);
    }

    private IEnumerator DecreaseHealth()
    {
        while (isTouchingCollider)
        {
            FirstPersonController.OnTakeDamage(damageToTake);
            yield return new WaitForSeconds(timeBetweenDamage);
        }
    }
}
