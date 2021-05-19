using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingBackArrows : Skill
{

    public override void ActivatedSkill()
    {
        foreach(ArrowScript a in ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).ActiveArrows)
            a.isBeingCalledBack = true;

        base.ActivatedSkill();
    }
}
