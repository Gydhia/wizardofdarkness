using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDebuffs { Stun }
public enum EStatsDebuffs { MaxHP, Defense, Strength, AttackSpeed, MoveSpeed }

public abstract class EnemyStats : EntityStat
{
    public enum EEnemyElements { Void = 0, Wind = 1, Earth = 2, None = 3 }
    public EEnemyElements ActualElement;

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
}
