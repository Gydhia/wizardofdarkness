using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBeam : MonoBehaviour
{
    public float Damages;
    public bool IsCasted = false;
    [ConditionalField("IsCasted")]
    public float CastTime = 0f;

    [ConditionalField("IsCasted")]
    public GameObject ChargingParticle;
    public GameObject ThrowingParticle;

    // In code go
    [ConditionalField("IsCasted")]
    private GameObject ChargingSpell;
    private GameObject ThrowingSpell;

    [HideInInspector]
    public Vector3 Target;

    public void CastSpell()
    {
        if (IsCasted)
        {
            ChargingSpell = Instantiate(ChargingParticle, this.transform);
            ChargingParticle.SetActive(true);
        }
    }

    public void EndCast()
    {
        if (IsCasted)
            Destroy(ChargingSpell);

        ThrowingSpell = Instantiate(ThrowingParticle, this.transform);
        ThrowingSpell.transform.rotation = Quaternion.LookRotation(Target - this.transform.position);

        ThrowingSpell.GetComponent<ProjectileCollision>().ProjectileDamages = this.Damages;
    }
}
