using UnityEngine;

namespace _Main.Scripts.ScriptableClasses
{
    [CreateAssetMenu(fileName = "so_enemy_type", menuName = "Scriptabl Objects/so_enemy_type", order = 0)]
    public class EnemySo : ScriptableObject
    {
        public float stoppingDistance;
        public float damage;
        public float attackCooldown;
    }
}