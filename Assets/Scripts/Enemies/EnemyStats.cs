using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : EntityStat
{
    
    public EEnemyElements ActualElement;
    public Animator EntityAnimator;

    public Skill[] Skills;
    public MeshRenderer MatRenderer;
    bool IsFlashing;
    [Header("Debuff")]
    public Animator EnemyAnimator;

    protected void Start()
    {
        EnemyAnimator = this.GetComponent<Animator>();
    }

    public void ActivateSkill(int index)
    {
        Skills[index].ActivatedSkill();
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject, 1.5f);
        GameController.Instance.FireOnEnemyDeath();
    }
}
