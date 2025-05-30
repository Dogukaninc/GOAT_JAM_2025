using Scripts.AI.Base.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.AI.Enemy.States
{
    public class MoveToTargetState : IState
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private EnemyController _enemyController;

        public MoveToTargetState(EnemyController enemyController)
        {
            _agent = enemyController.NavMeshAgent;
            _enemyController = enemyController;
        }

        public void OnEnter()
        {
            Debug.Log("Hareket Ediyorum");
            _agent.isStopped = false;
            _target = GeneralValuesHolder.Instance.Player.transform;
        }

        public void Tick()
        {
            if (_target != null)
            {
                if (!IsCloseEnough())
                {
                    _agent.SetDestination(_target.position);
                }
                else
                {
                    _agent.isStopped = true;
                }
            }
        }

        public void OnExit()
        {
        }

        private bool IsCloseEnough() =>
            Vector3.Distance(_agent.transform.position, _target.position) <= _enemyController.StoppingDistance;
    }
}