using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicShock : Skill
{
    float buffTimer = 7f;
    public override void ActivatedSkill()
    {
        base.ActivatedSkill();
        /*"Réduction de dégâts - 75% pendant 7 secondes -> boost la parade
        de base durant l'effet"
        Genre de l'armure de fou en gros quoi
        bref, maintenant brow go playerstats.cs! Oeoe
         */
        PlayerController.Instance.PlayerStats.LaunchStatModifier(buffTimer, EStatsDebuffs.Defense, 75);
    }
}
