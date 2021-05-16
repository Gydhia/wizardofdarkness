using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBoost : Skill
{
    public float buffTime;

    public override void ActivatedSkill()
    {
        /*
         bande l'arc plus rapidement. Pour �a, je dirais que tu fait l'impl�mentation dans PlayerStats. Ici, tu met qu'un bool�en set � true, puis PlayerStats fera le reste.
         D'ailleurs, fonce dans playerStats, maintenant!
         */
        PlayerCoroutines.Instance.LaunchRoutine(PlayerStats.Instance.StatBuff(buffTime, EStatsDebuffs.AttackSpeed, 70));
        canLaunch = false;
    }

}