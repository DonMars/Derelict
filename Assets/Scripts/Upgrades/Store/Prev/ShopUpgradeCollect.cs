using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgradeCollect : MonoBehaviour
{
    public UpgradeEffect upgradeEffect;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            upgradeEffect.UpgradeApplyEffect(collision.gameObject);
        }
    }
}
