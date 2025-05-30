using System;
using Scripts.AI.Base;
using Scripts.AI.Enemy.States;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.Enemy
{
    public class EnemyStateManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stateDebugText;

        private StateMachine _stateMachine;
        private EnemyController _enemyController;

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        private void Start()
        {
            SetAgentStates();
        }

        private void SetAgentStates()
        {
            _stateMachine = new StateMachine();
            var idleState = new Idle();
            var moveToTargetState = new MoveToTargetState(_enemyController);
            var attackState = new AttackState(_enemyController.EnemySo);

            Func<bool> isPlayerFar = () => !IsPlayerDead() && IsPlayerFarAway() && _enemyController.isReadyToMove;
            Func<bool> isNoTarget = () => IsPlayerDead() || !_enemyController.isReadyToMove;
            Func<bool> isReadyToAttack = () => !IsPlayerDead() && !IsPlayerFarAway() && _enemyController.isReadyToMove;

            _stateMachine.AddAnyTransition(moveToTargetState, isPlayerFar);
            _stateMachine.AddAnyTransition(idleState, isNoTarget);
            _stateMachine.AddAnyTransition(attackState, isReadyToAttack);

            _stateMachine.SetState(idleState);
        }

        private void Update()
        {
            _stateMachine.Tick();

            stateDebugText.SetText(_stateMachine.CurrentState.GetType().Name);
        }

        private bool IsPlayerDead() => GeneralValuesHolder.Instance.Player.IsDead;

        private bool IsPlayerFarAway() =>
            Vector3.Distance(_enemyController.transform.position, GeneralValuesHolder.Instance.Player.transform.position) >
            _enemyController.StoppingDistance;
    }
}