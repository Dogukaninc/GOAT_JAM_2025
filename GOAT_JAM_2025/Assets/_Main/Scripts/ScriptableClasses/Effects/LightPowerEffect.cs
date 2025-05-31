using UnityEngine;

[CreateAssetMenu(fileName = "New Light Power Effect", menuName = "Skill Effects/Light Power Effect")]
public class LightPowerEffect : SkillEffect
{
    public string skillName = "Increase Light Power";
    public string skillDescription = "You feel the support of everything you consider holy in your light";
    
    public override void ApplyEffect(float value)
    {
        GeneralValuesHolder.Instance.playerLightPower += value;
    }   
}
