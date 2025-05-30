using UnityEngine;

namespace _Main.Scripts.AI.Enemy.Controllers
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private int _holdLantern = Animator.StringToHash("HoldLantern");

        public Animator animator;
        
        private static readonly int VelocityZ = Animator.StringToHash("VelocityZ");
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");

        public void AnimateMovement(Vector3 movement)
        {
            Vector3 localMovement = transform.InverseTransformDirection(movement);

            float velocityX = localMovement.x;
            float velocityZ = localMovement.z;

            // Threshold altında ise sıfırla
            if (Mathf.Abs(velocityZ) < 0.05f) velocityZ = 0f;
            if (Mathf.Abs(velocityX) < 0.05f) velocityX = 0f;

            Vector2 planar = new Vector2(velocityX, velocityZ);
            if (planar.magnitude > 0f)
            {
                planar.Normalize(); // sabit hız
                velocityX = planar.x;
                velocityZ = planar.y;
            }

            animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        }


        public void HoldLanternAnim()
        {
            
        }
    }
}