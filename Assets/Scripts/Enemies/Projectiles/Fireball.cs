using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Inspector assets
    public GameObject ChargingParticle;
    public GameObject ThrowingParticle;

    // In code go
    private GameObject ChargingSpell;
    private GameObject ThrowingSpell;

    public int ProjectileDamages = 120;
    public float CastingTime;

    [HideInInspector]
    public GameObject Target;

    public void CastSpell(GameObject target, float castingTime)
    {
        this.Target = target;
        this.CastingTime = castingTime;

        ChargingSpell = Instantiate(ChargingParticle, this.transform);
        ChargingParticle.SetActive(true);
        StartCoroutine(EndCast());
    }

    public IEnumerator EndCast()
    {
        yield return new WaitForSeconds(CastingTime);
        
        Destroy(ChargingSpell);

        ThrowingSpell = Instantiate(ThrowingParticle, this.transform);
        ThrowingSpell.transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);

        ThrowingSpell.GetComponent<FireballCollision>().ProjectileDamages = this.ProjectileDamages;
    }

    
}
