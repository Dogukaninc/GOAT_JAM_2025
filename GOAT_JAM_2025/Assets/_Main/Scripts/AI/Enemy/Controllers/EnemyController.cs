using _Main.Scripts.AI.Enemy.Controllers;
using _Main.Scripts.Interface;
using _Main.Scripts.ScriptableClasses;
using DG.Tweening;
using Main._Project.Scripts.Utilities.Pool;
using System.Collections;
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
        StartCoroutine(death());
    }

    private IEnumerator death()
    {
        EnemyAnimationHandler.PlayDeathClip();
        this.NavMeshAgent.enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        SpreadLightSeams();
    }
    
    public void OnRevive()
    {
    }

    private void SpreadLightSeams()
    {
        for (int i = 0; i < EnemySo.lightSeamCount; i++)
        {
            var seam = PoolSystem.Instance.SpawnGameObject("LightSeam");
            seam.transform.position = transform.position;
            seam.transform.DOJump(SelectRandomPos(), 2, 1, 0.5f).SetEase(Ease.OutQuad);
        }
    }

    private Vector3 SelectRandomPos()
    {
        var randomPos = new Vector3(Random.Range(-3, 3), transform.position.y, Random.Range(-3, 3));
        return transform.position + randomPos;
    }
}