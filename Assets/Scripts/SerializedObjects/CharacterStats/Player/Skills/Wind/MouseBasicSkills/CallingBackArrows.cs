using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingBackArrows : Skill
{

    public override void ActivatedSkill()
    {
        foreach(GameObject a in PlayerStats.Instance.activeArrows)
        {

        }
    }
}
