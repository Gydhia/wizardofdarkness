using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBoost : Skill
{
    public float buffTime;

    public override void ActivatedSkill()
    {
        /*
         bande l'arc plus rapidement. Pour ça, je dirais que tu fait l'implémentation dans PlayerStats. Ici, tu met qu'un booléen set à true, puis PlayerStats fera le reste.
         D'ailleurs, fonce dans playerStats, maintenant!
         */
        PlayerController.Instance.PlayerStats.LaunchStatModifier(buffTime, EStatsDebuffs.AttackSpeed, 70);
        CanLaunch = false;
    }

}
