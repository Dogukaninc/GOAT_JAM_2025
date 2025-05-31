using UnityEngine;
using UnityEngine.UI;


public abstract class SkillEffect : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;

    public abstract void ApplyEffect(float value);

}
