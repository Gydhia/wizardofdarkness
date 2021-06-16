using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : Skill
{    
    public EStatsBuffs StatBuff;
    public float BuffTime;
    public int BuffPercent;

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        EntityHolder.LaunchStatModifier(BuffTime, StatBuff, BuffPercent);
    }
}
