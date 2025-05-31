using UnityEngine;

[CreateAssetMenu(fileName = "New Light Power Effect", menuName = "Skill Effects/Light Power Effect")]
public class LightPowerEffect : SkillEffect
{

    public override void ApplyEffect(float value)
    {
        GeneralValuesHolder.Instance.playerLightPower += value;
    }   
}
