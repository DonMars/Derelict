using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRadius = 5;
    public LayerMask playerLayer;
    bool aware;
    bool enemyCall = false;
    bool calledSwitch = false;
    Transform player;
    Vector3 target;

    [Header("Patrolling")]
    public float patrolRangeMin;
    public float patrolRangeMax;
    public int patrolWaitTimeMin;
    public int patrolWaitTimeMax;
    public Transform centrePoint;
    NavMeshAgent agent;
    float patrolRange;
    bool patrolSwitch = false;
    
    [Header("Collision Damage")]
    public bool dealsOnCollisionDamage = false;
    public int collisionDamageRate = 6;
    public int collisionDamageMin = 1;
    public int collisionDamageMax = 2;
    int collisionDamage;

    Rigidbody rb;
    EnemyHealth enemyHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Detect
        Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        if (Physics.CheckSphere(transform.position, detectionRadius, playerLayer))
            aware = true;

        if (aware)
        {
            enemyCall = true;
            target = player.position;
            agent.SetDestination(target);
            rb.velocity = Vector3.zero;
        }

        // Patrolling
        if (agent.remainingDistance <= agent.stoppingDistance && !aware && !patrolSwitch)
        {
            Vector3 point;
            patrolRange = Random.Range(patrolRangeMin, patrolRangeMax);

            if (RandomPoint(centrePoint.position, patrolRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
                StartCoroutine(PatrolWait());
                patrolSwitch = true;
            }
        }

        // Damage Detect
        if (enemyHealth.health < enemyHealth.startingHealth)
        {
            aware = true;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * patrolRange;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(Random.Range(patrolWaitTimeMin, patrolWaitTimeMax+1));
        patrolSwitch = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && enemyCall && !calledSwitch)
        {
            calledSwitch = true;
            other.GetComponent<EnemyBehavior>().aware = true;
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
