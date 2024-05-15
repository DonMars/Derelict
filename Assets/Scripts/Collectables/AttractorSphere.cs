using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSphere : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float collectDistance = 1;
    [SerializeField] bool playerDetected;
    [SerializeField] float attractorSpeed = 0.05f;

    VFXOverlayHandler overlayHandler;

    Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        overlayHandler = FindAnyObjectByType<VFXOverlayHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            playerDetected = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerDetected)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, attractorSpeed);

            if (Vector3.Distance(transform.position, target.transform.position) < collectDistance)
            {
                BroadcastMessage("CollectSphere");
                overlayHandler.TriggerOverlayHealthRegen();
                Destroy(gameObject);
            }
        }
    }
}
