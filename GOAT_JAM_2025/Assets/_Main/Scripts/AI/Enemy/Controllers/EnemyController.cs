using System;
using _Main.Scripts.Interface;
using _Main.Scripts.ScriptableClasses;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDieable
{
    [field: SerializeField] public EnemySo EnemySo { get; private set; }
    [field: SerializeField] public float StoppingDistance { get; private set; }
    [field: SerializeField] public bool IsDead { get; set; }
    public NavMeshAgent NavMeshAgent { get; set; }
    public Transform target;
    public bool isReadyToMove; //TODO-> Bu flag sadece ai'ı başlangıçta koşullandırmak için kullanılan bir debug.

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StoppingDistance = EnemySo.stoppingDistance;
    }

    public void OnDead()
    {
    }

    public void OnRevive()
    {
    }
}