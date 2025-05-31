using Scripts.Utilities;
using UnityEngine;

public class GeneralValuesHolder : SingletonMonoBehaviour<GeneralValuesHolder>
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public float playerMaxHealth = 100;
    [field: SerializeField] public float playerLightPower = 1;
    [field: SerializeField] public float playerDamage = 10;
    [field: SerializeField] public bool SkillSelected = false;
}