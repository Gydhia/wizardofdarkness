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
        Debug.Log(Target.transform.position);
        ThrowingSpell = Instantiate(ThrowingParticle, this.transform);
        ThrowingSpell.transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if(TryGetComponent(out PlayerStats player)) {
                player.TakeDamage(ProjectileDamages);
            }
        }

        Invoke("DelayDestroy", 1.5f);
    }
    private void DelayDestroy()
    {
        Destroy(gameObject);
    }
}
