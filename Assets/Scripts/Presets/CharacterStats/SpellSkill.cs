using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSkill : AttackSkill
{
    public SpellProjectile SpellProjectile;
    private SpellProjectile _actualSpell;

    public bool IsBeam = false;
    [ConditionalField("IsBeam")]
    public float BeamTime = 0f;

    public override void ActivatedSkill()
    {
        IsBeingCast = true;
        this.HasReleased = false;

        if(_actualSpell == null)
            _actualSpell = Instantiate(SpellProjectile, PlayerController.Instance.PlayerStats.AimPoint.transform);
        _actualSpell.Damages = this.Damages;
        _actualSpell.IsCasted = this.IsCasted;
        _actualSpell.CastTime = this.CastTime;
        _actualSpell.IsBeam = this.IsBeam;
        _actualSpell.BeamTime = this.BeamTime;

        _actualSpell.gameObject.SetActive(true);
        StartCoroutine(CastSpell());
    }

    public IEnumerator CastSpell()
    {
        if (IsCasted)
        {
            _actualSpell.CastSpell();
            float timer = 0f;
            while (!HasReleased && timer < CastTime)
            { 
                timer += Time.deltaTime;
                yield return null;
            }
            if(timer < CastTime - 0.01f) {
                _actualSpell.EndCast();
                IsBeingCast = false;
                CanLaunch = true;
                yield break;
            }
        }
  
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity))
            _actualSpell.Target = hit.point;
        else
            _actualSpell.Target = Vector3.forward;

        _actualSpell.EndCast();
        _actualSpell.ThrowSpell();
        BeginCooldown();

        this.HasReleased = false;
    }
}
