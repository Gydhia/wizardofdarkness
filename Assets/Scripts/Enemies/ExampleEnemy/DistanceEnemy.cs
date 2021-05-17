using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : BasicEnemy
{
    // Behaviour 
    public float PreferedDistance = 50f;
    public float DangerDistance = 20f;
    public float MinDisengageDistance = 20f;
    public float MaxDisengageDistance = 30f;
    private Vector3 _disengagePosition;
    private Vector3 _adjustingPlacement;

    private float _castingTime = 0.5f;
    public GameObject Spell;
    public GameObject SpellAnchor;

    public void RangeAttack()
    {
        GameObject spell = Instantiate(Spell, SpellAnchor.transform);
        spell.GetComponent<Fireball>().CastSpell(Target.AimPoint.gameObject, _castingTime);
        EnemyAnimator.SetTrigger("Attacked");
    }
    public void Disengage()
    {
        _disengagePosition = this.transform.position + Random.insideUnitSphere * (Random.Range(MinDisengageDistance, MaxDisengageDistance));
        
        this.Agent.SetDestination(new Vector3(_disengagePosition.x, 0f, _disengagePosition.y));
    }
    public void SetNextPlacement()
    {
        if (Physics.Raycast(SpellAnchor.transform.position, (transform.forward / 2 - transform.right).normalized, out RaycastHit leftHit))
        {
            if(Physics.Raycast(SpellAnchor.transform.position, (transform.forward / 2 + transform.right).normalized, out RaycastHit rightHit)){
                _adjustingPlacement = leftHit.collider.transform.position.sqrMagnitude < rightHit.transform.position.sqrMagnitude ? leftHit.transform.position : rightHit.transform.position;
            }
        }
        this.Agent.SetDestination(_adjustingPlacement);
    }


    public IEnumerator CheckToAttack()
    {
        while(TriggeredAggro && !Target.IsDead)
        {
            yield return new WaitForSeconds(AttackDelay + _castingTime);

            this.transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);
            Vector3 direction = Target.AimPoint.transform.position - this.SpellAnchor.transform.position;

            if (Physics.Raycast(SpellAnchor.transform.position, direction, out RaycastHit hit) && !hit.collider.CompareTag("Player")) {
                SetNextPlacement();
            } else {
                this.Agent.SetDestination(this.transform.position);
                _adjustingPlacement = Vector3.zero;
                RangeAttack();
            }
        }
    }

    public override IEnumerator StartBehaviour()
    {
        StartCoroutine(CheckToAttack());

        while (TriggeredAggro && !Target.IsDead)
        {
            yield return new WaitForSeconds(0.3f);
            if((Target.transform.position - this.transform.position).sqrMagnitude / 5f <= DangerDistance) {
                if(this.Agent.remainingDistance < 0.5f)
                    Disengage();
            }
        }

        yield return null;
    }
}
