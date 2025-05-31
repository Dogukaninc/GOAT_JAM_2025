using UnityEngine;
using UnityEngine.UI;


public abstract class SkillEffect : ScriptableObject
{
    public string skillName{get; protected set;}
    public string skillDescription{get; protected set;}
    public Image skillIcon{get; protected set;}

    public abstract void ApplyEffect(float value);

}
