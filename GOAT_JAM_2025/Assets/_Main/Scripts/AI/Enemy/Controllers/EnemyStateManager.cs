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

        public void Initialize()
        {
        }

        private void Start()
        {
            SetAgentStates();
        }

        private void SetAgentStates()
        {
            _stateMachine = new StateMachine();
            var idleState = new Idle();
            var moveToTargetState = new MoveToTargetState();

            // Func<bool> isPlayerAlive = () => IsPlayerAlive();

            // _stateMachine.AddAnyTransition(moveState, isPlayerAlive);

            _stateMachine.SetState(idleState);
        }

        private void Update()
        {
            _stateMachine.Tick();

            stateDebugText.SetText(_stateMachine.CurrentState.GetType().Name);
        }
        
        // private void IsPlayerAlive()=>GeneralValuesHolder.Instance.Player.
    }
}