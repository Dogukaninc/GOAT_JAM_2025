using _Main.Scripts.ScriptableClasses;
using Scripts.AI.Base.Interfaces;
using Scripts.GeneralSystems;
using UnityEngine;

namespace Scripts.AI.Enemy.States
{
    public class AttackState : IState
    {
        private Player _player;
        private float _attackCooldown;
        private float _attackDefaultTime;
        private float _damage;

        public AttackState(EnemySo enemySo)
        {
            _attackCooldown = enemySo.attackCooldown;
            _damage = enemySo.damage;
        }

        public void OnEnter()
        {
            Debug.Log("Attack State'e girdim");
            _player = GeneralValuesHolder.Instance.Player;
            _attackDefaultTime = _attackCooldown;
        }

        public void Tick()
        {
            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown <= 0)
            {
                _attackCooldown = _attackDefaultTime;
                Attack();
            }
        }

        public void OnExit()
        {
        }

        private void Attack()
        {
            if (_player.TryGetComponent(out Health playerHealth))
            {
                playerHealth.TakeDamage(_damage);
            }
            else
            {
                Debug.LogWarning("Player health component does not attached");
            }
        }
    }
}