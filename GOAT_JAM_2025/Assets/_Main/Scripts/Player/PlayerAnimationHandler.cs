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

        private void Start()
        {
            PlayIdleClip();
        }

        public void PlayIdleClip()
        {
            // animator.CrossFade(_idleClipName, 0.1f);
            animator.SetBool("isRunning", false);
        }

        public void PlayRunClip()
        {
            // animator.CrossFade(_runClipName, 0.1f);
            animator.SetBool("isRunning", true);
        }

        public void PlayGetHitClip()
        {
            animator.CrossFade(_getHitClipName, 0.1f);
        }

        public void PlayAttackClip()
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger(_attackClipName);
        }
    }
}