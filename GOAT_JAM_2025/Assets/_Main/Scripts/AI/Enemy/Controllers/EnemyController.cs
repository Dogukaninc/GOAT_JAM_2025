using System;
using _Main.Scripts.Interface;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour,IDieable
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

    public bool IsDead { get; set; }
    public void OnDead()
    {
        throw new NotImplementedException();
    }

    public void OnRevive()
    {
        throw new NotImplementedException();
    }
}