using System.Collections;
using UnityEngine;


public class Skill : MonoBehaviour
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
    public bool canLaunch = true;

    [UnityEngine.Header("Common:")]
    [UnityEngine.Tooltip("Skill cooldown.")] public float coolDown;
    [UnityEngine.Header("To Implement:")]
    [UnityEngine.Tooltip("Skill damage based on THIS % of base strength.(not implemented)")]public float dmgPercent = 100;
    [UnityEngine.Tooltip("The skill's areaOfEffect radius.(not implemented)")] public GameObject AOERadius;
    [UnityEngine.Tooltip("Duration the skill has to be casted in order to be released.(not implemented)")] public float castTime;

    [Header("Specificities to this skill:")]
    [UnityEngine.Tooltip("Please ignore, this is just a placeholder for the header. Just in case.")] private float PH;
    private void Start()
    {
        canLaunch = true;
    }
    public virtual void ActivatedSkill()
    {

    }
}
