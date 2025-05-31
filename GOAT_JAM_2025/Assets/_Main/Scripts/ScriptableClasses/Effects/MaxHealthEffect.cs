using UnityEngine;

[CreateAssetMenu(fileName = "New Max Health Effect", menuName = "Skill Effects/Max Health Effect")]
public class MaxHealthEffect : SkillEffect
{

    public override void ApplyEffect(float value)
    {
        GeneralValuesHolder.Instance.playerMaxHealth += value;
    }
}
