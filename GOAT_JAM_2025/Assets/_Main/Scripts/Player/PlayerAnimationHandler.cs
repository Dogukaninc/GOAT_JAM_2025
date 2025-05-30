using UnityEngine;

namespace _Main.Scripts.AI.Enemy.Controllers
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private int _idleClipName = Animator.StringToHash("Idle");
        private int _attackClipName = Animator.StringToHash("Attack");
        private int _runClipName = Animator.StringToHash("Run");
        private int _getHitClipName = Animator.StringToHash("GetHit");

        public Animator animator;
        
        private static readonly int VelocityZ = Animator.StringToHash("VelocityZ");
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");

        public void AnimateMovement(Vector3 movement)
        {
            Vector3 localMovement = transform.InverseTransformDirection(movement);

            float velocityZ = localMovement.z;
            float velocityX = localMovement.x;

            animator.SetFloat(VelocityZ, velocityZ, 0.1f, Time.deltaTime);
            animator.SetFloat(VelocityX, velocityX, 0.1f, Time.deltaTime);
        }
    
        public void PlayIdleClip()
        {
           
        }

        public void PlayRunClip()
        {
           
        }

        public void PlayGetHitClip()
        {
        }

        public void PlayAttackClip()
        {
            
        }
    }
}