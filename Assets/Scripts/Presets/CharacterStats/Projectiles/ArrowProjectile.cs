using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject BackPoint;

    public bool IsLaunched = false;
    private float _projectileSpeed = 0.5f;

    private void Update()
    {
        if (IsLaunched)
        {
            transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
        }
    }

    public void LaunchProjectile()
    {
        IsLaunched = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out EntityStat entity))
        {
            entity.TakeDamage(10);
        }
    }
}
