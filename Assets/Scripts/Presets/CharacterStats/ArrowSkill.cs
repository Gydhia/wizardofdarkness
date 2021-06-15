using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowSkill : AttackSkill
{
    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        Bow bow = ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).ElementWeapon.GetComponent<Bow>();

        bow.RemindedArrows.ForEach(arrow => arrow.CallBackArrow());
    }
}
