using UnityEngine;

[CreateAssetMenu(fileName = "New Max Health Effect", menuName = "Skill Effects/Max Health Effect")]
public class MaxHealthEffect : SkillEffect
{
    public string skillName = "Increase Max Health";
    public string skillDescription = "You feel your body become more resilient to this filth";

    public override void ApplyEffect(float value)
    {
        GeneralValuesHolder.Instance.playerMaxHealth += value;
    }
}
