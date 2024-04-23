using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandomly : MonoBehaviour
{
    public int chanceUpTo;
    public int chanceSet;

    void Start()
    {
        int chance = Random.Range(1, chanceUpTo + 1);

        if (chance > chanceSet)
            Destroy(this.gameObject);
    }
}
