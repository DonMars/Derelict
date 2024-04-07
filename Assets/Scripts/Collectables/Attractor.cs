using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float collectDistance = 1;
    [SerializeField] bool playerDetected;
    [SerializeField] float attractorSpeed = 0.05f;

    Vector3 _velocity = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            playerDetected = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    //        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    //        playerDetected = false;
    //    }
    //}

    private void FixedUpdate()
    {
        if (playerDetected)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, attractorSpeed);

            if (Vector3.Distance(transform.position, target.transform.position) < collectDistance)
            {
                BroadcastMessage("Collect");
                Destroy(gameObject);
            }
        }
    }
}
