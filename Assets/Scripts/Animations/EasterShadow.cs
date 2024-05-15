using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterShadow : MonoBehaviour
{
    public int chanceToSelfdestroy = 50;
    public float minTimeToMove = 2f;
    public float maxTimeToMove = 4f;
    public float moveForce = 4f;

    void Start()
    {
        int chance = Random.Range(1, 101);

        if (chance > chanceToSelfdestroy)
        {
            Destroy(gameObject);
        }

        StartCoroutine(MoveAndDestroy());
    }

    IEnumerator MoveAndDestroy()
    {
        yield return new WaitForSeconds(Random.Range(minTimeToMove, maxTimeToMove));

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.right * moveForce, ForceMode.Impulse);

        yield return new WaitForSeconds(1.2f);

        Destroy(gameObject);
    }
}
