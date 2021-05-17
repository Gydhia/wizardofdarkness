using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    public float warpTime;
    public override void ActivatedSkill()
    {
        PlayerCoroutines.Instance.LaunchRoutine(PlayerCoroutines.Instance.VoidDash(warpTime,this));
        Debug.Log("dash!");
        base.ActivatedSkill();
    }

}
