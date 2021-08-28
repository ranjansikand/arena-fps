using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour
{
    private Transform player;
    
    [SerializeField] private NavMeshAgent agent;

    //Attacking
    [SerializeField] private float attackRadius = 9f;
    [SerializeField] float timeBetweenAttacks, attackForce;
    bool alreadyAttacked;
    [SerializeField] GameObject projectile;

    [SerializeField] Transform fireFromPoint;
    [SerializeField] Vector3 firingAdjustment = new Vector3(0, -0.25f, 0);


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(player);
        
        if ((player.position - transform.position).magnitude > attackRadius)
        {
            ChasePlayer();
        }
        else 
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player.position +firingAdjustment);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, fireFromPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
