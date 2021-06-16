using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float Damages;
    public bool IsCasted = false;
    [ConditionalField("IsCasted")]
    public float CastTime = 0f;

    public bool IsBeam = false;
    [ConditionalField("IsBeam")]
    public float BeamTime = 0f;

    [ConditionalField("IsCasted")]
    public ParticleSystem ChargingParticle;
    public ParticleSystem ThrowingParticle;

    // In code go
    [ConditionalField("IsCasted")]
    private ParticleSystem _chargingSpell;
    private ParticleSystem _throwingSpell;

    [HideInInspector]
    public Vector3 Target;

    public void CastSpell()
    {
        if (IsCasted)
        {
            if(_chargingSpell == null) {
                _chargingSpell = Instantiate(ChargingParticle, this.transform);
            } else {
                _chargingSpell.Clear();
            }

            _chargingSpell.gameObject.SetActive(true);
            _chargingSpell.Play();
        }
    }

    public void EndCast()
    {
        if (IsCasted) {
            _chargingSpell.Stop();
            _chargingSpell.gameObject.SetActive(false);
        }
    }
    public void ThrowSpell()
    {
        if(_throwingSpell == null)
            _throwingSpell = Instantiate(ThrowingParticle, this.transform);
        _throwingSpell.transform.rotation = Quaternion.LookRotation(Target - this.transform.position);

        if (!IsBeam)
            _throwingSpell.GetComponent<ProjectileCollision>().ProjectileDamages = this.Damages;
        else {
            BeamCollision beam = _throwingSpell.GetComponent<BeamCollision>();
            beam.BeamTime = this.BeamTime;
            beam.BeamDamages = this.Damages;
            beam.BeamParticles = _throwingSpell;
        }

        _throwingSpell.gameObject.SetActive(true);
            
    }
}
