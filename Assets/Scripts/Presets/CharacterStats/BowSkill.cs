using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkill : AttackSkill
{
    public float CameraMaxAditionalFOV = -10;
    [HideInInspector]
    public Vector3 ArrowStartPostion;
    public float BendDistance = 1f;

    private LineRenderer _lineRenderer;
    public ArrowProjectile ArrowProjectile;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        ArrowProjectile = Instantiate(ArrowProjectile, ((WindElement)PlayerController.Instance.PlayerStats.ActualElement).ElementWeapon.transform);

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

