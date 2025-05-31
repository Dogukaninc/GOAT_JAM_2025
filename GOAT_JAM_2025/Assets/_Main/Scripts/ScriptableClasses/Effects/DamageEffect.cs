using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Skill Effects/Damage Effect")]
public class DamageEffect : SkillEffect
{

    public override void ApplyEffect(float amount)
    {
        GeneralValuesHolder.Instance.playerDamage += amount; 
    }
}
