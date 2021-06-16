using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    public bool IsAoe;
    [ConditionalField("IsAoe")]
    public float AoeRadius = 0f;

    public bool IsCasted;
    [ConditionalField("IsCasted")]
    public float CastTime = 0f;

    public bool IsPiercing = false;

    public float Damages;
}
