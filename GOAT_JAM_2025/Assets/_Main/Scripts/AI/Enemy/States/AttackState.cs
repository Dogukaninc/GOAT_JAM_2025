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
        private EnemyController _enemyController;

        public AttackState(EnemyController enemyController)
        {
            _attackCooldown = enemyController.EnemySo.attackCooldown;
            _damage = enemyController.EnemySo.damage;
            _enemyController = enemyController;
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

            Debug.Log("Attack Cooldown:" + _attackCooldown);
        }

        public void OnExit()
        {
            _attackCooldown = _attackDefaultTime;
        }

        private void Attack()
        {
            if (_player.TryGetComponent(out Health playerHealth))
            {
                playerHealth.TakeDamage(_damage);
                _enemyController.EnemyAnimationHandler.PlayAttackClip();
                Debug.Log("<color=red>Attack Made</color>");
            }
            else
            {
                Debug.LogWarning("Player health component does not attached");
            }
        }
    }
}