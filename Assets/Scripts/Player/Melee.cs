using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Melee : MonoBehaviour
{
    public int Damages;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out EntityStat entity))
            {
                entity.TakeDamage(Damages);
            }
        }
    }
}
