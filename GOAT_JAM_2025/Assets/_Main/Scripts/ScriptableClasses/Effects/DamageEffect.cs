using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Skill Effects/Damage Effect")]
public class DamageEffect : SkillEffect
{
    public string skillName = "Increase Damage";
    public string skillDescription = "You feel an immense power increase in your crossbow";

    public override void ApplyEffect(float amount)
    {
        GeneralValuesHolder.Instance.playerDamage += amount; 
    }
}
