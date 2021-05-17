using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingBackArrows : Skill
{

    public override void ActivatedSkill()
    {
        /*�a mon pote, �a marche du feu de dieu frr
         c'�tait assez simple, faut juste que t'ailles voir ArrowScript.cs maintenant, c'est le script sur chaque fl�che.*/
        foreach(ArrowScript a in PlayerStats.Instance.activeArrows)
        {
            a.isBeingCalledBack = true;
        }
        base.ActivatedSkill();
    }
}
