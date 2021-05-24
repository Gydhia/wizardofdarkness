using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSkill : Skill
{
    public bool IsTeleported;

    [ConditionalField("IsTeleported", true)]
    public bool IsStraight;

    [ConditionalField("IsStraight", true)]
    public float DashHeight;
    [ConditionalField("IsStraight", true)]
    public float DashLength;

    public void SetupDash()
    {
        List<Vector3> DashPath = MathsFunctions.GetBezierCurve(DashHeight, DashLength);
    }
}
