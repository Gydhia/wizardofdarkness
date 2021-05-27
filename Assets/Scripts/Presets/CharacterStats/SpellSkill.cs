using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSkill : AttackSkill
{
    public SpellProjectile SpellProjectile;

    public bool IsBeam = false;
    [ConditionalField("IsBeam")]
    public float BeamTime = 0f;

    public override void ActivatedSkill()
    {
        if (IsBeingCast) return;

        IsBeingCast = true;
        base.ActivatedSkill();

        SpellProjectile = Instantiate(SpellProjectile, PlayerController.Instance.PlayerStats.AimPoint.transform);
        SpellProjectile.Damages = this.Damages;
        SpellProjectile.IsCasted = this.IsCasted;
        SpellProjectile.CastTime = this.CastTime;
        SpellProjectile.IsBeam = this.IsBeam;
        SpellProjectile.BeamTime = this.BeamTime;

        StartCoroutine(CastSpell());
    }

    public IEnumerator CastSpell()
    {
        CanLaunch = false;
        if (IsCasted)
        {
            SpellProjectile.CastSpell();
            float timer = 0f;
            while (!HasReleased && timer < CastTime)
            { 
                timer += Time.deltaTime;
                yield return null;
            }
            if(timer < CastTime - 0.01f) {
                SpellProjectile.EndCast();
                yield break;
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity))
            SpellProjectile.Target = hit.transform.position;
        else
            SpellProjectile.Target = Vector3.zero;
            
        SpellProjectile.EndCast();
        SpellProjectile.ThrowSpell();
        BeginCooldown();
        
        this.HasReleased = false;
    }
}
