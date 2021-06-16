using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float ProjectileDamages;

    public bool IsPiercing;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out EntityStat entity))
            {
                entity.TakeDamage((int)ProjectileDamages);
            }
        }

        Invoke("DelayDestroy", 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out EntityStat entity))
            {
                entity.TakeDamage((int)ProjectileDamages);
            }
        }

        Invoke("DelayDestroy", 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent(out EntityStat entity))
            {
                entity.TakeDamage((int)ProjectileDamages);
            }
        }

        Invoke("DelayDestroy", 1.5f);
    }

    private void DelayDestroy()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
