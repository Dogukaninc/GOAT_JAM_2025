using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent { get; set; }
    public Transform target;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
    }

    void Update()
    {
        NavMeshAgent.SetDestination(target.position);
    }
}