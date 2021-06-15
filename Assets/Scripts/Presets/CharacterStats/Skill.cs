using System.Collections;
using System.Linq;
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
    public EElements SkillElement = EElements.None;
    
    public EntityStat EntityHolder;

    public bool IsBeingCast = false;
    [HideInInspector]
    public bool CanLaunch = true;
    [HideInInspector]
    public bool HasReleased = false;
    [HideInInspector]
    public float CooldownTimer;

    public float Cooldown;

    private void Awake()
    {
        this.EntityHolder = this.GetComponentInParent<EntityStat>();
        CooldownTimer = Cooldown;
        CanLaunch = true;
    }
    public virtual void ActivatedSkill()
    {
        if (!CanLaunch && 
            PlayerController.Instance.PlayerStats.Elements.Single(element => element.Type == this.SkillElement).IsActive) 
            return; 
    }

    protected void BeginCooldown()
    {
        if (Cooldown != 0f)
            StartCoroutine(_onCooldown());
    }

    private IEnumerator _onCooldown()
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
