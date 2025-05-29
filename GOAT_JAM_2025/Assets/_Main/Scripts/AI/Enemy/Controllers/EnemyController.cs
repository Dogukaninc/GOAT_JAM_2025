using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent { get; set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NavMeshAgent.SetDestination(Input.mousePosition);
        }
        
    }
}
