using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : BasicEnemy
{
    private float _castingTime = 0.5f;
    public GameObject Spell;
    public GameObject SpellAnchor;

    public void RangeAttack()
    {
        GameObject spell = Instantiate(Spell, SpellAnchor.transform);
        spell.GetComponent<Fireball>().CastSpell(Target.AimPoint.gameObject, _castingTime);
    }
    public void Disengage()
    {

    }
    public void AdjustPlacement()
    {
        Vector3 newDestination = new Vector3();


    }

    public void CheckToAttack()
    {
        Vector3 direction = Target.AimPoint.transform.position - this.SpellAnchor.transform.position;
        
        if(Physics.Raycast(SpellAnchor.transform.position, direction, out RaycastHit hit) && !hit.collider.CompareTag("Player")){
            Debug.DrawRay(SpellAnchor.transform.position, direction, Color.green, 1f);
            Debug.Log("Need to adjust placement");
            AdjustPlacement();    
        } else {
            Debug.DrawRay(SpellAnchor.transform.position, direction, Color.green, 1f);
            Debug.Log("Can attack player");
            RangeAttack();
        }
    }

    public override IEnumerator StartBehaviour()
    {
        while (TriggeredAggro && !Target.IsDead)
        {
            yield return new WaitForSeconds(AttackDelay);
            CheckToAttack();
        }

        yield return null;
    }
}
