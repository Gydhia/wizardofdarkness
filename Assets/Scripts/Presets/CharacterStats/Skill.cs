using System.Collections;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public bool CanLaunch = true;
    public float CooldownTimer;

    public float Cooldown;
    public float CastTime;

    public ParticleSystem SpellParticles;

    public bool IsAoe;
    [ConditionalField("IsAoe")]
    public float AoeRadius;

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
