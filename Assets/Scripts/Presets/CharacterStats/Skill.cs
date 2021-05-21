using System.Collections;
using UnityEngine;

public enum SkillBind
{
    None,

    MainAttack,
    Secondary,

    FirstSpell,
    SecondSpell,
    ThirdSpell
}

public class Skill : MonoBehaviour
{
    public SkillBind SkillBind = SkillBind.None;

    public bool CanLaunch = true;
    public float CooldownTimer;

    public float Cooldown;

    public ParticleSystem SpellParticles;

    public bool IsAoe;
    [ConditionalField("IsAoe")]
    public float AoeRadius;

    public bool IsIncanted;
    [ConditionalField("IsIncanted")]
    public float CastTime;

    public bool IsCasted;

    public bool IsPiercing;

    private void Awake()
    {
        CooldownTimer = Cooldown;
        CanLaunch = true;
    }
    public virtual void ActivatedSkill()
    {
        if(Cooldown != 0f)
            StartCoroutine(OnCooldown());
    }

    public IEnumerator OnCooldown()
    {
        CanLaunch = false;
        CooldownTimer = Cooldown;
        while(CooldownTimer >= 0f)
        {
            CooldownTimer -= Time.deltaTime;
            yield return null;
        }
        CanLaunch = true;

        yield return null;
    }
    
}
