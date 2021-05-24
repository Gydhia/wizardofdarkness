using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BowSkill : AttackSkill
{
    [HideInInspector]
    public Vector3 ArrowStartPostion;
    public float BendDistance = 1f;

    private LineRenderer _lineRenderer;
    public ArrowProjectile ArrowProjectile;

    private Bow _bow;

    private void Start()
    {
        Element windElement = PlayerController.Instance.PlayerStats.Elements.SingleOrDefault(element => element.Type == EElements.Wind);
        _bow = windElement.ElementWeapon.GetComponent<Bow>();

        _lineRenderer = _bow._stringRenderer;
    }

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        ArrowProjectile = Instantiate(ArrowProjectile, _bow.ArrowPoint.transform);

        StartCoroutine(BendBow());
    }

    public IEnumerator BendBow()
    {
        float timer = 0f;
        while (!HasReleased) 
        {
            if(timer < CastTime)
            {
                timer += Time.deltaTime;

                ArrowProjectile.transform.localPosition += new Vector3(0f, 0f, BendDistance / CastTime * timer);
                _lineRenderer.SetPosition(1, ArrowProjectile.BackPoint.transform.localPosition + ArrowProjectile.transform.localPosition);
            }
            yield return null;
        }

        _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(0));
        BeginCooldown();
        ThrowProjectile();
        this.HasReleased = false;
    }

    public void ThrowProjectile()
    {
        ArrowProjectile.LaunchProjectile();
    }
}

