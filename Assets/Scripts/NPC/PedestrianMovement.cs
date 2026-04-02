using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PedestrianMovement : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public Transform targetWaypoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (targetWaypoint != null)
        {
            agent.SetDestination(targetWaypoint.position);
        }
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

}