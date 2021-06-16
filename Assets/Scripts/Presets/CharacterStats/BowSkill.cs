using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BowSkill : AttackSkill
{
    [HideInInspector]
    public Vector3 ArrowStartPostion;
    private float _bendDistance;
    public bool RemindArrow = false;

    private LineRenderer _lineRenderer;
    public ArrowProjectile ArrowProjectile;
    private ArrowProjectile _currentArrow;

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

        _currentArrow = Instantiate(ArrowProjectile, _bow.ArrowPoint.transform);
        _currentArrow.BasePosition = _currentArrow.transform.localPosition;
        _currentArrow.Reminded = RemindArrow;
        _currentArrow.LinkedEntity = this.EntityHolder;
        _bendDistance = _bow.BackArrowPoint.position.z - _bow.ArrowPoint.position.z;

        StartCoroutine(BendBow());
    }

    public IEnumerator BendBow()
    {
        float timer = 0f;
        float castTime = CastTime / PlayerController.Instance.PlayerStats.AtkSpeed;
        while (!HasReleased) 
        {
            if(timer < castTime)
            {
                timer += Time.deltaTime;

                float chargingDist = Mathf.Lerp(_bow.ArrowPoint.localPosition.z, _bow.BackArrowPoint.localPosition.z, timer / castTime);
                _currentArrow.transform.localPosition = new Vector3(0f, 0f, chargingDist);
                _lineRenderer.SetPosition(1, _currentArrow.transform.localPosition);
            }
            
            yield return null;
        }

        this.HasReleased = false;
        if (timer < castTime) {
            Destroy(_currentArrow.gameObject);
            Destroy(_currentArrow);
            _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(0));
        } else {
            _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(0));
            BeginCooldown();
            ThrowProjectile();
        }
    }

    public void ThrowProjectile()
    {
        _currentArrow.transform.parent = GameController.Instance.ProjectilePool.transform;
        if (RemindArrow)
            _bow.RemindedArrows.Add(_currentArrow);

        int mask = LayerMask.GetMask("TriggeredEnemy", "Enemy", "Weapon");
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, ~mask))
            _currentArrow.transform.rotation = Quaternion.LookRotation(hit.point - ((WindElement)(PlayerController.Instance.PlayerStats.ActualElement)).ElementWeapon.transform.position);

        //if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity)){
        //    _currentArrow.transform.rotation = Quaternion.LookRotation(hit.point - this.transform.position);
        //}

        _currentArrow.LaunchProjectile();
    }
}

