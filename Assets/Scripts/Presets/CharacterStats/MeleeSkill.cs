using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeSkill : AttackSkill
{
    public Animator WeaponAnimator;

    private void Start()
    {
        Element earthElement = PlayerController.Instance.PlayerStats.Elements.SingleOrDefault(element => element.Type == EElements.Earth);
        WeaponAnimator = earthElement.ElementWeapon.GetComponent<Animator>();
    }

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        WeaponAnimator.SetTrigger("Hit");
    }
}
