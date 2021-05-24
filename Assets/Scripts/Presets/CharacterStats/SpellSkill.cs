using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSkill : AttackSkill
{
    public SpellProjectile SpellProjectile;

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        SpellProjectile = Instantiate(SpellProjectile, ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).ElementWeapon.transform);
        SpellProjectile.Damages = this.Damages;
        SpellProjectile.IsCasted = this.IsCasted;
        SpellProjectile.CastTime = this.CastTime;

        StartCoroutine(CastSpell());
    }

    public IEnumerator CastSpell()
    {
        if (IsCasted)
        {
            SpellProjectile.CastSpell();
            float timer = 0f;
            while (!HasReleased && timer < CastTime)
            { 
                timer += Time.deltaTime;
                yield return null;
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity))
            SpellProjectile.Target = hit.transform.position;
        else
            SpellProjectile.Target = Vector3.zero;
            
        SpellProjectile.EndCast();
        BeginCooldown();
        
        this.HasReleased = false;
    }
}
