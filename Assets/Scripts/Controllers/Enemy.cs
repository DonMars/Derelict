using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] float detectionRadius = 5;
    [SerializeField] LayerMask playerLayer;
    private bool aware;

    [Header("Collision Damage")]
    [SerializeField] bool dealsOnCollisionDamage = false;
    [SerializeField] int collisionDamageRate = 6;
    [SerializeField] int collisionDamageMin = 1;
    [SerializeField] int collisionDamageMax = 2;
    int collisionDamage;

    NavMeshAgent agent;
    Transform player;
    Vector3 target;
    Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        if (Physics.CheckSphere(transform.position, detectionRadius, playerLayer))
            aware = true;

        if (aware)
        {
            target = player.position;
            agent.SetDestination(target);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && dealsOnCollisionDamage)
        {
            int chance = Random.Range(1, collisionDamageRate);

            if (chance == 1)
            {
                collisionDamage = Random.Range(collisionDamageMin, collisionDamageMax+1);
                FirstPersonController.OnTakeDamage(collisionDamage);
            }
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
    }
}
