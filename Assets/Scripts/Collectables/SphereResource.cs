using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereResource : MonoBehaviour
{
    public float healthRecoverValue = 10;
    public bool isRecoverValueRandom = false;
    public float minRecoverValue = 5;
    public float maxRecoverValue = 10;
    float randomRecoverValue;
    public bool isSphereSpecial = false;
    public float specialHealthRecoverValue = 35;
    public bool isContaminated = false;
    public SphereCollider sphereCollider;

    FirstPersonController player;
    VFXOverlayHandler overlayHandler;

    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        overlayHandler = FindObjectOfType<VFXOverlayHandler>();

        StartCoroutine(EnableCollider());
    }

    void Update()
    {
        
    }

    public void CollectSphere()
    {
        // Health Recover Value
        if (isRecoverValueRandom)
        {
            healthRecoverValue = Random.Range(minRecoverValue, maxRecoverValue);
            healthRecoverValue = randomRecoverValue;
        }
        
        if (isSphereSpecial)
            healthRecoverValue = specialHealthRecoverValue;

        // Value Apply
        if (player.currentHealth < player.maxHealth && !isContaminated)
        {
            player.currentHealth += healthRecoverValue;
        }
        else if (player.currentHealth < player.maxHealth && isContaminated)
            FirstPersonController.OnTakeDamage(healthRecoverValue);

        // Max Health Check
        if (player.currentHealth >= player.maxHealth)
        {
            player.currentHealth = player.maxHealth;
        }

        AudioManager.Instance.Play("sphereCollect");
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.2f);
        sphereCollider.enabled = true;
    }
}
