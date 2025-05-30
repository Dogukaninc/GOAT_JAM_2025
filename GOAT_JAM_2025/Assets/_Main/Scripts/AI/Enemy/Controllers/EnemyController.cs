using _Main.Scripts.AI.Enemy.Controllers;
using _Main.Scripts.Interface;
using _Main.Scripts.ScriptableClasses;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDieable
{
    [field: SerializeField] public EnemySo EnemySo { get; private set; }
    [field: SerializeField] public bool IsDead { get; set; }
    public float StoppingDistance { get; private set; }
    public EnemyAnimationHandler EnemyAnimationHandler { get; private set; }
    public NavMeshAgent NavMeshAgent { get; set; }
    public bool isReadyToMove; //TODO-> Bu flag sadece ai'ı başlangıçta koşullandırmak için kullanılan bir debug.

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
    }

    private void Start()
    {
        StoppingDistance = EnemySo.stoppingDistance;
        NavMeshAgent.stoppingDistance = StoppingDistance;
    }

    public void OnDead()
    {
    }

    public void OnRevive()
    {
    }
}