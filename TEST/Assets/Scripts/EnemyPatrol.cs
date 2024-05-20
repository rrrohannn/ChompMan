using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange;
    bool playerInSight;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        
        Patrol();
        if(playerInSight) Chase();
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Patrol()
    {
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);

        if (Vector3.Distance(transform.position, destPoint) < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchForDest()
    {
        float z = UnityEngine.Random.Range(-range, range);
        float x = UnityEngine.Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            walkPointSet = true;
        }
    }
}
