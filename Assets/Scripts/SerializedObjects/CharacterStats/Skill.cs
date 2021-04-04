using UnityEngine;


public abstract class Skill : MonoBehaviour
{
    public enum ESkillType
    {
        Movement = 0,
        Defense = 1,
        Damage = 2
    }
    public enum ESkillRangeType
    {
        Melee = 0,
        Ranged = 1,
    }
    [UnityEngine.Header("Damages")]
    [UnityEngine.Tooltip("Skill damage based on THIS % of base strength.")]public float dmgPercent = 100;
    [UnityEngine.Tooltip("Skill cooldown.")] public float skillCD;
    [UnityEngine.Tooltip("The skill's areaOfEffect radius.")] public float AOERadius;
    [UnityEngine.Tooltip("Duration the skill has to be casted in order to be released.")] public float castTime;
    public ESkillType state;
    [UnityEngine.Header("Movement (not sure)")]
    [UnityEngine.Tooltip("Please ignore, this is just a placeholder for the header. Just in case.")] public float speedBoost;

    [UnityEngine.Header("Defense (not sure)")]
    [UnityEngine.Tooltip("Please ignore, this is just a placeholder for the header. Just in case.")] public float defBoost;

    public abstract void ActivatedSkill();
}
