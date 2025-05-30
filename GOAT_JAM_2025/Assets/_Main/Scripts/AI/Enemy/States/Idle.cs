using Scripts.AI.Base.Interfaces;
using UnityEngine;

namespace Scripts.AI.Enemy.States
{
    public class Idle : IState
    {
        /// <summary>
        /// Idle da duracak
        /// Eğer ReadyToAttack olarak bool dönerse generalValues'dan player'ı kontrol edecek
        /// Player hayattaysa ona doğru hareket edecek
        /// Trigger alanı olacak (Overlap Sphere)
        /// Eğer player bu alandaysa ona zarar verecek
        /// Hasar so'su olacak
        /// Stopping distance so'dan gelecek ve player'a bu kadar hasar verecek
        /// </summary>
        private EnemyController _enemyController;

        public Idle(EnemyController enemyController)
        {
            _enemyController = enemyController;
            _enemyController.EnemyAnimationHandler.PlayIdleClip();
        }

        public void OnEnter()
        {
            Debug.Log("Idle Statedeyim");
        }

        public void Tick()
        {
        }

        public void OnExit()
        {
        }
    }
}