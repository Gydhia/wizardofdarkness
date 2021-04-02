using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingBackArrows : Skill
{

    public override void ActivatedSkill()
    {
        foreach(ArrowScript a in PlayerStats.Instance.activeArrows)
        {
            a.isBeingCalledBack = true;
        }
    }
}
