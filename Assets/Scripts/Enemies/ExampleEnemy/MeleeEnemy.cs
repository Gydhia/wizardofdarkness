using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BasicEnemy
{
    // Behaviour 
    private Vector3 _nextPlacement;
    public MeleeWeapon Weapon;
    public int Damages = 15;

    private float _attackRange = 4f;
    private Coroutine _attackingCor = null;
    public GameObject AttackParticles;

    protected void Start()
    {
        base.Start();
        Weapon.WeaponDamages = Damages;
    }

    public IEnumerator MeleeAttack()
    {
        this.transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position + (transform.right / 2));
        EnemyAnimator.SetTrigger("Attacked");

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length / 3);
        Weapon.CanHit = true;
        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length / 3 * 2);
        Weapon.CanHit = false;

        _attackingCor = null;
    }
    public void SetNextPlacement()
    {
        TrySetDestination(Target.transform.position);   
    }


    //public IEnumerator CheckToAttack()
    //{
    //    while (TriggeredAggro && !Target.IsDead)
    //    {
    //        if(_isAttacking)
    //            yield return new WaitForSeconds(AttackDelay + );

    //        this.transform.rotation = Quaternion.LookRotation(Target.transform.position - this.transform.position);
    //        Vector3 direction = Target.AimPoint.transform.position - this.Weapon.transform.position;

    //        if (Physics.Raycast(Weapon.transform.position, direction, out RaycastHit hit) && !hit.collider.CompareTag("Player"))
    //        {
    //            SetNextPlacement();
    //        }
    //        else
    //        {
    //            MeleeAttack();
    //        }
    //    }
    //}

    public override IEnumerator StartBehaviour()
    {
        while (TriggeredAggro && !Target.IsDead)
        {
            yield return new WaitForSeconds(0.2f);
            if ((Target.transform.position - this.transform.position).sqrMagnitude / 2.5f >= _attackRange) {
                SetNextPlacement();
            } else {
                TrySetDestination(this.transform.position);
                if (_attackingCor == null)
                    _attackingCor = StartCoroutine(MeleeAttack());
            }
        }

        yield return null;
    }
}
