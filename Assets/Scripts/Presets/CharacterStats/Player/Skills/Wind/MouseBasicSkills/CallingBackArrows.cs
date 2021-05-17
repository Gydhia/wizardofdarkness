using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingBackArrows : Skill
{

    public override void ActivatedSkill()
    {
        /*ça mon pote, ça marche du feu de dieu frr
         c'était assez simple, faut juste que t'ailles voir ArrowScript.cs maintenant, c'est le script sur chaque flèche.*/
        foreach(ArrowScript a in PlayerStats.Instance.activeArrows)
        {
            a.isBeingCalledBack = true;
        }
        base.ActivatedSkill();
    }
}
