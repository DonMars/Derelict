using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeEffect : ScriptableObject
{
    public abstract void UpgradeApplyEffect(GameObject target);

    public abstract void UpgradeUnapplyEffect(GameObject target);
}
