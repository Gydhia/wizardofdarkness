using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStat
{
    
    public EEnemyElements ActualElement;
    public Animator EntityAnimator;
    public EnemyHealth EnemyHealth;

    public Skill[] Skills;
    public MeshRenderer MatRenderer;
    bool IsFlashing;
    [Header("Debuff")]
    public Animator EnemyAnimator;

    protected void Start()
    {
        EnemyAnimator = this.GetComponent<Animator>();
        this.HP = this.MaxHP;
        EnemyHealth.Init(this.MaxHP);
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        EnemyHealth.RefreshHealth(this.HP);
    }

    public void ActivateSkill(int index)
    {
        Skills[index].ActivatedSkill();
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject, 1.5f);
        EnemyAnimator.SetTrigger("Death");
        GameController.Instance.FireOnEnemyDeath();
    }
}
