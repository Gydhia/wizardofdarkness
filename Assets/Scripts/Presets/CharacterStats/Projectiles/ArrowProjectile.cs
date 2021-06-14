using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public Vector3 BasePosition;

    private bool _isLaunched = false;
    private float _projectileSpeed = 90f;

    private void Update()
    {
        if (_isLaunched)
        {
            transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
        }
    }
    public void LaunchProjectile()
    {
        _isLaunched = true;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out EntityStat entity))
        {
            entity.TakeDamage(10);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.TryGetComponent(out EntityStat entity))
        {
            entity.TakeDamage(10);
        }
    }
}
