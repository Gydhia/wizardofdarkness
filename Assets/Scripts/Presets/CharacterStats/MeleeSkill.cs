using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeSkill : AttackSkill
{
    public Animator WeaponAnimator;
    public string AnimationName;

    public ParticleSystem AttackParticle;
    private ParticleSystem _attackParticle;

    private void Start()
    {
        Element earthElement = PlayerController.Instance.PlayerStats.Elements.SingleOrDefault(element => element.Type == EElements.Earth);
        WeaponAnimator = earthElement.ElementWeapon.GetComponent<Animator>();
    }

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        WeaponAnimator.SetTrigger(AnimationName);
        if(AttackParticle != null) {
            if(_attackParticle == null)
            {
                _attackParticle = Instantiate(AttackParticle,
                PlayerController.Instance.transform.position + new Vector3(0f, 0.1f, 0f),
                Quaternion.identity,
                GameController.Instance.ProjectilePool.transform);
            } else {
                _attackParticle.transform.position = PlayerController.Instance.transform.position + new Vector3(0f, 0.1f, 0f);
            }
            _attackParticle.Clear();
            _attackParticle.Play();
        }
        if (IsAoe) {
            Collider[] colliders = Physics.OverlapSphere(PlayerController.Instance.transform.position, this.AoeRadius);
            foreach (Collider entity in colliders)
            {
                if(entity.TryGetComponent(out EntityStat target)) {
                    if(target != this.EntityHolder)
                        target.TakeDamage((int)this.Damages);
                }
            }    
        }
    }
}
